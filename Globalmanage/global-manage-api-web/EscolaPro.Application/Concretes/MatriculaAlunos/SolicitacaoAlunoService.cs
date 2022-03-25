using AutoMapper;
using EscolaPro.Core.Helpers;
using EscolaPro.Core.Model.ArquivoRemessa.ArquivoSimples;
using EscolaPro.Core.Model.Enums;
using EscolaPro.Core.Model.MetasComissoes;
using EscolaPro.Core.Model.Pagamentos;
using EscolaPro.Core.Model.PainelMatricula;
using EscolaPro.Core.Model.Solicitacoes;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Interfaces.EmailEnviados;
using EscolaPro.Repository.Interfaces.Pagamentos;
using EscolaPro.Repository.Interfaces.Solicitacoes;
using EscolaPro.Repository.Interfaces.Tickets;
using EscolaPro.Service.Dto;
using EscolaPro.Service.Dto.MatriculaAlunoVO;
using EscolaPro.Service.Dto.MatriculaAlunoVO.SolicitacaoVO;
using EscolaPro.Service.Dto.PagamentosVO;
using EscolaPro.Service.Dto.TicketVO;
using EscolaPro.Service.Interfaces;
using EscolaPro.Service.Interfaces.EmailEnviados;
using EscolaPro.Service.Interfaces.MatriculaAlunos;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using FastReport;
using FastReport.Export.PdfSimple;
using FastReport.Utils;
using System.Globalization;
using System.Drawing;
using Microsoft.AspNetCore.Hosting;
using EscolaPro.Service.Interfaces.ProvasCertificados;
using EscolaPro.Repository.Interfaces.MatriculaAlunos;

namespace EscolaPro.Service.Concretes.MatriculaAlunos
{
    public class SolicitacaoAlunoService : ISolicitacaoAlunoService
    {
        private readonly ISolicitacaoService _solicitacaoService;
        private readonly ISolicitacaoAlunoRepository _solicitacaoAlunoRepository;
        private readonly IAlunoFinanceiroContratoRepository _alunoFinanceiroContratoRepository;
        private readonly IMatriculaAlunoService _matriculaAlunoService;
        private readonly ITicketService _ticketService;
        private readonly IAssuntoTicketRepository _assuntoTicketRepository;
        private readonly IAlunoService _alunoService;
        private readonly IRegistroCobrancaService _registroCobrancaService;
        private readonly IAnexoRepository _anexoRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IEmailEnviadoRepository _emailEnviadoRepository;
        private readonly IUnidadeService _unidadeService;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        private readonly IWebHostEnvironment _webHostingEnvironment;

        public SolicitacaoAlunoService(
            ISolicitacaoService solicitacaoService,
            ISolicitacaoAlunoRepository solicitacaoAlunoRepository,
            IAlunoFinanceiroContratoRepository alunoFinanceiroContratoRepository,
            IMatriculaAlunoService matriculaAlunoService,
            IRegistroCobrancaService registroCobrancaService,
            IAssuntoTicketRepository assuntoTicketRepository,
            IEmailSenderService emailSenderService,
            IEmailEnviadoRepository emailEnviadoRepository,
            IAlunoService alunoService,
            ITicketService ticketService,
            IAnexoRepository anexoRepository,
            IUsuarioService usuarioService,
            IUnidadeService unidadeService,
            IMapper mapper,
            IOptions<AppSettings> appSettings,
            IWebHostEnvironment webHostingEnvironment)
        {
            _solicitacaoService = solicitacaoService;
            _solicitacaoAlunoRepository = solicitacaoAlunoRepository;
            _alunoFinanceiroContratoRepository = alunoFinanceiroContratoRepository;
            _matriculaAlunoService = matriculaAlunoService;
            _registroCobrancaService = registroCobrancaService;
            _ticketService = ticketService;
            _alunoService = alunoService;
            _anexoRepository = anexoRepository;
            _assuntoTicketRepository = assuntoTicketRepository;
            _usuarioService = usuarioService;
            _emailSenderService = emailSenderService;
            _emailEnviadoRepository = emailEnviadoRepository;
            _unidadeService = unidadeService;
            _mapper = mapper;
            _appSettings = appSettings.Value;

            _webHostingEnvironment = webHostingEnvironment;
        }

        public async Task<IEnumerable<DtoSolicitacaoAluno>> BuscarHistorico(int matriculaId)
        {
            try
            {
                var historicoSolicitacaoAluno = await _solicitacaoAlunoRepository.BuscarHistorico(matriculaId);

                List<DtoSolicitacaoAluno> dtoSolicitacaoAlunos = new List<DtoSolicitacaoAluno>();

                foreach (var item in historicoSolicitacaoAluno)
                {
                    var anexoId = await _anexoRepository.ExisteAnexo(item.Id);

                    var solicitacaoAluno = _mapper.Map<DtoSolicitacaoAluno>(item);
                    solicitacaoAluno.AnexoId = anexoId;

                    dtoSolicitacaoAlunos.Add(solicitacaoAluno);
                }

                return dtoSolicitacaoAlunos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoSolicitacaoAluno> EfetuarSolicitacao(DtoSolicitacaoEfetuar dtoSolicitacaoEfetuar)
        {
            try
            {
                var solicitacao = await _solicitacaoService.BuscarPorId(dtoSolicitacaoEfetuar.SolicitacaoId.Value);

                var solicitacaoAluno = new SolicitacaoAluno
                {
                    SolicitacaoId = dtoSolicitacaoEfetuar.SolicitacaoId.Value,
                    MatriculaId = dtoSolicitacaoEfetuar.MatriculaId,
                    DataSolicitacao = DateTime.Now,
                };

                if (solicitacao.TipoSolicitacao == TipoSolicitacaoEnum.Gratis)
                {
                    solicitacaoAluno.StatusPagamento = StatusPagamentoEnum.Gratis;
                }
                else
                {
                    if (dtoSolicitacaoEfetuar.TEF)
                    {
                        solicitacaoAluno.StatusPagamento = StatusPagamentoEnum.Pago;
                    }
                    else
                    {
                        solicitacaoAluno.StatusPagamento = StatusPagamentoEnum.AReceber;
                    }
                }

                var solicitacaoAlunoRetorno = await Inserir(solicitacao.Id, dtoSolicitacaoEfetuar.MatriculaId, solicitacaoAluno.StatusPagamento, dtoSolicitacaoEfetuar.UsuarioLogadoId);

                if (dtoSolicitacaoEfetuar.TipoPagamento == TipoPagamentoEnum.BoletoBancario || solicitacao.TipoSolicitacao == TipoSolicitacaoEnum.Gratis)
                {
                    DtoPagamento pagamento = new DtoPagamento();

                    pagamento.Descricao = solicitacao.Descricao;
                    pagamento.MatriculaId = dtoSolicitacaoEfetuar.MatriculaId;
                    pagamento.Valor = solicitacao.Valor.HasValue ? solicitacao.Valor.Value : 0;
                    pagamento.DataEmissao = DateTime.Now;
                    pagamento.DataVencimento = DateTime.Now.AddDays(1);
                    pagamento.Desconto = 0;
                    pagamento.DescontoPontualidade = 0;
                    pagamento.SolicitacaoAlunoId = solicitacaoAlunoRetorno.Id;

                    if (solicitacao.TipoSolicitacao != TipoSolicitacaoEnum.Gratis)
                    {
                        pagamento.TipoSituacao = TipoSituacaoEnum.Aberto;

                        string nossoNumero = new Random().Next(20201190, 99991123).ToString();

                        var matricula = await _matriculaAlunoService.BuscarPorId(dtoSolicitacaoEfetuar.MatriculaId);

                        var pagamentoRetorno = await GerarBoletos(pagamento, matricula, nossoNumero);

                        List<string> emails = new List<string>();

                        List<DtoPagamento> pagamentos = new List<DtoPagamento>();

                        pagamentos.Add(_mapper.Map<DtoPagamento>(pagamentoRetorno));

                        var unidadeFoto = await _unidadeService.SelecionarFoto(matricula.UnidadeId);

                        List<Attachment> attachments = new List<Attachment>();

                        var boletoPdf = await Core.Helpers.CoreHelpers.ConverterBoletoPDF(new List<string>() { pagamentoRetorno.BoletoHTML }, _appSettings.BoletoServiceUrl);

                        Stream stream = new MemoryStream(Convert.FromBase64String(boletoPdf.FirstOrDefault()));

                        attachments.Add(new Attachment(stream, $"{pagamentoRetorno.Descricao.Replace("/", "-")}.pdf"));

                        var conteudoDoEmail = Service.Helpers.CoreHelpers.MontarConteudoEmail(pagamentos, unidadeFoto, matricula.Aluno.Nome);

                        var aluno = await _alunoService.BuscarPorId(matricula.AlunoId);

                        emails.Add(aluno.Contato.Email);

                        if (matricula.Curso.Descricao == "Alfabetização, Ensino Fundamental e Médio" ||
                            matricula.Curso.Descricao == "Ensino Fundamental e Médio" ||
                            matricula.Curso.Descricao == "Ensino Fundamental" ||
                            matricula.Curso.Descricao == "Ensino Médio")
                            await _emailSenderService.SendEmailAsync(emails.ToArray(), $"Seu boleto está disponível - {solicitacao.Descricao}", conteudoDoEmail.ToString(), attachments);
                        else
                            await _emailSenderService.SendEmailAsync(emails.ToArray(), $"Seu boleto está disponível - {solicitacao.Descricao}", conteudoDoEmail.ToString(), attachments, true);

                        await _emailEnviadoRepository.AddAsync(new EmailEnviado
                        {
                            CorpoEmail = conteudoDoEmail.ToString(),
                            DataEnvio = DateTime.Now,
                            PagamentoId = pagamentoRetorno.Id,
                            EmailPara = emails.FirstOrDefault()
                        });

                    }
                }
                else
                {
                    solicitacaoAlunoRetorno.Solicitacao = solicitacao;

                    await _alunoFinanceiroContratoRepository.InserirPagamentoSolicitacao(_mapper.Map<SolicitacaoAluno>(solicitacaoAlunoRetorno), _mapper.Map<SolicitacaoEfetuar>(dtoSolicitacaoEfetuar));
                }

                solicitacaoAlunoRetorno.Solicitacao = solicitacao;

                if (solicitacaoAlunoRetorno.Solicitacao.EnviaEmail && (solicitacaoAluno.StatusPagamento != StatusPagamentoEnum.AReceber || !solicitacaoAlunoRetorno.Solicitacao.EnviaEmailPosPgto))
                {

                    StringBuilder email = new StringBuilder();

                    var matricula = await _matriculaAlunoService.BuscarPorId(solicitacaoAlunoRetorno.MatriculaId);

                    email.Append(solicitacaoAlunoRetorno.Solicitacao.EmailConteudo);
                    email.Append("<br />");
                    email.Append("<br />");
                    email.Append($"Nome: {matricula.Aluno.Nome}");
                    email.Append("<br />");
                    email.Append($"RG: {matricula.Aluno.RG}");
                    email.Append("<br />");
                    email.Append($"CPF: {CoreHelpers.FormatCNPJouCPF(matricula.Aluno.CPF)}");
                    email.Append("<br />");

                    List<string> emails = new List<string>();

                    foreach (var item in solicitacaoAlunoRetorno.Solicitacao.EmailDestinatario)
                    {
                        emails.Add(item.Email);
                    }

                    // Não esta enviando o USUÁRIO_ID
                    //var bytePdf = await GerarReportByte(solicitacaoAlunoRetorno.Id, 3, matricula.Id);

                    List<Attachment> attachments = new List<Attachment>();

                    //Stream stream = new MemoryStream(bytePdf);

                    //attachments.Add(new Attachment(stream, $"{solicitacaoAlunoRetorno.Solicitacao.Descricao}.pdf"));

                    if (matricula.Curso.Descricao == "Alfabetização, Ensino Fundamental e Médio" ||
                        matricula.Curso.Descricao == "Ensino Fundamental e Médio" ||
                        matricula.Curso.Descricao == "Ensino Fundamental" ||
                        matricula.Curso.Descricao == "Ensino Médio")
                    {
                        await _emailSenderService.SendEmailAsync(emails.ToArray(), solicitacaoAlunoRetorno.Solicitacao.EmailTitulo, email.ToString(), attachments);
                    }
                    else
                        await _emailSenderService.SendEmailAsync(emails.ToArray(), solicitacaoAlunoRetorno.Solicitacao.EmailTitulo, email.ToString(), attachments, true);

                }
                if (solicitacao.Descricao.ToUpper().Contains("APOSTILA") && 
                    (solicitacaoAluno.StatusPagamento == StatusPagamentoEnum.Pago || 
                    solicitacaoAluno.StatusPagamento == StatusPagamentoEnum.Gratis))
                {
                    await _matriculaAlunoService.AtualizarMaterialLiberado(dtoSolicitacaoEfetuar.MatriculaId, true);
                }

                return solicitacaoAlunoRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<DtoSolicitacaoAluno> AtualizarPagamentoSolicitacao(int SolicitacaoId, StatusPagamentoEnum statusPagamento)
        {
            try
            {
                var solicitacaoAluno = await _solicitacaoAlunoRepository.BuscarPorId(SolicitacaoId);
                solicitacaoAluno.StatusPagamento = statusPagamento;


                if (solicitacaoAluno.Solicitacao.EnviaEmail || solicitacaoAluno.Solicitacao.EnviaTicket)
                {

                    DtoMatriculaAlunoResponse matricula = await _matriculaAlunoService.BuscarPorId(solicitacaoAluno.MatriculaId);
                    if (solicitacaoAluno.Solicitacao.EnviaEmail)
                    {
                        if (statusPagamento != Core.Model.MetasComissoes.StatusPagamentoEnum.AReceber)
                        {
                            matricula = await _matriculaAlunoService.BuscarPorId(solicitacaoAluno.MatriculaId);
                            await EnviarEmaildeSolicitacao(statusPagamento, _mapper.Map<DtoSolicitacao>(solicitacaoAluno.Solicitacao), matricula);
                        }
                    }

                    #region criar ticket para envio
                    if (solicitacaoAluno.Solicitacao.EnviaTicket && solicitacaoAluno.StatusPagamento != StatusPagamentoEnum.AReceber)
                    {
                        await EnviarEmaildeTicket(solicitacaoAluno, _mapper.Map<DtoSolicitacao>(solicitacaoAluno.Solicitacao), matricula);
                    }
                }
                await _solicitacaoAlunoRepository.UpdateAsync(_mapper.Map<SolicitacaoAluno>(solicitacaoAluno));

                #endregion
                return _mapper.Map<DtoSolicitacaoAluno>(solicitacaoAluno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<DtoSolicitacaoAluno> Inserir(int solicitacaoId, int matriculaId, StatusPagamentoEnum statusPagamento, int UsuarioLogadoId)
        {

            DtoSolicitacao solicitacao = await _solicitacaoService.BuscarPorId(solicitacaoId);

            var matricula = await _matriculaAlunoService.BuscarPorId(matriculaId);

            var solicitacaoAluno = new SolicitacaoAluno
            {
                SolicitacaoId = solicitacao.Id,
                MatriculaId = matriculaId,
                DataSolicitacao = DateTime.Now,
                StatusPagamento = statusPagamento,
                UsuarioLogadoId = UsuarioLogadoId,
                Valor = solicitacao.Valor.HasValue ? solicitacao.Valor.Value : 0
            };

            #region criar ticket para envio

            if (solicitacao.EnviaTicket && (solicitacaoAluno.StatusPagamento != StatusPagamentoEnum.AReceber || !solicitacao.EnviaEmailPosPgto))
            {
                await EnviarEmaildeTicket(solicitacaoAluno, solicitacao, matricula);
            }

            if (solicitacao.EnviaEmail)
            {
                await EnviarEmaildeSolicitacao(statusPagamento, solicitacao, matricula);
            }

            #endregion

            var solicitacaoAlunoRetorno = await _solicitacaoAlunoRepository.AddAsync(solicitacaoAluno);

            return _mapper.Map<DtoSolicitacaoAluno>(solicitacaoAlunoRetorno);
        }

        private async Task EnviarEmaildeSolicitacao(StatusPagamentoEnum statusPagamento, DtoSolicitacao solicitacao, DtoMatriculaAlunoResponse matricula)
        {
            if (statusPagamento != Core.Model.MetasComissoes.StatusPagamentoEnum.AReceber)
            {
                StringBuilder email = new StringBuilder();

                email.Append(solicitacao.EmailConteudo);
                email.Append("<br />");
                email.Append("<br />");
                email.Append($"Nome: {matricula.Aluno.Nome}");
                email.Append("<br />");
                email.Append($"RG: {matricula.Aluno.RG}");
                email.Append("<br />");
                email.Append($"CPF: {CoreHelpers.FormatCNPJouCPF(matricula.Aluno.CPF)}");
                email.Append("<br />");

                List<string> emails = new List<string>();

                foreach (var item in solicitacao.EmailDestinatario)
                {
                    emails.Add(item.Email);
                }

                List<Attachment> attachments = new List<Attachment>();

                if (matricula.Curso.Descricao == "Alfabetização, Ensino Fundamental e Médio" ||
                    matricula.Curso.Descricao == "Ensino Fundamental e Médio" ||
                    matricula.Curso.Descricao == "Ensino Fundamental" ||
                    matricula.Curso.Descricao == "Ensino Médio")
                {
                    await _emailSenderService.SendEmailAsync(emails.ToArray(), solicitacao.EmailTitulo, email.ToString(), attachments);
                }
                else
                    await _emailSenderService.SendEmailAsync(emails.ToArray(), solicitacao.EmailTitulo, email.ToString(), attachments, true);
            }
        }

        private async Task EnviarEmaildeTicket(SolicitacaoAluno solicitacaoAluno, DtoSolicitacao solicitacao, DtoMatriculaAlunoResponse matricula)
        {
            var assunto = await _assuntoTicketRepository.BuscarAssuntoSolicitacao();

            var ticket = new Dto.TicketVO.DtoTicket
            {
                DataAbertura = DateTime.Now,
                Status = Core.Model.Tickets.StatusTicketEnum.Aberto,
                AssuntoTicketId = assunto.Id,
                UsuarioLogadoId = solicitacaoAluno.UsuarioLogadoId,
                IdFuncionarioAtendente = solicitacaoAluno.UsuarioLogadoId,
                MatriculaId = matricula.Id
            };

            ticket.DestinatarioTicket = new Dto.TicketVO.DtoDestinatarioTicket();

            if (solicitacao.UnidadeId.HasValue)
            {
                ticket.DestinatarioTicket.UnidadeId = solicitacao.UnidadeId;

                if (solicitacao.CentroCustoId.HasValue)
                {
                    ticket.DestinatarioTicket.DepartamentoId = solicitacao.CentroCustoId;
                }

                ticket.DestinatarioTicket.Mensagem = $"{solicitacao.Descricao}\n\n" +
                    $"Aluno(a): {matricula.Aluno.Nome} \n" +
                    $"CPF: { CoreHelpers.FormatCNPJouCPF(matricula.Aluno.CPF)}\n" +
                    $"RG: {matricula.Aluno.RG}\n" +
                    $"Matrícula: {matricula.NumeroMatricula} \n" +
                    $"Curso: {matricula.Curso.Descricao}\n";

                ticket.DestinatarioTicket.UsuarioLogadoId = solicitacaoAluno.UsuarioLogadoId;
                ticket.DestinatarioTicket.StatusTicket = Core.Model.Tickets.StatusTicketEnum.Aberto;
                ticket.DataAbertura = DateTime.Now;
                ticket.NumeroProtocolo = new Random(DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + DateTime.Now.Minute).Next().ToString();

                if (solicitacao.SolicitacaoFuncionarioTicket.Count > 0)
                {
                    ticket.DestinatarioTicket.UsuarioDestinarioTicket = new List<DtoUsuarioDestinarioTicket>();

                    foreach (var item in solicitacao.SolicitacaoFuncionarioTicket)
                    {
                        var usuario = await _usuarioService.FiltrarUsuario(new Dto.UsuarioVO.DtoFiltrarUsuario { FuncionarioId = item.FuncionarioId });

                        ticket.DestinatarioTicket.UsuarioDestinarioTicket.Add(new Dto.TicketVO.DtoUsuarioDestinarioTicket
                        {
                            FuncionarioId = usuario.FirstOrDefault().Id
                        });
                    }
                }
            }

            await _ticketService.Inserir(ticket);
        }

        public async Task<Pagamento> GerarBoletos(DtoPagamento dtoPagamento, DtoMatriculaAlunoResponse matriculaAluno, string nossoNumero)
        {
            try
            {
                var simplesCorpoCobranca = await EnviarBoletoItau(dtoPagamento, matriculaAluno, nossoNumero);

                if (dtoPagamento.Valor > 0)
                {
                    var retorno = await _registroCobrancaService.GerarBoleto(simplesCorpoCobranca);

                    retorno.data_vencimento = simplesCorpoCobranca.data_vencimento;
                    retorno.valor_cobrado = simplesCorpoCobranca.valor_cobrado;

                    if (!string.IsNullOrEmpty(retorno.numero_linha_digitavel))
                    {
                        var pagamento = _mapper.Map<Pagamento>(dtoPagamento);

                        if (dtoPagamento.Id > 0)
                        {
                            pagamento.Id = dtoPagamento.Id;
                        }
                        else
                        {
                            pagamento.Id = 0;
                        }

                        pagamento.Descricao = dtoPagamento.Descricao;
                        pagamento.DataEmissao = DateTime.Now;
                        pagamento.NossoNumero = retorno.nosso_numero.Substring(0, retorno.nosso_numero.Length - 1);
                        pagamento.CodigoBarras = retorno.codigo_barras;
                        pagamento.Valor = dtoPagamento.Valor;
                        pagamento.PromocaoBolsaConvenio = dtoPagamento.PromocaoBolsaConvenio;
                        pagamento.NumeroLinhaDigitavel = retorno.numero_linha_digitavel;
                        pagamento.TipoPagamento = Core.Model.Enums.TipoPagamentoEnum.BoletoBancario;
                        pagamento.MatriculaId = matriculaAluno.Id;
                        pagamento.NumeroRegistro = dtoPagamento.NumeroRegistro;
                        pagamento.TipoSituacao = TipoSituacaoEnum.Aberto;

                        var boletoPDF = await _registroCobrancaService.BoletoImpressoPdf(retorno, matriculaAluno, dtoPagamento.Descricao, pagamento);

                        pagamento.BoletoHTML = boletoPDF;

                        if (pagamento.Id > 0)
                        {
                            await _alunoFinanceiroContratoRepository.UpdateAsync(pagamento);

                            return await _alunoFinanceiroContratoRepository.BuscarPorId(pagamento.Id);
                        }
                        else
                        {
                            return await _alunoFinanceiroContratoRepository.AddAsync(pagamento);
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    var pagamento = _mapper.Map<Pagamento>(dtoPagamento);

                    pagamento.Descricao = dtoPagamento.Descricao;
                    pagamento.DataEmissao = null;
                    pagamento.NossoNumero = "";
                    pagamento.CodigoBarras = "";
                    pagamento.Valor = dtoPagamento.Valor;
                    pagamento.PromocaoBolsaConvenio = dtoPagamento.PromocaoBolsaConvenio;
                    pagamento.NumeroLinhaDigitavel = "";
                    pagamento.TipoPagamento = Core.Model.Enums.TipoPagamentoEnum.BoletoBancario;
                    pagamento.MatriculaId = matriculaAluno.Id;
                    pagamento.DataVencimento = null;

                    if (pagamento.Id > 0)
                    {
                        await _alunoFinanceiroContratoRepository.UpdateAsync(pagamento);

                        return await _alunoFinanceiroContratoRepository.BuscarPorId(pagamento.Id);
                    }
                    else
                    {
                        return await _alunoFinanceiroContratoRepository.AddAsync(pagamento);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ItauSimplesCorpoCobranca> EnviarBoletoItau(DtoPagamento dtoPagamento, DtoMatriculaAlunoResponse matriculaAluno, string nossoNumero)
        {
            try
            {
                int nossoNumeroNovo = int.Parse(nossoNumero) + 1;

                var dac = Core.Helpers.CoreHelpers.GerarDAC(nossoNumeroNovo.ToString());

                var aluno = await _alunoService.BuscarPorId(matriculaAluno.AlunoId);

                string valor = dtoPagamento.Valor.ToString("N2").Replace(",", "").Replace(".", "");

                var simplesCorpoCobranca = new Core.Model.ArquivoRemessa.ArquivoSimples.ItauSimplesCorpoCobranca
                {
                    tipo_ambiente = _appSettings.HomologItau ? 1 : 2,
                    tipo_registro = 1,
                    tipo_cobranca = 1,
                    seu_numero = dtoPagamento.NumeroRegistro,
                    tipo_produto = "00006",
                    subproduto = "00008",
                    titulo_aceite = "S",
                    tipo_carteira_titulo = "109",
                    nosso_numero = nossoNumeroNovo.ToString(),
                    digito_verificador_nosso_numero = dac,
                    data_vencimento = dtoPagamento.DataVencimento.HasValue ? dtoPagamento.DataVencimento.Value.ToString("yyyy-MM-dd") : "", // Data de vencimento da parcela
                    valor_cobrado = Core.Helpers.CoreHelpers.ComplementarZeroEsquerda(valor),
                    especie = "04",
                    data_emissao = DateTime.Now.ToString("yyyy-MM-dd"), // data de emissão do boleto
                    tipo_pagamento = 3,
                    indicador_pagamento_parcial = "false",
                    beneficiario = new Core.Model.ArquivoRemessa.ItauBeneficiario
                    {
                        agencia_beneficiario = "0940",
                        cpf_cnpj_beneficiario = "09435113000194",
                        conta_beneficiario = "0014369",
                        digito_verificador_conta_beneficiario = "6",
                    },
                    pagador = new Core.Model.ArquivoRemessa.ArquivoSimples.ItauSimplesPagador
                    {
                        cpf_cnpj_pagador = aluno.CPF,
                        nome_pagador = aluno.Nome.Length < 30 ? aluno.Nome : aluno.Nome.Substring(0, 30),
                        logradouro_pagador = aluno.Endereco.Rua,
                        cidade_pagador = aluno.Endereco.Cidade.Length < 15 ? aluno.Endereco.Cidade : aluno.Endereco.Cidade.Substring(0, 15),
                        uf_pagador = aluno.Endereco.Estado,
                        cep_pagador = aluno.Endereco.CEP,
                        grupo_email_pagador = new List<Core.Model.ArquivoRemessa.ItauGrupoEmailPagador>
                        {
                            new Core.Model.ArquivoRemessa.ItauGrupoEmailPagador
                            {
                                email_pagador = aluno.Contato.Email
                            }
                        }
                    },
                    moeda = new Core.Model.ArquivoRemessa.ArquivoSimples.ItauSimplesMoeda
                    {
                        codigo_moeda_cnab = "09"
                    },
                    juros = new Core.Model.ArquivoRemessa.ArquivoSimples.ItauSimplesJuros
                    {
                        tipo_juros = 5
                    },
                    multa = new Core.Model.ArquivoRemessa.ArquivoSimples.ItauSimplesMulta
                    {
                        tipo_multa = 3
                    },
                    recebimento_divergente = new Core.Model.ArquivoRemessa.ArquivoSimples.ItauSimplesRecebimentoDivergente
                    {
                        tipo_autorizacao_recebimento = "1"
                    },
                };

                if (dtoPagamento.Desconto.HasValue)
                {
                    if (dtoPagamento.Desconto.HasValue)
                    {
                        if (dtoPagamento.Desconto > 0)
                        {
                            var descontoBoleto = (dtoPagamento.Valor * dtoPagamento.Desconto) / 100;

                            var descontoValorFormatado = descontoBoleto.Value.ToString("N2");

                            simplesCorpoCobranca.grupo_desconto = new List<Core.Model.ArquivoRemessa.ArquivoSimples.ItauSimplesGrupoDesconto>
                        {
                            new Core.Model.ArquivoRemessa.ArquivoSimples.ItauSimplesGrupoDesconto
                            {
                                tipo_desconto = 1,
                                data_desconto = dtoPagamento.DataVencimento.Value.ToString("yyyy-MM-dd"),
                                percentual_desconto = dtoPagamento.Desconto.Value.ToString().Replace(",00", ""),
                                valor_desconto = Core.Helpers.CoreHelpers.ComplementarZeroEsquerda(descontoValorFormatado.Replace(",", "").Replace(".",""))
                            }
                        };
                        }
                        else
                        {
                            simplesCorpoCobranca.grupo_desconto = new List<Core.Model.ArquivoRemessa.ArquivoSimples.ItauSimplesGrupoDesconto>
                            {
                                new Core.Model.ArquivoRemessa.ArquivoSimples.ItauSimplesGrupoDesconto
                                {
                                    tipo_desconto = 0,
                                }
                            };
                        }
                    }
                }

                return simplesCorpoCobranca;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoSolicitacaoAluno> BuscarPorId(int solicitacaoId)
        {
            try
            {
                var solicitacaoAluno = await _solicitacaoAlunoRepository.BuscarPorId(solicitacaoId);

                return _mapper.Map<DtoSolicitacaoAluno>(solicitacaoAluno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ConsultarPendencia(int matriculaId)
        {
            try
            {
                var solicitacaoAlunos = await _solicitacaoAlunoRepository.BuscarHistorico(matriculaId);

                return solicitacaoAlunos.Where(x => x.Solicitacao.IsAnexo).Count() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<byte[]> GerarReportByte(int solicitacaoId, int usuarioLogadoId, int matriculaId)
        {
            try
            {
                var solicitacao = await BuscarPorId(solicitacaoId);

                var matricula = await _matriculaAlunoService.BuscarPorId(matriculaId);

                var unidade = await _unidadeService.BuscarPorId(matricula.UnidadeId);

                var aluno = await _alunoService.BuscarPorId(matricula.AlunoId);

                var usuarioLogado = await _usuarioService.BuscarPorId(usuarioLogadoId);

                string nomeReport = string.Empty;

                Report report = new Report();

                if (!matricula.Curso.NacionatalTec)
                {
                    switch (solicitacao.Solicitacao.Descricao)
                    {
                        case "Pedido de Apostila Supletivo":
                            nomeReport = "DeclaracaoApostilaEja";
                            break;
                        case "Pedido de Apostila Supletivo Grátis":
                            nomeReport = "DeclaracaoApostilaEja";
                            break;
                        case "Declaração de Cursando Eja":
                            nomeReport = "DeclaracaoCursandoEja";
                            break;
                        case "Declaração de Comparecimento na Prova":
                            nomeReport = "DeclaracaoRealizacaoProvas";
                            break;
                    }
                }
                else
                {
                    switch (solicitacao.Solicitacao.Descricao)
                    {
                        case "Pedido de Apostila Supletivo - NacionalTec":
                            nomeReport = "DeclaracaoApostilaNacionalTec";
                            break;
                        case "Declaração de Cursando - NacionalTec":
                            nomeReport = "DeclaracaoCursandoNacionalTec";
                            break;
                        case "Declaração de Comparecimento na Prova - NacionalTec":
                            nomeReport = "DeclaracaoRealizacaoProvasNacionalTec";
                            break;
                    }
                }

                if (string.IsNullOrEmpty(nomeReport))
                    return null;

                report.Load($"{_webHostingEnvironment.ContentRootPath}/App_Data/{nomeReport}.frx");

                if (matricula.Curso.NacionatalTec)
                {
                    report.SetParameterValue("NomeCurso", $"{matricula.Curso.Descricao}");
                }

                if (nomeReport == "DeclaracaoCursandoEja")
                {
                    if (matricula.Curso.Descricao.ToUpper().Contains("MÉDIO"))
                    {
                        report.SetParameterValue("NomeCurso", $"médio");
                    }
                    else
                    {
                        report.SetParameterValue("NomeCurso", $"fundamental");
                    }
                }

                if (nomeReport == "DeclaracaoRealizacaoProvas" || nomeReport == "DeclaracaoRealizacaoProvasNacionalTec")
                {
                    report.SetParameterValue("NomeCurso", $"provas presenciais no dia {DateTime.Now.ToString("dd/MM/yyyy")}, " +
                        $"estando presente neste compromisso no horário das 08:00Hrs. até 18:00Hrs. ");
                }
                if (!matricula.Curso.NacionatalTec)
                {
                    report.SetParameterValue("Linha1", $"{unidade.RazaoSocial}");
                    report.SetParameterValue("Linha2", $"CNPJ: {CoreHelpers.FormatCNPJouCPF(unidade.CNPJ)}");

                    report.SetParameterValue("Linha3", $"{unidade.Endereco.Rua}, " +
                        $"{unidade.Endereco.Numero}, " +
                        $"{unidade.Endereco.Bairro}");

                    report.SetParameterValue("Linha4",
                   $"{unidade.Endereco.Cidade} - {unidade.Endereco.Estado}, " +
                   $"CEP: {unidade.Endereco.CEP}");
                }
                else
                {
                    string telefone = string.Empty;
                    if (!string.IsNullOrEmpty(unidade.Contato.TelefoneFixoPrincipal) && unidade.Contato.TelefoneFixoPrincipal.Length > 8)
                    {
                        string ddd = unidade.Contato.TelefoneFixoPrincipal.Substring(0, 2);
                        string fone = unidade.Contato.TelefoneFixoPrincipal.Substring(2, unidade.Contato.TelefoneFixoPrincipal.Length - 2);
                        telefone = string.Format("Tel.: ({0}) {1}", ddd, fone);
                    }
                    report.SetParameterValue("Linha1", $"{telefone}");
                }
                report.SetParameterValue("NomeAluno", aluno.Nome);
                report.SetParameterValue("RG", aluno.RG);
                report.SetParameterValue("CPF", CoreHelpers.FormatCNPJouCPF(aluno.CPF));

                string mes = new CultureInfo("pt-BR").DateTimeFormat.GetMonthName(DateTime.Now.Month);

                report.SetParameterValue("Data_Endereco", $"{unidade.Endereco.Cidade}, {DateTime.Now.Day} de {mes} de {DateTime.Now.Year}");

                if (usuarioLogado.Funcionario != null)
                {
                    report.SetParameterValue("usuarioLogado", $"{usuarioLogado.Funcionario.Nome}");
                }
                else
                {
                    report.SetParameterValue("usuarioLogado", $"Usuário do Sistema: {usuarioLogado.UserName}");
                }

                if (solicitacao == null)
                    return null;

                var usuario = await _usuarioService.BuscarPorId(usuarioLogadoId);

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
    }
}
