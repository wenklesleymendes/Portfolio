using AutoMapper;
using EscolaPro.Core.Helpers;
using EscolaPro.Core.Model;
using EscolaPro.Core.Model.Enums;
using EscolaPro.Core.Model.MetasComissoes;
using EscolaPro.Core.Model.Pagamentos;
using EscolaPro.Core.Model.PainelMatricula;
using EscolaPro.Core.Model.PainelMatricula.Enums;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Interfaces.EmailEnviados;
using EscolaPro.Repository.Interfaces.MatriculaAlunos;
using EscolaPro.Repository.Interfaces.Pagamentos;
using EscolaPro.Repository.Interfaces.Tickets;
using EscolaPro.Service.Dto;
using EscolaPro.Service.Dto.MatriculaAlunoVO;
using EscolaPro.Service.Dto.PagamentosVO;
using EscolaPro.Service.Dto.TicketVO;
using EscolaPro.Service.Dto.UsuarioVO;
using EscolaPro.Service.Interfaces;
using EscolaPro.Service.Interfaces.MatriculaAlunos;
using FastReport;
using FastReport.Export.PdfSimple;
using FastReport.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes.MatriculaAlunos
{
    public class CancelamentoMatriculaService : ICancelamentoMatriculaService
    {
        private readonly IMapper _mapper;
        private readonly ITicketService _ticketService;
        private readonly IUsuarioService _usuarioService;
        private readonly IUnidadeService _unidadeService;
        private readonly IAlunoRepository _alunoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IWebHostEnvironment _webHostingEnvironment;
        private readonly IMatriculaAlunoRepository _matriculaAlunoRepository;
        private readonly IAssuntoTicketRepository _assuntoTicketRepository;
        private readonly ICancelamentoIsencaoRepository _cancelamentoIsencaoRepository;
        private readonly IAlunoFinanceiroContratoService _alunoFinanceiroContratoService;
        private readonly ICancelamentoMatriculaRepository _cancelamentoMatriculaRepository;
        private readonly ICancelamentoIsencaoPagamentoRepository _cancelamentoIsencaoPagamentoRepository;
        private readonly List<TipoSituacaoEnum> statusAberdo;

        public CancelamentoMatriculaService(
            IMapper mapper,
            ITicketService ticketService,
            IUsuarioService usuarioService,
            IUnidadeService unidadeService,
            IAlunoRepository alunoRepository,
            IUsuarioRepository usuarioRepository,
            IWebHostEnvironment webHostingEnvironment,
            IAssuntoTicketRepository assuntoTicketRepository,
            IMatriculaAlunoRepository matriculaAlunoRepository,
            ICancelamentoIsencaoRepository cancelamentoIsencaoRepository,
            IAlunoFinanceiroContratoService alunoFinanceiroContratoService,
            ICancelamentoMatriculaRepository cancelamentoMatriculaRepository,
            ICancelamentoIsencaoPagamentoRepository cancelamentoIsencaoPagamentoRepository)
        {
            _mapper = mapper;
            _ticketService = ticketService;
            _usuarioService = usuarioService;
            _unidadeService = unidadeService;
            _alunoRepository = alunoRepository;
            _alunoRepository = alunoRepository;
            _usuarioRepository = usuarioRepository;
            _webHostingEnvironment = webHostingEnvironment;
            _assuntoTicketRepository = assuntoTicketRepository;
            _matriculaAlunoRepository = matriculaAlunoRepository;
            _cancelamentoIsencaoRepository = cancelamentoIsencaoRepository;
            _alunoFinanceiroContratoService = alunoFinanceiroContratoService;
            _cancelamentoMatriculaRepository = cancelamentoMatriculaRepository;
            _cancelamentoIsencaoPagamentoRepository = cancelamentoIsencaoPagamentoRepository;
            statusAberdo = new List<TipoSituacaoEnum>();
            statusAberdo.Add(TipoSituacaoEnum.Aberto);
            statusAberdo.Add(TipoSituacaoEnum.Inadimplente);
            statusAberdo.Add(TipoSituacaoEnum.InadimplenteBloqueado);
            statusAberdo.Add(TipoSituacaoEnum.Residual);
        }
        public async Task<DtoCancelamentoMatriculaResult> BuscarPorMatricula(int matriculaId)
        {
            try
            {

                DtoCancelamentoMatriculaResult cancelamentoMatricula = new DtoCancelamentoMatriculaResult();
                CancelamentoMatricula cancelamento = await _cancelamentoMatriculaRepository.BuscarPorMatricula(matriculaId);
                if (cancelamento != null)
                {
                    cancelamentoMatricula = _mapper.Map<DtoCancelamentoMatriculaResult>(cancelamento);
                    List<int> idPG = cancelamento.CancelamentoIsencaos.SelectMany(x => x?.CancelamentoIsencaoPagamentos.Select(y => y.PagamentoId)).ToList();
                    if (cancelamento.CancelamentoIsencaos.Count > 0)
                    {
                        var isencao = await _cancelamentoIsencaoRepository.GetByIdAsync(cancelamento.CancelamentoIsencaos.OrderByDescending(x => x.Id).FirstOrDefault().Id);
                        var usuario = await _usuarioRepository.BuscarPorId(isencao.UsarioId);
                        cancelamentoMatricula.CancelamentoIsencao = new DtoCancelamentoIsencao
                        {
                            Id = isencao.Id,
                            Justificativa = isencao.Justificativa,
                            NomeUsuario = usuario?.Funcionario?.Nome ?? usuario.UserName
                        };
                    }
                    var painelFinanceiro = await _alunoFinanceiroContratoService.ConsultarPainelFinanceiro(matriculaId);
                    cancelamentoMatricula.ParcelasEmAtraso = painelFinanceiro.Pagamento.Where(x => idPG?.Contains(x.Id) == true ||
                                                                                                x.Descricao.ToUpper().Contains("MULTA DE CANCELAMENTO") ||
                                                                                              ((x.Descricao.ToUpper().Contains("TAXA DE INSCRIÇÃO") ||
                                                                                                x.Descricao.ToUpper().Contains("TAXA DE MATRICULA") ||
                                                                                                x.Descricao.ToUpper().Contains("PARCELA") ||
                                                                                                x.TipoSituacao == TipoSituacaoEnum.Residual) &&
                                                                                                statusAberdo.Contains(x.TipoSituacao) &&
                                                                                                x.DataVencimento?.Date < DateTime.Now.Date)).ToList();

                    cancelamentoMatricula.ValorEmAtraso = cancelamentoMatricula.ParcelasEmAtraso.Where(x => x.TipoSituacao != TipoSituacaoEnum.Isento && !x.Descricao.ToUpper().Contains("MULTA DE CANCELAMENTO")).Sum(x => x.Valor);

                    cancelamentoMatricula.PagoTotal = !painelFinanceiro.Pagamento.Any(x => idPG?.Contains(x.Id) == true ||
                                                                                             ((x.Descricao.ToUpper().Contains("TAXA DE INSCRIÇÃO") ||
                                                                                               x.Descricao.ToUpper().Contains("TAXA DE MATRICULA") ||
                                                                                               x.Descricao.ToUpper().Contains("PARCELA") ||
                                                                                               x.TipoSituacao == TipoSituacaoEnum.Residual) &&
                                                                                               statusAberdo.Contains(x.TipoSituacao)));
                    if (cancelamentoMatricula.StatusCancelamento != StatusCancelamento.Efetivado && !cancelamentoMatricula.DentroPrazoCancelamento && !cancelamentoMatricula.PagoTotal)
                    {
                        if (!cancelamentoMatricula.ParcelasEmAtraso.Any(x => x.Descricao.ToUpper().Contains("MULTA DE CANCELAMENTO")))
                        {
                            if (painelFinanceiro.Pagamento.Any(x => x.Descricao.ToUpper().Contains("MULTA DE CANCELAMENTO")))
                                cancelamentoMatricula.ParcelasEmAtraso.Insert(0, painelFinanceiro.Pagamento.Where(x => x.Descricao.ToUpper().Contains("MULTA DE CANCELAMENTO")).FirstOrDefault());
                            else if (!cancelamentoMatricula.DentroPrazoCancelamento && !cancelamentoMatricula.PagoTotal)
                                cancelamentoMatricula.ParcelasEmAtraso.Insert(0, this.GerarPagemanto(cancelamentoMatricula));
                        }
                    }
                }
                else
                {
                    var painelFinanceiro = await _alunoFinanceiroContratoService.ConsultarPainelFinanceiro(matriculaId);
                    cancelamentoMatricula.MatriculaAlunoId = matriculaId;
                    cancelamentoMatricula.DataCancelamento = DateTime.Now;
                    cancelamentoMatricula.PagoTotal = painelFinanceiro?.PlanoPagamentoAluno?.TipoPagamento == Core.Model.Enums.TipoPagamentoEnum.CartaoDebito ||
                                                      painelFinanceiro?.PlanoPagamentoAluno?.TipoPagamento == Core.Model.Enums.TipoPagamentoEnum.CartaoCredito ||
                                                      painelFinanceiro?.Pagamento.Any(x => (x.Descricao.Contains("Taxa de Inscrição") ||
                                                                                            x.Descricao.Contains("Taxa de Matricula") ||
                                                                                            x.Descricao.Contains("Parcela")) &&
                                                                                            statusAberdo.Contains(x.TipoSituacao)) == false;
                    cancelamentoMatricula.ValorMultaCancelamento = !cancelamentoMatricula.PagoTotal ?
                                          painelFinanceiro.Pagamento.Where(x => (x.Descricao.Contains("Parcela") || x.TipoSituacao == TipoSituacaoEnum.Residual))
                                                                    .Sum(x => x.Valor) * 0.2m :
                                                                    0m;
                    cancelamentoMatricula.MatriculaAluno = _mapper.Map<DtoMatriculaAluno>(await _matriculaAlunoRepository.BuscarPorId(matriculaId));
                    cancelamentoMatricula.ParcelasEmAtraso = painelFinanceiro.Pagamento.Where(x => (x.Descricao.Contains("Taxa de Inscrição") ||
                                                                                                    x.Descricao.Contains("Taxa de Matricula") ||
                                                                                                    x.Descricao.Contains("Parcela") ||
                                                                                                    x.TipoSituacao == TipoSituacaoEnum.Residual) &&
                                                                                                    (x.TipoSituacao != TipoSituacaoEnum.Pago && x.TipoSituacao != TipoSituacaoEnum.Isento) &&
                                                                                                    x.DataVencimento?.Date < DateTime.Now.Date).ToList();

                    if (cancelamentoMatricula.ParcelasEmAtraso != null && (cancelamentoMatricula.ParcelasEmAtraso.Count() > 0))
                        cancelamentoMatricula.ValorEmAtraso = cancelamentoMatricula.ParcelasEmAtraso.Where(x => x.TipoSituacao != TipoSituacaoEnum.Isento).Sum(x => x.Valor);

                    if (!cancelamentoMatricula.DentroPrazoCancelamento)
                        cancelamentoMatricula.ParcelasEmAtraso.Insert(0, this.GerarPagemanto(cancelamentoMatricula));
                }
                return cancelamentoMatricula;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<DtoCancelamentoMatriculaResult> EfetuarCancelamento(DtoCancelamentoMatriculaRequest cancelamentoMatricula)
        {
            CancelamentoMatricula cancelamento = new CancelamentoMatricula();
            cancelamento = _mapper.Map<CancelamentoMatricula>(cancelamentoMatricula);
            cancelamento.StatusCancelamento = StatusCancelamento.Efetivado;

            if (cancelamentoMatricula.Id > 0)
            {
                _ = await _cancelamentoMatriculaRepository.UpdateAsync(cancelamento);
                await CancelarMatricula(cancelamentoMatricula.MatriculaAlunoId);
            }
            else
            {
                cancelamento = await _cancelamentoMatriculaRepository.AddAsync(cancelamento);

                if (!cancelamentoMatricula.DentroPrazoCancelamento && !cancelamentoMatricula.PagoTotal)
                    _ = await GerarMultaCancelamento(cancelamentoMatricula);
                else
                    await CancelarMatricula(cancelamentoMatricula.MatriculaAlunoId);
            }


            _ = _mapper.Map<DtoCancelamentoMatriculaResult>(cancelamento);

            return _mapper.Map<DtoCancelamentoMatriculaResult>(cancelamento);
        }

        private async Task CancelarMatricula(int matriculaAlunoId)
        {
            if (await _matriculaAlunoRepository.AlterarStatus(matriculaAlunoId, false))
            {
                var matricula = await _matriculaAlunoRepository.BuscarPorId(matriculaAlunoId);
                Core.Model.Aluno aluno = await _alunoRepository.BuscarPorId(matricula.AlunoId);
                if (!aluno.Matriculas.Any(x => x.Id != matriculaAlunoId))
                {
                    aluno.IsActive = false;
                    await _alunoRepository.UpdateAsync(aluno);
                    var usuario = await _usuarioRepository.BuscarPorAlunoId(aluno.Id);
                    usuario.IsActive = false;
                    await _usuarioRepository.UpdateAsync(usuario);
                }
            }
        }

        public async Task<bool> SalvarAutorizacaoIsencao(DtoCancelamentoAutorizacaoIsencao autorizacaoIsencao)
        {
            try
            {
                var usuario = await _usuarioRepository.Login(autorizacaoIsencao.Login, autorizacaoIsencao.Senha);
                if (usuario?.PerfilUsuario?.IsentarMultaCancelamento != true)
                    return false;

                CancelamentoMatricula cancelamento = new CancelamentoMatricula();
                autorizacaoIsencao.CancelamentoMatricula.StatusCancelamento = StatusCancelamento.AConfirmar;
                autorizacaoIsencao.CancelamentoMatricula.IsentarCancelamento = true;

                if (autorizacaoIsencao.CancelamentoMatricula.Id == 0)
                {
                    cancelamento = await _cancelamentoMatriculaRepository.AddAsync(_mapper.Map<CancelamentoMatricula>(autorizacaoIsencao.CancelamentoMatricula));
                    await CancelarMatricula(autorizacaoIsencao.CancelamentoMatricula.MatriculaAlunoId);
                    if (!autorizacaoIsencao.Pagamentos.Any(x => x.Id == 0))
                        await _alunoFinanceiroContratoService.GerarMultaCancelamento(this.GerarPagemanto(autorizacaoIsencao.CancelamentoMatricula), false);
                }
                else
                    cancelamento = _mapper.Map<CancelamentoMatricula>(autorizacaoIsencao.CancelamentoMatricula);

                CancelamentoIsencao cancelamentoIsencao = new CancelamentoIsencao
                {
                    CancelamentoMatriculaId = cancelamento.Id,
                    Justificativa = autorizacaoIsencao.Justificativa,
                    MatriculaId = autorizacaoIsencao.CancelamentoMatricula.MatriculaAlunoId,
                    MotivoIsencao = autorizacaoIsencao.CancelamentoMatricula.MotivoIsencao.Value,
                    UsarioId = usuario.Id
                };
                cancelamentoIsencao = await _cancelamentoIsencaoRepository.AddAsync(cancelamentoIsencao);

                foreach (var item in autorizacaoIsencao.Pagamentos)
                {
                    DtoPagamento pagamento;
                    if (item.Id == 0)
                    {
                        cancelamento.ValorMultaCancelamento = 0;
                        pagamento = await _alunoFinanceiroContratoService.GerarMultaCancelamento(item, true);
                    }
                    else
                    {
                        pagamento = await _alunoFinanceiroContratoService.IsentarPagemento(item);
                        if(pagamento.Descricao.ToUpper().Contains("MULTA DE CANCELAMENTO"))
                        cancelamento.ValorEmAtraso -= pagamento.Valor;
                    }

                    CancelamentoIsencaoPagamento isencaoPagamento = new CancelamentoIsencaoPagamento()
                    {
                        PagamentoId = pagamento.Id,
                        CancelamentoIsencaoId = cancelamentoIsencao.Id
                    };
                    await _cancelamentoIsencaoPagamentoRepository.AddAsync(isencaoPagamento);
                }

                cancelamento.UsuarioIsencaoId = usuario.Id;
                await _cancelamentoMatriculaRepository.UpdateAsync(cancelamento);

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<DtoCancelamentoMatriculaResult> GerarMultaCancelamento(DtoCancelamentoMatriculaRequest dtoCancelamentoMatricula)
        {
            CancelamentoMatricula cancelamento = new CancelamentoMatricula();
            if (dtoCancelamentoMatricula.Id == 0)
            {
                dtoCancelamentoMatricula.StatusCancelamento = StatusCancelamento.Efetivado;
                cancelamento = await _cancelamentoMatriculaRepository.AddAsync(_mapper.Map<CancelamentoMatricula>(dtoCancelamentoMatricula));
                await CancelarMatricula(dtoCancelamentoMatricula.MatriculaAlunoId);

            }
            DtoPagamento pagamento = await _alunoFinanceiroContratoService.GerarMultaCancelamento(this.GerarPagemanto(dtoCancelamentoMatricula), false);

            return await this.BuscarPorMatricula(dtoCancelamentoMatricula.MatriculaAlunoId);
        }

        public async Task<byte[]> GerarReportByte(int matriculaId, int usuarioLogadoId, MotivoCancelamento motivoCancelamento)
        {
            try
            {
                var cancelamento = await _cancelamentoMatriculaRepository.BuscarPorMatricula(matriculaId);
                var matricula = await _matriculaAlunoRepository.BuscarPorId(matriculaId);
                var unidade = await _unidadeService.BuscarPorId(matricula.UnidadeId.Value);
                var usuario = await _usuarioRepository.BuscarPorId(usuarioLogadoId);

                string nomeReport = string.Empty;

                Report report = new Report();


                report.Load($"{_webHostingEnvironment.ContentRootPath}/App_Data/DeclaracaoCancelamento.frx");


                report.SetParameterValue("NomeFantasia", $"{unidade.NomeFantasia}");
                report.SetParameterValue("Linha1", $"{unidade.RazaoSocial}");
                report.SetParameterValue("Linha2", $"CNPJ: {CoreHelpers.FormatCNPJouCPF(unidade.CNPJ)}");

                report.SetParameterValue("Linha3", $"{unidade.Endereco.Rua}, " +
                    $"{unidade.Endereco.Numero}, " +
                    $"{unidade.Endereco.Bairro}");

                report.SetParameterValue("Linha4",
               $"{unidade.Endereco.Cidade} - {unidade.Endereco.Estado}, " +
               $"CEP: {unidade.Endereco.CEP}");

                string telefone = string.Empty;
                if (!string.IsNullOrEmpty(unidade.Contato.TelefoneFixoPrincipal) && unidade.Contato.TelefoneFixoPrincipal.Length > 8)
                {
                    string ddd = unidade.Contato.TelefoneFixoPrincipal.Substring(0, 2);
                    string fone = unidade.Contato.TelefoneFixoPrincipal.Substring(2, unidade.Contato.TelefoneFixoPrincipal.Length - 2);
                    telefone = string.Format("Tel.: ({0}) {1}", ddd, fone);
                }
                report.SetParameterValue("Linha1", $"{telefone}");




                if (matricula.Curso.NacionatalTec) report.SetParameterValue("Titulo1", "CURSOS PREPARATÓRIOS E PROFISSIONALIZANTES");
                else report.SetParameterValue("Titulo1", "CURSOS PREPARATÓRIOS PARA EXAMES SUPLETIVO");

                report.SetParameterValue("NomeAluno", matricula.Aluno.Nome);
                report.SetParameterValue("UsuarioLogado", usuario?.Funcionario?.Nome);
                report.SetParameterValue("RG", matricula.Aluno.RG);
                report.SetParameterValue("CPF", CoreHelpers.FormatCNPJouCPF(matricula.Aluno.CPF));
                report.SetParameterValue("Matricula", $"{matricula.NumeroMatricula}");
                report.SetParameterValue("MotivoCancelamento", $"{motivoCancelamento.GetDisplayName()}");

                string mes = new CultureInfo("pt-BR").DateTimeFormat.GetMonthName(DateTime.Now.Month);

                report.SetParameterValue("Data_Endereco", $"{unidade.Endereco.Cidade}, {DateTime.Now.Day} de {mes} de {DateTime.Now.Year}");


                //Get picture object from the report template
                PictureObject pic = report.FindObject("Picture1") as PictureObject;
                //Set object bounds
                pic.Bounds = new RectangleF(0, 0, Units.Centimeters * 4, Units.Centimeters * 4);

                var logo = await _unidadeService.SelecionarFoto(unidade.Id);
                //Set the image

                if (logo.Foto != null)
                {
                    using (var ms = new MemoryStream(logo.Foto))
                    {
                        pic.Image = new Bitmap(Image.FromStream(ms));
                    }
                }

                //report.SetParameterValue("Endereco2", $"{unidade.Endereco.Cidade} - {unidade.Endereco.Estado}, " + $"CEP: {unidade.Endereco.CEP}");

                //Running Report
                report.Prepare();

                byte[] arquivo = null;

                PDFSimpleExport pdfExport = new PDFSimpleExport();
                //export PDF Report form

                using (MemoryStream strm = new MemoryStream())
                {
                    report.Report.Export(pdfExport, strm);
                    report.Dispose();
                    pdfExport.Dispose();
                    strm.Position = 0;

                    arquivo = strm.ToArray();
                }

                return arquivo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DtoPagamento GerarPagemanto(DtoCancelamentoMatriculaRequest dtoCancelamentoMatricula)
        {
            return new DtoPagamento
            {
                Descricao = "Multa de cancelamento",
                TipoPagamento = Core.Model.Enums.TipoPagamentoEnum.BoletoBancario,
                NossoNumero = new Random().Next(20201190, 99991123).ToString(),
                DataEmissao = DateTime.Now,
                DataVencimento = DateTime.Now,
                Valor = dtoCancelamentoMatricula.ValorMultaCancelamento,
                Desconto = 0,
                TipoSituacao = TipoSituacaoEnum.Aberto,
                MatriculaId = dtoCancelamentoMatricula.MatriculaAlunoId,
                PromocaoBolsaConvenio = 0,
            };
        }

        public async Task<string> ValidarCancelamento(DtoCancelamentoMatriculaRequest efetuarCancelamento)
        {
            try
            {

                StringBuilder mensagem = new StringBuilder();

                var cancelamento = await BuscarPorMatricula(efetuarCancelamento.MatriculaAlunoId);


                if (!cancelamento.DentroPrazoCancelamento && !cancelamento.PagoTotal)
                {
                    int indice = 1;
                    var multa = cancelamento.ParcelasEmAtraso.Where(x => x.Descricao.Contains("Multa de cancelamento") && statusAberdo.Contains(x.TipoSituacao)).FirstOrDefault();
                    if (multa != null)
                    {
                        mensagem.Append($"<br /> {indice}- A multa de cancelamento foi gerada, favor imprimir o boleto entregar ao aluno e solicitar que realize o pagamento. <br />");
                        indice++;
                        if (multa.Id == 0)
                            await _alunoFinanceiroContratoService.GerarMultaCancelamento(multa, false);
                    }

                    if (cancelamento.ParcelasEmAtraso.Any(x => !x.Descricao.Contains("Multa de cancelamento") && statusAberdo.Contains(x.TipoSituacao)))
                        mensagem.Append($"<br /> {indice}- Existem parcelas inadimplentes, orientar o aluno a pagar via cartão ou via boleto. <br />");
                }

                await TicketEnviar(cancelamento.MatriculaAlunoId, cancelamento.UsuarioLogadoId);

                return mensagem.ToString();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task TicketEnviar(int matriculaId, int UsuarioLogadoId)
        {

            try
            {
                var matricula = await _matriculaAlunoRepository.BuscarPorId(matriculaId);
                var assunto = _assuntoTicketRepository.BuscarAuditoriaCancelamento();

                var tickets = await _ticketService.BuscarPorMatriculaId(matriculaId, assunto.Id);

                if (tickets.Count() > 0)
                    return;

                var usuarioLogado = await _usuarioService.BuscarPorId(UsuarioLogadoId);

                var usuarioLista = await _usuarioService.BuscarUsuarioAtendente();

                Dto.UnidadeVO.DtoUnidadeResponse unidade;
                if (usuarioLogado.Unidade != null)
                    unidade = await _unidadeService.BuscarPorId(usuarioLogado.Unidade.Id);
                else
                    unidade = await _unidadeService.BuscarPorId(matricula.UnidadeId.Value);

                #region criar ticket para envio

                var ticket = new Dto.TicketVO.DtoTicket
                {
                    DataAbertura = DateTime.Now,
                    Status = Core.Model.Tickets.StatusTicketEnum.Aberto,
                    AssuntoTicketId = assunto.Id,
                    UsuarioLogadoId = UsuarioLogadoId,
                    IdFuncionarioAtendente = UsuarioLogadoId,
                    MatriculaId = matricula.Id
                };

                ticket.DestinatarioTicket = new Dto.TicketVO.DtoDestinatarioTicket();

                ticket.DestinatarioTicket.UnidadeId = assunto.UnidadeId;

                ticket.DestinatarioTicket.DepartamentoId = assunto.CentroCustoId;

                string descricao = string.Format(@"Aluno: {0}{1}, 
                                               Rm: {2} {3} 
                                               Aluno realizou cancelamento do curso.Realizar a baixa no Itaú dos boletos com status 'Isento' e / ou a vencer. Verificar upload da carta de cancelamento e de atestado se houver."
                                             , matricula.Aluno.Nome
                                             , Environment.NewLine
                                             , matricula.NumeroMatricula
                                             , Environment.NewLine);

                ticket.DestinatarioTicket.Mensagem = descricao;
                ticket.DestinatarioTicket.UsuarioLogadoId = UsuarioLogadoId;
                ticket.DestinatarioTicket.StatusTicket = Core.Model.Tickets.StatusTicketEnum.Aberto;
                ticket.DataAbertura = DateTime.Now;
                ticket.NumeroProtocolo = new Random(DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + DateTime.Now.Minute).Next().ToString();

                ticket.DestinatarioTicket.UsuarioDestinarioTicket = new List<DtoUsuarioDestinarioTicket>();
                if (assunto.FuncionarioAssuntoTicket.Count > 0)
                {

                    foreach (var item in assunto.FuncionarioAssuntoTicket)
                    {
                        var usuario = await _usuarioService.FiltrarUsuario(new Dto.UsuarioVO.DtoFiltrarUsuario { FuncionarioId = item.Usuario.FuncionarioId });

                        ticket.DestinatarioTicket.UsuarioDestinarioTicket.Add(new Dto.TicketVO.DtoUsuarioDestinarioTicket
                        {
                            FuncionarioId = usuario.FirstOrDefault().Id
                        });
                    }
                }
                else
                {
                    var usuarios = await _usuarioService.FiltrarUsuario(new DtoFiltrarUsuario() { CentroCustoId = assunto.CentroCustoId, UnidadeId = assunto.UnidadeId });
                    foreach (var item in usuarios)
                    {
                        if (item.Funcionario == null) break;
                        var usuario = await _usuarioService.FiltrarUsuario(new Dto.UsuarioVO.DtoFiltrarUsuario { FuncionarioId = item.Funcionario.Id });

                        ticket.DestinatarioTicket.UsuarioDestinarioTicket.Add(new Dto.TicketVO.DtoUsuarioDestinarioTicket
                        {
                            FuncionarioId = usuario.FirstOrDefault().Id
                        });
                    }

                }

                await _ticketService.Inserir(ticket);

                #endregion
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
