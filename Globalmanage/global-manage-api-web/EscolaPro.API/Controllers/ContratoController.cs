using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EscolaPro.Core.Helpers;
using EscolaPro.Service.Dto;
using EscolaPro.Service.Dto.MatriculaAlunoVO;
using EscolaPro.Service.Dto.RelatoriosVO;
using EscolaPro.Service.Interfaces;
using EscolaPro.Service.Interfaces.MatriculaAlunos;
using FastReport;
using FastReport.Export.PdfSimple;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContratoController : ControllerBase
    {
        IMatriculaAlunoService _matriculaAlunoService;
        IAnexoService _anexoService;
        IPlanoPagamentoService _planoPagamentoService;
        IUnidadeService _unidadeService;
        IAlunoFinanceiroContratoService _alunoFinanceiroContratoService;
        IAlunoService _alunoService;
        IEmailSenderService _emailSenderService;
        IUsuarioService _usuarioService;
        private readonly IWebHostEnvironment _webHostingEnvironment;

        public ContratoController(IMatriculaAlunoService matriculaAlunoService,
            IAnexoService anexoService,
            IUnidadeService unidadeService,
            IAlunoFinanceiroContratoService alunoFinanceiroContratoService,
            IEmailSenderService emailSenderService,
            IAlunoService alunoService,
            IUsuarioService usuarioService,
            IPlanoPagamentoService planoPagamentoService,
            IWebHostEnvironment webHostingEnvironment)
        {
            _matriculaAlunoService = matriculaAlunoService;
            _anexoService = anexoService;
            _unidadeService = unidadeService;
            _alunoFinanceiroContratoService = alunoFinanceiroContratoService;
            _emailSenderService = emailSenderService;
            _alunoService = alunoService;
            _planoPagamentoService = planoPagamentoService;
            _usuarioService = usuarioService;
            _webHostingEnvironment = webHostingEnvironment;
        }

        [Route("GerarContrato")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> GerarContrato(RelatorioRequestDto relatorio)
        {
            try
            {
                var matricula = await _matriculaAlunoService.BuscarPorId(relatorio.MatriculaId);

                if (matricula == null)
                    return Ok();

                byte[] arquivo = null;

                arquivo = await GerarContratoPDF(matricula, relatorio.UsuarioLogadoId);

                return Ok(arquivo);
            }
            catch (Exception ex)
            {
                var json = JsonConvert.SerializeObject(relatorio);
                RegistraLog.Log(ex.Message + "\n" + json, TipoResquisicao.Json, this.GetType().FullName, "");
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, this.GetType().FullName, "");
                throw;
            }
        }

        [Route("DownloadContrato")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> DownloadContrato(int matriculaId)
        {
            try
            {
                var retorno = await _anexoService.DownloadDocumentoPorTipoEnum(matriculaId, Core.Model.TipoAnexoEnum.ContratoProcuracaoEja);

                return Ok(retorno.Arquivo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("UploadContrato")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> UploadContrato([FromForm] DtoAnexo dtoAnexo, IFormFile file)
        {
            try
            {
                await _anexoService.DeleterDocumento(dtoAnexo.MatriculaAlunoId, Core.Model.TipoAnexoEnum.ContratoProcuracaoEja);
  
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();

                        string s = Convert.ToBase64String(fileBytes);

                        string extension = Path.GetExtension(file.FileName);

                        dtoAnexo.Arquivo = fileBytes;
                        dtoAnexo.Extensao = file.ContentType;
                        dtoAnexo.DataAnexo = DateTime.Now;
                        dtoAnexo.ArquivoString = file.FileName;
                        dtoAnexo.TipoAnexo = Core.Model.TipoAnexoEnum.ContratoProcuracaoEja;
                    }
                }

                var retorno = await _anexoService.Inserir(dtoAnexo);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                var json = JsonConvert.SerializeObject(dtoAnexo);
                RegistraLog.Log(ex.Message + "\n" + json, TipoResquisicao.Json, this.GetType().FullName, "");
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, this.GetType().FullName, "");
                throw ex;
            }
        }


        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<byte[]> GerarContratoPDF(DtoMatriculaAlunoResponse matricula, int usuarioLogadoId)
        {
            try
            {
                var aluno = await _alunoService.BuscarPorId(matricula.AlunoId);

                var usuario = await _usuarioService.BuscarPorId(usuarioLogadoId);

                Report report = new Report();


                string mesAtual = new CultureInfo("pt-BR").DateTimeFormat.GetMonthName(DateTime.Now.Month);
                string dataAtual = $"{DateTime.Now.Day} de {mesAtual} de {DateTime.Now.Year}";

                if (matricula.Curso.NacionatalTec)
                {
                    report.Load($"{_webHostingEnvironment.ContentRootPath}/App_Data/contratoProcuracaoEjaNacionalTec.frx");

                    report.SetParameterValue("NomeCurso", $"{matricula.Curso.Descricao}.");
                }
                else if (matricula.Turma.Presencial)
                {
                    report.Load($"{_webHostingEnvironment.ContentRootPath}/App_Data/contratoProcuracaoEjaPresencial.frx");

                    report.SetParameterValue("MesInicio", new CultureInfo("pt-BR").DateTimeFormat.GetMonthName(matricula.Turma.InicioTurma.Value.Month));
                    report.SetParameterValue("AnoInicio", matricula.Turma.InicioTurma.Value.Year);

                    report.SetParameterValue("MesFim", new CultureInfo("pt-BR").DateTimeFormat.GetMonthName(matricula.Turma.TerminoTurma.Value.Month));
                    report.SetParameterValue("AnoFim", matricula.Turma.TerminoTurma.Value.Year);



                    report.SetParameterValue("DataAtual", dataAtual);
                }
                else
                {
                    report.Load($"{_webHostingEnvironment.ContentRootPath}/App_Data/contratoProcuracaoEjaDistancia.frx");
                }

                if (usuario.Funcionario != null)
                {
                    report.SetParameterValue("UsuarioLogado", $"{usuario.Funcionario.Nome}, ");
                }
                else
                {
                    report.SetParameterValue("UsuarioLogado", $"{usuario.UserName}, ");
                }

                report.SetParameterValue("DataAtual", dataAtual);

                var unidade = await _unidadeService.BuscarPorId(matricula.UnidadeId);

                report.SetParameterValue("UnidadeDados", $"{unidade.RazaoSocial} - {unidade.NomeFantasia}");

                report.SetParameterValue("RazaoSocial", $"{unidade.RazaoSocial}");
                report.SetParameterValue("NomeFantasia", $"{unidade.NomeFantasia}");

                string enderecoCompleto = $"{unidade.Endereco.Rua}, nº {unidade.Endereco.Numero}, {unidade.Endereco.Complemento}, " +
                    $"{unidade.Endereco.Bairro}, {unidade.Endereco.Cidade}, {unidade.Endereco.Estado}, CEP: {CoreHelpers.FormataCep(unidade.Endereco.CEP)}";

                report.SetParameterValue("UnidadeEndereco", enderecoCompleto);

                report.SetParameterValue("NomeAluno", aluno.Nome);
                report.SetParameterValue("Rua", aluno.Endereco.Rua);
                report.SetParameterValue("Numero", aluno.Endereco.Numero);
                report.SetParameterValue("Complemento", aluno.Endereco.Complemento);
                report.SetParameterValue("Bairro", aluno.Endereco.Bairro);
                report.SetParameterValue("Cidade", aluno.Endereco.Cidade);
                report.SetParameterValue("CEP", CoreHelpers.FormataCep(aluno.Endereco.CEP));
                report.SetParameterValue("Email", aluno.Contato.Email);
                report.SetParameterValue("Telefone", CoreHelpers.AplicarMascaraTelefone(aluno.Contato.Celular));
                report.SetParameterValue("RG", aluno.RG);
                report.SetParameterValue("CPF", CoreHelpers.FormatCNPJouCPF(aluno.CPF));

                if(aluno.Naturalidade != null)
                    report.SetParameterValue("Naturalizacao", aluno.Naturalidade.Descricao);
                else
                    report.SetParameterValue("Naturalizacao", "");

                string cnpj = Convert.ToUInt64(matricula.Unidade.CNPJ).ToString(@"00\.000\.000\/0000\-00");

                report.SetParameterValue("CNPJ", cnpj);
                report.SetParameterValue("CidadeUnidade", unidade.Endereco.Cidade);
                report.SetParameterValue("EstadoUnidade", unidade.Endereco.Estado);

                report.SetParameterValue("Estado", aluno.Endereco.Estado);
                report.SetParameterValue("DataNascimento", aluno.DataNascimento.ToString("dd/MM/yyyy"));

                var maiorIdade = Convert.ToDateTime(aluno.DataNascimento).AddYears(18) < DateTime.Now;

                if (!maiorIdade)
                {
                    string responsavel = $"Menor de 18 anos, dados do responsável: Nome: {aluno.NomeResponsavel} R.G.nº {aluno.RGResponsavel} CPF nº{CoreHelpers.FormatCNPJouCPF(aluno.CPFResponsavel)}";
                    report.SetParameterValue("NomeResponsavel", responsavel);
                }

                var planoPagamento = await _planoPagamentoService.BuscarPorId(matricula.PlanoPagamentoAluno.PlanoPagamentoId.Value);

                string planoPagamentoDescricao = string.Empty;
                string taxaMatricula = string.Empty;
                string apostila = string.Empty;
                string parcela = string.Empty;
                string desconto = string.Empty;
                string tipoPagamento = string.Empty;
                string descricaoBoleto = string.Empty;
                string taxaInscricao = string.Empty;
                string valorMulta = string.Empty;
                string turmaMatricula = string.Empty;

                planoPagamentoDescricao = string.Empty;

                switch (planoPagamento.TipoPagamento)
                {
                    case Core.Model.Enums.TipoPagamentoEnum.CartaoCredito:
                        tipoPagamento = "Cartão de Crédito";
                        break;
                    case Core.Model.Enums.TipoPagamentoEnum.CartaoDebito:
                        tipoPagamento = "Cartão de Débito";
                        break;
                    case Core.Model.Enums.TipoPagamentoEnum.BoletoBancario:
                        tipoPagamento = "Boleto Bancário";

                        var dataPrimeiroParcela = matricula.PlanoPagamentoAluno.DataPrimeiraParcela.Value.ToString("dd/MM/yyyy");
                        if (matricula.PlanoPagamentoAluno.DataSegundaParcela.HasValue)
                        {
                            var dataUltimaParcela = matricula.PlanoPagamentoAluno.DataSegundaParcela.Value.AddMonths(planoPagamento.QuantidadeParcela - 2).ToString("dd/MM/yyyy");

                            descricaoBoleto = $"\nA primeira e a segunda parcela da taxa de inscrição terão seus vencimentos para 30 e 60 dias" +
                                $" (consecutivamente) após o vencimento da última mensalidade. 1ª mensalidade vencimento: {dataPrimeiroParcela} última mensalidade " +
                                $"vencimento: {dataUltimaParcela}. O (a) Contratante declara ter recebidos os boletos para pagamento do curso, " +
                                "no ato da assinatura do contrato.";
                        }
                        else
                        {
                            descricaoBoleto = "\nA primeira e a segunda parcela da taxa de inscrição terão seus vencimentos para 30 e 60 dias" +
                                $" (consecutivamente) após o vencimento do parcela a vista. 1ª e única parcela vencimento: {dataPrimeiroParcela}. " +
                                "O (a) Contratante declara ter recebidos os boletos para pagamento do curso, no ato da assinatura do contrato.";
                        }
                        break;
                    case Core.Model.Enums.TipoPagamentoEnum.CobrancaRecorrente:
                        tipoPagamento = "Cobrança Recorrente";
                        break;
                    default:
                        break;
                }

                report.SetParameterValue("Boleto", descricaoBoleto);

                if (planoPagamento.IsentarMatricula)
                {
                    taxaMatricula = "Taxa de matrícula: grátis";
                }
                else
                {
                    // var valorTaxaMatricula = planoPagamento.ValorTaxaMatricula / 2;
                    taxaMatricula = $"Taxa de matrícula: R$ {planoPagamento.ValorTaxaMatricula.ToString("N2") }";
                }

                if (planoPagamento.TipoPagamento == Core.Model.Enums.TipoPagamentoEnum.BoletoBancario)
                {
                    var valorTaxaInscricao = planoPagamento.ValorTotalInscricaoProva / 2;

                    taxaInscricao = $"Taxa de inscrição: 2x de R$ {valorTaxaInscricao.Value.ToString("N2")}";
                }

                if (matricula.PlanoPagamentoAluno.TemApostila.HasValue)
                {
                    if (matricula.PlanoPagamentoAluno.TemApostila.Value)
                    {
                        apostila = planoPagamento.IsentarMaterialDidatico ? "Apostila: grátis" : $"Apostila: R$ {planoPagamento.ValorMaterialDidatico.Value.ToString("N2")}";
                    }
                    else
                    {
                        apostila = planoPagamento.IsentarMaterialDidatico ? "Apostila: grátis" : $"";
                    }
                }
                else
                {
                    apostila = planoPagamento.IsentarMaterialDidatico ? "Apostila: grátis" : $"";
                }

                //apostila = planoPagamento.IsentarMaterialDidatico ? "Apostila: grátis" : $"Apostila: R$ {planoPagamento.ValorMaterialDidatico.Value.ToString("N2")}";

                parcela = $"Parcelas curso: {planoPagamento.QuantidadeParcela}x R$ {planoPagamento.ValorParcela.ToString("N2")}";

                if (planoPagamento.PorcentagemDescontoPontualidade > 0)
                {
                    desconto = $" Desconto pontualidade: {planoPagamento.PorcentagemDescontoPontualidade}% para pagamento até a data do vencimento.";
                }

                if (planoPagamento.TipoPagamento == Core.Model.Enums.TipoPagamentoEnum.BoletoBancario)
                {
                    planoPagamentoDescricao = $"{tipoPagamento} - {taxaMatricula} {apostila} {parcela} {taxaInscricao}{desconto}";
                }
                else
                {
                    planoPagamentoDescricao = $"{tipoPagamento} - {taxaMatricula}, {apostila}, {parcela}{desconto}";
                }

                // Calcular valor da multa
                // Não considerar taxa de inscrição de prova
                var valorTotalParcela = planoPagamento.ValorParcela * planoPagamento.QuantidadeParcela;

                var valorMultaCalculado = (Math.Round(valorTotalParcela * 20, 2, MidpointRounding.ToEven)) / 100;

                report.SetParameterValue("ValorMulta", $" {valorMultaCalculado.ToString("N2")}");

                string segunda = matricula.Turma.Segunda ? "Segunda-Feira" : "";
                string terca = matricula.Turma.Terca ? "Terca-Feira" : "";
                string quarta = matricula.Turma.Quarta ? "Quarta-Feira" : "";
                string quinta = matricula.Turma.Quinta ? "Quinta-Feira" : "";
                string sexta = matricula.Turma.Sexta ? "Sexta-Feira" : "";
                string sabado = matricula.Turma.Sabado ? "Sabado" : "";
                string domingo = matricula.Turma.Domingo ? "Domingo" : "";

                var diaDaSemana = $"{segunda}{terca}{quarta}{quinta}{sexta}{sabado}{domingo}";

                string periodo = string.Empty;

                if (matricula.Turma.Presencial)
                {
                    if (matricula.Turma.Periodo.HasValue)
                    {
                        switch (matricula.Turma.Periodo.Value)
                        {
                            case Core.Model.PeriodoEnum.Manha:
                                periodo = "Manhã";
                                break;
                            case Core.Model.PeriodoEnum.Tarde:
                                periodo = "Tarde";
                                break;
                            case Core.Model.PeriodoEnum.Noite:
                                periodo = "Noite";
                                break;
                            default:
                                break;
                        }
                    }

                    turmaMatricula = $"PERÍODO: { matricula.Turma.Periodo}, DIA DA SEMANA: {diaDaSemana}," +
                                     $" HORÁRIO DA AULA: {matricula.Turma.HorarioInicio.Insert(2, ":")} - {matricula.Turma.HorarioTermino.Insert(2, ":")}," +
                                     $" SALA DE AULA: {(int)matricula.Turma.Sala}";

                }
                else
                {
                    turmaMatricula = "A DISTÂNCIA";
                }

                report.SetParameterValue("TurmaMatricula", turmaMatricula);
                report.SetParameterValue("DataMatricula", matricula.DataMatricula.ToString("dd/MM/yyyy"));

                report.SetParameterValue("PlanoPagamento", planoPagamentoDescricao);

                //Running Report
                report.Prepare();

                byte[] arquivo = null;

                PDFSimpleExport pdfExport = new PDFSimpleExport();
                ////export PDF Report form

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

        [ApiExplorerSettings(IgnoreApi = true)]
        public void ContratoAdistancia()
        {

        }
    }
}
