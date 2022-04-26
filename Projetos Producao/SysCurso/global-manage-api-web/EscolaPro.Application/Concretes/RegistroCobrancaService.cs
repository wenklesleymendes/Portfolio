using AutoMapper;
using BoletoNetCore;
using EscolaPro.ControleBoleto;
using EscolaPro.ControleBoleto.BoletoImpressao;
using EscolaPro.Core.Helpers;
using EscolaPro.Core.Model.ArquivoRemessa;
using EscolaPro.Core.Model.ArquivoRemessa.ArquivoSimples;
using EscolaPro.Core.Model.MetasComissoes;
using EscolaPro.Core.Model.Pagamentos;
using EscolaPro.Repository.Interfaces.Pagamentos;
using EscolaPro.Repository.Interfaces.Solicitacoes;
using EscolaPro.Repository.Interfaces.Tickets;
using EscolaPro.Service.Dto.MatriculaAlunoVO;
using EscolaPro.Service.Dto.PagamentosVO;
using EscolaPro.Service.Dto.RegistroCobrancaVO;
using EscolaPro.Service.Dto.TicketVO;
using EscolaPro.Service.Interfaces;
using EscolaPro.Service.Interfaces.MatriculaAlunos;
using EscolaPro.Service.Interfaces.Pagamentos;
using FastReport;
using FastReport.Export.PdfSimple;
using FastReport.Utils;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

namespace EscolaPro.Service.Concretes
{
    public class RegistroCobrancaService : IRegistroCobrancaService
    {
        private readonly IBoletoManager _boletoManager;
        private readonly IAuthItauService _authItauService;
        private readonly IAlunoService _alunoService;
        private readonly IAlunoFinanceiroContratoRepository _alunoFinanceiroContratoRepository;
        private readonly IUnidadeService _unidadeService;
        private readonly IUsuarioService _usuarioService;
        private readonly ITicketService _ticketService;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IMatriculaAlunoService _matriculaAlunoService;
        private readonly ISolicitacaoAlunoRepository _solicitacaoAlunoRepository;
        private readonly IAssuntoTicketRepository _assuntoTicketRepository;
        private readonly IWebHostEnvironment _webHostingEnvironment;
        private readonly IMapper _mapper;

        public RegistroCobrancaService(
            IBoletoManager boletoManager,
            IAuthItauService authItauService,
            IAlunoService alunoService,
            IUnidadeService unidadeService,
            IUsuarioService usuarioService,
            ITicketService ticketService,
            IWebHostEnvironment webHostingEnvironment,
            IEmailSenderService emailSenderService,
            ISolicitacaoAlunoRepository solicitacaoAlunoRepository,
            IAssuntoTicketRepository assuntoTicketRepository,
            IMatriculaAlunoService matriculaAlunoService,
            IAlunoFinanceiroContratoRepository alunoFinanceiroContratoRepository,
            IMapper mapper)
        {
            _boletoManager = boletoManager;
            _authItauService = authItauService;
            _alunoService = alunoService;
            _ticketService = ticketService;
            _unidadeService = unidadeService;
            _webHostingEnvironment = webHostingEnvironment;
            _emailSenderService = emailSenderService;
            _assuntoTicketRepository = assuntoTicketRepository;
            _matriculaAlunoService = matriculaAlunoService;
            _solicitacaoAlunoRepository = solicitacaoAlunoRepository;
            _alunoFinanceiroContratoRepository = alunoFinanceiroContratoRepository;
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        public async Task<DtoCorpoCobranca> GerarBoleto(ItauSimplesCorpoCobranca dtoCorpoCobranca)
        {
            try
            {
                var response = await _authItauService.AutenticarItau();

                var simplesCorpoCobranca = await _boletoManager.RegistrarBoletoItau(response.access_token, dtoCorpoCobranca);

                return JsonConvert.DeserializeObject<DtoCorpoCobranca>(simplesCorpoCobranca);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> BoletoImpressoPdf(DtoCorpoCobranca dtoCorpoCobranca, DtoMatriculaAlunoResponse matriculaAluno, string descricaoParcela, Pagamento dtoPagamento)
        {
            try
            {
                var _banco = Banco.Instancia(Bancos.Itau);

                var aluno = await _alunoService.BuscarPorId(matriculaAluno.AlunoId);

                var unidade = await _unidadeService.BuscarPorId(aluno.UnidadeId, false);

                var contaBancaria = new ContaBancaria
                {
                    Agencia = "0940",
                    Conta = "14369",
                    DigitoConta = "6",
                    CarteiraPadrao = "109"
                };

                Beneficiario beneficiario = new Beneficiario
                {
                    CPFCNPJ = CoreHelpers.FormatCNPJouCPF(dtoCorpoCobranca.beneficiario.cpf_cnpj_beneficiario),
                    Nome = dtoCorpoCobranca.beneficiario.nome_razao_social_beneficiario,
                    MostrarCNPJnoBoleto = true,
                    Endereco = new Endereco
                    {
                        LogradouroEndereco = dtoCorpoCobranca.beneficiario.logradouro_beneficiario,
                        LogradouroComplemento = dtoCorpoCobranca.beneficiario.complemento_beneficiario,
                        Bairro = dtoCorpoCobranca.beneficiario.bairro_beneficiario,
                        Cidade = dtoCorpoCobranca.beneficiario.cidade_beneficiario,
                        UF = dtoCorpoCobranca.beneficiario.uf_beneficiario,
                        CEP = dtoCorpoCobranca.beneficiario.cep_beneficiario
                    },
                };

                beneficiario.ContaBancaria = contaBancaria;

                _banco.Beneficiario = beneficiario;

                _banco.FormataBeneficiario();

                var Endereco = new Endereco
                {
                    LogradouroEndereco = aluno.Endereco.Rua,
                    LogradouroNumero = aluno.Endereco.Numero,
                    LogradouroComplemento = aluno.Endereco.Complemento,
                    Bairro = aluno.Endereco.Bairro,
                    Cidade = aluno.Endereco.Cidade,
                    UF = aluno.Endereco.Estado,
                    CEP = aluno.Endereco.CEP
                };

                var pagador = new Pagador
                {
                    CPFCNPJ = CoreHelpers.FormatCNPJouCPF(aluno.CPF),
                    Nome = aluno.Nome.Length < 30 ? aluno.Nome : aluno.Nome.Substring(0, 30),
                    Endereco = Endereco
                };

                var valorCalculadoDesconto = (dtoPagamento.Valor * dtoPagamento.Desconto) / 100;

                Boleto boleto = new Boleto(_banco)
                {
                    Pagador = pagador,
                    DataEmissao = dtoPagamento.DataEmissao.Value, //DateTime.ParseExact(dtoCorpoCobranca.data_emissao, "yyyy-MM-dd", null),
                    DataVencimento = dtoPagamento.DataVencimento.Value, //DateTime.ParseExact(dtoCorpoCobranca.data_vencimento, "yyyy-MM-dd", null),
                    NossoNumero = dtoPagamento.NossoNumero, //dtoCorpoCobranca.nosso_numero.Substring(2, 6),
                    NumeroDocumento = dtoPagamento.NumeroRegistro,
                    ValorTitulo = dtoPagamento.Valor, //Convert.ToDecimal(dtoCorpoCobranca.valor_cobrado),
                    ValorDesconto = valorCalculadoDesconto.HasValue ? valorCalculadoDesconto.Value : 0,
                };

                boleto.CodigoBarra.LinhaDigitavel = dtoCorpoCobranca.numero_linha_digitavel;

                //Mensagem - Instruções do Caixa
                StringBuilder msgCaixa = new StringBuilder();
                //if (boleto.ValorDesconto > 0)
                if (valorCalculadoDesconto.HasValue)
                {
                    if (valorCalculadoDesconto.Value > 0)
                    {
                        msgCaixa.AppendLine($"CONCEDER DESCONTO DE {valorCalculadoDesconto.Value.ToString("R$ ##,##0.00")} até {dtoPagamento.DataVencimento.Value.ToString("dd/MM/yyyy")}. <br /> ");
                    }
                }
                //if (boleto.ValorMulta > 0)
                msgCaixa.AppendLine($"{unidade.NomeFantasia} - SUPLETIVO RÁPIDO<br />");
                ////if (boleto.ValorJurosDia > 0)
                msgCaixa.AppendLine($"CERTIFICADO ESCOLAR, PATRIMÔNIO DO TRABALHADOR <br />");

                msgCaixa.AppendLine($"{descricaoParcela.ToUpper()} <br />");

                boleto.ValidarDados();

                var html = new StringBuilder();

                var boletoParaImpressao = new BoletoBancario
                {
                    Boleto = boleto,
                    MostrarComprovanteEntrega = false,
                    MostrarEnderecoBeneficiario = true,
                    ExibirDemonstrativo = true,
                    OcultarInstrucoes = false,
                    OcultarReciboPagador = false,
                    OcultarEnderecoPagador = false
                };

                html.Append("<div style=\"page-break-after: always;\">");
                html.Append(boletoParaImpressao.MontaHtmlEmbedded());
                html.Append("</div>");

                var boletoHtml = html.ToString().Replace("Instruções", msgCaixa.ToString()).Replace("(Texto de responsabilidade do beneficiário)", "");

                return boletoHtml;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UploadArquivoRemessa(List<DtoBoleto> boletos)
        {
            foreach (var boleto in boletos)
            {
                // Os boletos vem sem hora, como o servidor está em UTC temos que adicionar essa correção
                boleto.DataCredito = boleto.DataCredito;

                var pagamento = await _alunoFinanceiroContratoRepository.BuscarPorNossoNumero(boleto.NossoNumero);

                if (pagamento != null && pagamento.TipoPagamento == Core.Model.Enums.TipoPagamentoEnum.BoletoBancario)
                {
                    if (boleto.ValorPagoCredito > 0)
                    {
                        if (boleto.DescricaoMovimentoRetorno == "Liquidação normal")
                        {
                            var localDataVencimento = pagamento.DataVencimento.Value;

                            if (localDataVencimento.DayOfWeek == DayOfWeek.Saturday)
                                localDataVencimento = localDataVencimento.AddDays(2);

                            if (localDataVencimento.DayOfWeek == DayOfWeek.Sunday)
                                localDataVencimento = localDataVencimento.AddDays(1);

                            if (boleto.DataCredito > localDataVencimento)
                            {
                                // Verificar o valor pago
                                if (boleto.ValorPagoCredito == (pagamento.Valor - boleto.ValorTarifas))
                                {
                                    await AtualizarPagamento(pagamento, boleto);

                                    if (pagamento.SolicitacaoAlunoId > 0)
                                        await this.AtualizarPagamentoSolicitacao(pagamento.SolicitacaoAlunoId.Value, Core.Model.MetasComissoes.StatusPagamentoEnum.Pago);
                                }
                                else
                                {
                                    await AtualizarPagamento(pagamento, boleto);
                                    await GerarValorRedisual(pagamento, boleto);
                                }
                            }
                            else
                            {
                                var valorComDesconto = (pagamento.Valor - boleto.ValorTarifas) - boleto.ValorDesconto;

                                var valorAPagar = pagamento.Valor - ((pagamento.Valor * pagamento.Desconto) / 100);

                                valorAPagar = valorAPagar - boleto.ValorTarifas;

                                if (boleto.ValorPagoCredito >= valorAPagar)
                                {
                                    await AtualizarPagamento(pagamento, boleto);

                                    if (pagamento.SolicitacaoAlunoId > 0)
                                        await this.AtualizarPagamentoSolicitacao(pagamento.SolicitacaoAlunoId.Value, Core.Model.MetasComissoes.StatusPagamentoEnum.Pago);
                                }
                                else
                                {
                                    await AtualizarPagamento(pagamento, boleto);
                                    await GerarValorRedisual(pagamento, boleto);
                                }
                            }
                        }

                    }
                }
            }
        }

        public async Task AtualizarPagamento(Pagamento pagamento, DtoBoleto boleto)
        {
            try
            {
                pagamento.DataPagamento = boleto.DataCredito;
                pagamento.ValorPago = boleto.ValorPagoCredito;
                pagamento.TipoSituacao = TipoSituacaoEnum.Pago;
                pagamento.TarifaBanco = boleto.ValorTarifas;

                await _alunoFinanceiroContratoRepository.UpdateAsync(pagamento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task GerarValorRedisual(Pagamento pagamento, DtoBoleto boleto)
        {
            try
            {
                var valorRedisual = (pagamento.Valor - boleto.ValorTarifas) - boleto.ValorPagoCredito;

                var novoPagamento = pagamento;

                novoPagamento.PagamentoIdOld = pagamento.PagamentoIdOld.HasValue ? pagamento.PagamentoIdOld : pagamento.Id;
                novoPagamento.Descricao = pagamento.PagamentoIdOld.HasValue ? $"{pagamento.Descricao}" : $"Residual - {pagamento.Descricao}";

                novoPagamento.Id = 0;
                novoPagamento.TipoSituacao = TipoSituacaoEnum.Residual;
                novoPagamento.Valor = valorRedisual;
                novoPagamento.ValorPago = 0;
                novoPagamento.DataEmissao = DateTime.Now;
                novoPagamento.DataPagamento = null;
                novoPagamento.DataVencimento = null;
                novoPagamento.NossoNumero = string.Empty;
                novoPagamento.Desconto = 0;
                novoPagamento.SolicitacaoAlunoId = pagamento.SolicitacaoAlunoId;

                await _alunoFinanceiroContratoRepository.AddAsync(pagamento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoSolicitacaoAluno> AtualizarPagamentoSolicitacao(int solicitacaoId, StatusPagamentoEnum statusPagamento)
        {
            try
            {
                var solicitacaoAluno = await _solicitacaoAlunoRepository.BuscarPorId(solicitacaoId);

                solicitacaoAluno.StatusPagamento = statusPagamento;




                if (solicitacaoAluno.Solicitacao.EnviaEmail)
                {
                    if (statusPagamento != Core.Model.MetasComissoes.StatusPagamentoEnum.AReceber)
                    {
                        StringBuilder email = new StringBuilder();

                        var matricula = await _matriculaAlunoService.BuscarPorId(solicitacaoAluno.MatriculaId);

                        email.Append(solicitacaoAluno.Solicitacao.EmailConteudo);
                        email.Append("<br />");
                        email.Append("<br />");
                        email.Append($"Nome: {matricula.Aluno.Nome}");
                        email.Append("<br />");
                        email.Append($"RG: {matricula.Aluno.RG}");
                        email.Append("<br />");
                        email.Append($"CPF: {CoreHelpers.FormatCNPJouCPF(matricula.Aluno.CPF)}");
                        email.Append("<br />");

                        List<string> emails = new List<string>();

                        foreach (var item in solicitacaoAluno.Solicitacao.EmailDestinatario)
                        {
                            emails.Add(item.Email);
                        }

                        List<Attachment> attachments = new List<Attachment>();

                        if (matricula.Curso.Descricao == "Alfabetização, Ensino Fundamental e Médio" ||
                            matricula.Curso.Descricao == "Ensino Fundamental e Médio" ||
                            matricula.Curso.Descricao == "Ensino Fundamental" ||
                            matricula.Curso.Descricao == "Ensino Médio")
                        {
                            await _emailSenderService.SendEmailAsync(emails.ToArray(), solicitacaoAluno.Solicitacao.EmailTitulo, email.ToString(), attachments);
                        }
                        else
                            await _emailSenderService.SendEmailAsync(emails.ToArray(), solicitacaoAluno.Solicitacao.EmailTitulo, email.ToString(), attachments, true);
                    }
                }

                #region criar ticket para envio

                if (solicitacaoAluno.Solicitacao.EnviaTicket && solicitacaoAluno.StatusPagamento != StatusPagamentoEnum.AReceber)
                {
                    var assunto = await _assuntoTicketRepository.BuscarAssuntoSolicitacao();
                    var matricula = await _matriculaAlunoService.BuscarPorId(solicitacaoAluno.MatriculaId);

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

                    if (solicitacaoAluno.Solicitacao.UnidadeId.HasValue)
                    {
                        ticket.DestinatarioTicket.UnidadeId = solicitacaoAluno.Solicitacao.UnidadeId;

                        if (solicitacaoAluno.Solicitacao.CentroCustoId.HasValue)
                        {
                            ticket.DestinatarioTicket.DepartamentoId = solicitacaoAluno.Solicitacao.CentroCustoId;
                        }

                        ticket.DestinatarioTicket.Mensagem = $"{solicitacaoAluno.Solicitacao.Descricao}\n\n" +
                            $"Aluno(a): {matricula.Aluno.Nome} \n" +
                            $"CPF: { CoreHelpers.FormatCNPJouCPF(matricula.Aluno.CPF)}\n" +
                            $"RG: {matricula.Aluno.RG}\n" +
                            $"Matrícula: {matricula.NumeroMatricula} \n" +
                            $"Curso: {matricula.Curso.Descricao}\n";

                        ticket.DestinatarioTicket.UsuarioLogadoId = solicitacaoAluno.UsuarioLogadoId;
                        ticket.DestinatarioTicket.StatusTicket = Core.Model.Tickets.StatusTicketEnum.Aberto;
                        ticket.DataAbertura = DateTime.Now;
                        ticket.NumeroProtocolo = new Random(DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + DateTime.Now.Minute).Next().ToString();

                        if (solicitacaoAluno.Solicitacao.SolicitacaoFuncionarioTicket.Count > 0)
                        {
                            ticket.DestinatarioTicket.UsuarioDestinarioTicket = new List<DtoUsuarioDestinarioTicket>();

                            foreach (var item in solicitacaoAluno.Solicitacao.SolicitacaoFuncionarioTicket)
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

                #endregion
                return _mapper.Map<DtoSolicitacaoAluno>(solicitacaoAluno);
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
                var solicitacao = await _solicitacaoAlunoRepository.BuscarPorId(solicitacaoId);

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

                report.Load($"{_webHostingEnvironment.ContentRootPath}/App_Data/{nomeReport}.frx");

                if (!matricula.Curso.NacionatalTec)
                {
                    report.SetParameterValue("NomeCurso", $"{matricula.Curso.Descricao}");
                }

                if (nomeReport == "DeclaracaoCursandoEja")
                {
                    if (matricula.Turma.Presencial)
                    {
                        if (matricula.Curso.NacionatalTec)
                        {
                            if (!matricula.Curso.Descricao.Contains("Ensino Fundamental"))
                            {
                                report.SetParameterValue("NomeCurso", $"médio");
                            }
                            else
                            {
                                report.SetParameterValue("NomeCurso", $"fundamental");
                            }
                        }
                    }
                }

                if (nomeReport == "DeclaracaoRealizacaoProvas" || nomeReport == "DeclaracaoRealizacaoProvasNacionalTec")
                {
                    report.SetParameterValue("NomeCurso", $"provas presenciais no dia {DateTime.Now.ToString("dd/MM/yyyy")}, " +
                        $"estando presente neste compromisso no horário das 08:00Hrs. até 18:00Hrs. ");
                }

                report.SetParameterValue("Linha1", $"{unidade.RazaoSocial}");
                report.SetParameterValue("Linha2", $"CNPJ: {CoreHelpers.FormatCNPJouCPF(unidade.CNPJ)}");

                report.SetParameterValue("Linha3", $"{unidade.Endereco.Rua}, " +
                    $"{unidade.Endereco.Numero}, " +
                    $"{unidade.Endereco.Bairro}");

                report.SetParameterValue("Linha4",
               $"{unidade.Endereco.Cidade} - {unidade.Endereco.Estado}, " +
               $"CEP: {unidade.Endereco.CEP}");

                report.SetParameterValue("NomeAluno", aluno.Nome);
                report.SetParameterValue("RG", aluno.RG);
                report.SetParameterValue("CPF", CoreHelpers.FormatCNPJouCPF(aluno.CPF));

                string mes = new CultureInfo("pt-BR").DateTimeFormat.GetMonthName(DateTime.Now.Month);

                report.SetParameterValue("Data_Endereco", $"{unidade.Nome}, {DateTime.Now.Day} de {mes} de {DateTime.Now.Year}");

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
