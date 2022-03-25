using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using EscolaPro.Core.Helpers;
using EscolaPro.Core.Model;
using EscolaPro.Service.Dto;
using EscolaPro.Service.Dto.DocumentosAlunoVO;
using EscolaPro.Service.Dto.PagamentosVO;
using EscolaPro.Service.Interfaces;
using EscolaPro.Service.Interfaces.MatriculaAlunos;
using EscolaPro.Service.Interfaces.Pagamentos;
using FastReport;
using FastReport.Export.PdfSimple;
using FastReport.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using Newtonsoft.Json;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentosAlunoController : ControllerBase
    {
        IMatriculaAlunoService _matriculaAlunoService;
        IAnexoService _anexoService;
        IUnidadeService _unidadeService;
        IAlunoFinanceiroContratoService _alunoFinanceiroContratoService;
        IAlunoService _alunoService;
        IEmailSenderService _emailSenderService;
        IUsuarioService _usuarioService;
        IPlanoPagamentoService _planoPagamentoService;
        private readonly IWebHostEnvironment _webHostingEnvironment;

        public DocumentosAlunoController(IMatriculaAlunoService matriculaAlunoService,
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
            _usuarioService = usuarioService;
            _planoPagamentoService = planoPagamentoService;
            _webHostingEnvironment = webHostingEnvironment;
        }


        [Route("ConsultarDocumentosPendentes")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> ConsultarDocumentosPendentes(int matriculaId)
        {
            try
            {
                var retorno = await _matriculaAlunoService.ConsultarDocumentosPendentes(new Core.Model.PainelMatricula.MatriculaAluno { Id = matriculaId });

                var pendenciaDocumental = await _anexoService.DownloadDocumentoPorTipoEnum(matriculaId, TipoAnexoEnum.DeclaracaoPendenciaDocumental);

                retorno.DeclaracaoPendenciaDocumental = pendenciaDocumental == null ? true : false;

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("GerarPendenciaDocumental")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GerarPendenciaDocumental(int matriculaId)
        {
            try
            {
                var documentoAluno = await _matriculaAlunoService.GerarDocumentosPendencia(matriculaId);

                if (documentoAluno == null)
                    return Ok();


                Report report = new Report();

                report.Load($"{_webHostingEnvironment.ContentRootPath}/App_Data/declaracaoPendenciaDocumental.frx");

                // Imagem logo

                //Get picture object from the report template
                PictureObject pic = report.FindObject("Picture1") as PictureObject;
                //Set object bounds
                pic.Bounds = new RectangleF(0, 0, Units.Centimeters * 4, Units.Centimeters * 4);

                string logo = CoreHelpers.ExibirLogo(documentoAluno.Unidade.CNPJ);
                //Set the image
                pic.Image = new Bitmap($"{_webHostingEnvironment.ContentRootPath}/App_Data/Imagens/{logo}");

                // Documentos
                if (documentoAluno.Unidade != null)
                {
                    string cnpj = Convert.ToUInt64(documentoAluno.Unidade.CNPJ).ToString(@"00\.000\.000\/0000\-00");

                    report.SetParameterValue("DescricaoPrimeira", $"{documentoAluno.Unidade.RazaoSocial}");
                    report.SetParameterValue("CNPJ", $"CNPJ: {cnpj}");
                }
                else
                {
                    report.SetParameterValue("DescricaoPrimeira", $"");
                }

                if (documentoAluno.Unidade.Endereco != null)
                {
                    report.SetParameterValue("DescricaoSegunda", $"{documentoAluno.Unidade.Endereco.Rua}, " +
                        $"{documentoAluno.Unidade.Endereco.Numero}, " +
                        $"{documentoAluno.Unidade.Endereco.Bairro}, " +
                        $"{documentoAluno.Unidade.Endereco.Cidade} - {documentoAluno.Unidade.Endereco.Estado}, " +
                        $"CEP: {documentoAluno.Unidade.Endereco.CEP}");

                    report.SetParameterValue("NomeUnidade", documentoAluno.Unidade.NomeFantasia);
                }
                else
                {
                    report.SetParameterValue("DescricaoSegunda", $"");
                    report.SetParameterValue("NomeUnidade", "");
                }

                if (documentoAluno.Unidade.Contato != null)
                {
                    string telefone = CoreHelpers.AplicarMascaraTelefone(documentoAluno.Unidade.Contato.TelefoneFixoPrincipal);

                    if (documentoAluno.Unidade.Contato.TelefoneFixoPrincipal.Length > 8)
                    {
                        report.SetParameterValue("DescricaoTerceira", $"Telefone: {telefone}");
                    }
                    else
                    {
                        report.SetParameterValue("DescricaoTerceira", $"Telefone: {telefone}");
                    }
                }
                else
                {
                    report.SetParameterValue("DescricaoTerceira", $"");
                }

                report.SetParameterValue("NomeAluno", documentoAluno.Aluno.Nome);
                report.SetParameterValue("RG", documentoAluno.Aluno.RG);
                report.SetParameterValue("CPF", documentoAluno.Aluno.CPF);
                report.SetParameterValue("DataImpressao", DateTimeOffset.Now.ToString("dd/MM/yyyy"));

                string mes = new CultureInfo("pt-BR").DateTimeFormat.GetMonthName(DateTime.Now.Month);
                int dia = DateTime.Now.Day;
                int ano = DateTime.Now.Year;

                report.SetParameterValue("EnderecoUnidade", $"{documentoAluno.Unidade.Endereco.Cidade}, {documentoAluno.Unidade.Endereco.Estado}");

                report.SetParameterValue("NomeUnidade", documentoAluno.Unidade.NomeFantasia);

                report.SetParameterValue("Dia", dia);
                report.SetParameterValue("Mes", mes);
                report.SetParameterValue("Ano", ano);

                var maiorIdade = Convert.ToDateTime(documentoAluno.Aluno.DataNascimento).AddYears(18) < DateTime.Now;

                foreach (var documento in documentoAluno.Documentos)
                {
                    switch (documento)
                    {
                        case Core.Model.TipoAnexoEnum.RG_CPF:
                            report.SetParameterValue("CopiaRG", "X");
                            report.SetParameterValue("CopiaCPF", "X");
                            break;
                        case Core.Model.TipoAnexoEnum.RG_CPF_TituloEleitor:
                            report.SetParameterValue("CopiaRG", "X");
                            report.SetParameterValue("CopiaCPF", "X");
                            if (maiorIdade)
                            {
                                report.SetParameterValue("TituloEleitor", "X");
                            }
                            break;
                        case Core.Model.TipoAnexoEnum.CopiaRG:
                            report.SetParameterValue("CopiaRG", "X");
                            break;
                        case Core.Model.TipoAnexoEnum.CopiaCPF:
                            report.SetParameterValue("CopiaCPF", "X");
                            break;
                        case Core.Model.TipoAnexoEnum.Foto3X4:
                            report.SetParameterValue("Foto3X4", "X");
                            break;
                        case Core.Model.TipoAnexoEnum.ComprovanteEndereco:
                            report.SetParameterValue("ComprovanteEndereco", "X");
                            break;
                        case Core.Model.TipoAnexoEnum.HistoricoEscolar:
                            report.SetParameterValue("HistoricoEscolar", "X");
                            break;
                        case Core.Model.TipoAnexoEnum.ComprovanteAlfabetizacao:
                            if (documentoAluno.Curso.Descricao.ToUpper() == "ENSINO MÉDIO")
                            {
                                report.SetParameterValue("ComprovanteAlfabetizacao", "X");
                            }
                            break;
                        case Core.Model.TipoAnexoEnum.Reservista:
                            if (documentoAluno.Aluno.Sexo == Core.Model.SexoEnum.Masculino)
                            {
                                if (maiorIdade)
                                {
                                    report.SetParameterValue("Reservista", "X");
                                }
                            }
                            break;
                        case Core.Model.TipoAnexoEnum.TituloEleitor:
                            if (maiorIdade)
                            {
                                report.SetParameterValue("TituloEleitor", "X");
                            }
                            break;
                        case Core.Model.TipoAnexoEnum.CopiaRGcomCPFResponsavel:
                            if (!maiorIdade)
                            {
                                report.SetParameterValue("CopiaRGResponsavel", "X");
                            }
                            if (!maiorIdade)
                            {
                                report.SetParameterValue("CopiaCPFResponsavel", "X");
                            }
                            break;
                        case Core.Model.TipoAnexoEnum.CopiaRGResponsavel:
                            if (!maiorIdade)
                            {
                                report.SetParameterValue("CopiaRGResponsavel", "X");
                            }
                            break;
                        case Core.Model.TipoAnexoEnum.CopiaCPFResponsavel:
                            if (!maiorIdade)
                            {
                                report.SetParameterValue("CopiaCPFResponsavel", "X");
                            }
                            break;
                        case Core.Model.TipoAnexoEnum.CertidaoNascimento:
                            report.SetParameterValue("CertidaoNascimento", "X");
                            break;
                        case Core.Model.TipoAnexoEnum.CNH:
                            report.SetParameterValue("CNH", "X");
                            break;
                        case Core.Model.TipoAnexoEnum.Redacao:
                            report.SetParameterValue("Redacao", "X");
                            break;
                    }
                }

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

                return Ok(arquivo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("DownloadDeclaracaoPendenciaDocumental")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> DownloadDeclaracaoPendenciaDocumental(int matriculaId)
        {
            try
            {
                var retorno = await _anexoService.DownloadDocumentoPorTipoEnum(matriculaId, TipoAnexoEnum.DeclaracaoPendenciaDocumental);

                return Ok(retorno.Arquivo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [Route("UploadDeclaracaoPendenciaDocumental")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> UploadPendenciaDocumental([FromForm] DtoAnexo dtoAnexo, IFormFile file)
        {
            try
            {
                await _anexoService.DeleterDocumento(dtoAnexo.MatriculaAlunoId, TipoAnexoEnum.DeclaracaoPendenciaDocumental);

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
                        dtoAnexo.TipoAnexo = TipoAnexoEnum.DeclaracaoPendenciaDocumental;
                    }
                }

                var retorno = await _anexoService.InserirPendenciaDocumental(dtoAnexo);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                var json = JsonConvert.SerializeObject(dtoAnexo);
                RegistraLog.Log(ex.Message + "\n" + json, TipoResquisicao.Json, "Inserir", "UploadPendenciaDocumental");
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, "Inserir", "UploadPendenciaDocumental");
                throw ex;
            }
        }


        [Route("GerarDocumentacaoMatricula")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GerarDocumentacaoMatricula(int matriculaId)
        {
            try
            {
                var matricula = await _matriculaAlunoService.BuscarPorId(matriculaId);

                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("BuscarComprovanteBolsaConvenio")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarComprovanteBolsaConvenio(int matriculaId)
        {
            try
            {
                var retorno = await _anexoService.BuscarComprovante(matriculaId);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("ReciboPagamentoMensalidade")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> ReciboPagamentoMensalidade(DtoReciboPagamento reciboPagamento)
        {
            try
            {
                var matricula = await _matriculaAlunoService.BuscarPorId(reciboPagamento.MatriculaId);

                if (matricula == null)
                    return Ok();

                Report report = new Report();

                report.Load($"{_webHostingEnvironment.ContentRootPath}/App_Data/reciboEscola.frx");

                // Imagem logo

                //Get picture object from the report template
                PictureObject pic = report.FindObject("Picture1") as PictureObject;
                //Set object bounds
                pic.Bounds = new RectangleF(0, 0, Units.Centimeters * 4, Units.Centimeters * 4);

                var unidade = await _unidadeService.BuscarPorId(matricula.UnidadeId);

                var aluno = await _alunoService.BuscarPorId(matricula.AlunoId);

                var logo = await _unidadeService.SelecionarFoto(unidade.Id);
                //string logo = CoreHelpers.ExibirLogo(matricula.Unidade.CNPJ);
                //Set the image

                if (logo.Foto != null)
                {
                    using (var ms = new MemoryStream(logo.Foto))
                    {
                        pic.Image = new Bitmap(Image.FromStream(ms));
                    }
                    //pic.Image = new Bitmap(logo.Foto.ToArray())
                }

                //pic.Image = new Bitmap($"{_webHostingEnvironment.ContentRootPath}/App_Data/Imagens/{logo}");

                report.SetParameterValue("Endereco1", $"{unidade.Endereco.Rua}, " +
                    $"{unidade.Endereco.Numero}, " +
                    $"{unidade.Endereco.Bairro}");

                report.SetParameterValue("Endereco2", $"{unidade.Endereco.Cidade} - {unidade.Endereco.Estado}, " + $"CEP: {unidade.Endereco.CEP}");

                report.SetParameterValue("NomeAluno", $"RM: {matricula.NumeroMatricula} - {matricula.Aluno.Nome}");

                report.SetParameterValue("NumeroRecibo", new Random().Next(10000, 99999).ToString());

                List<DtoPagamento> dtoPagamentos = new List<DtoPagamento>();

                foreach (var pagamentoId in reciboPagamento.PagamentoIds)
                {
                    var pagamento = await _alunoFinanceiroContratoService.BuscarPorId(pagamentoId);

                    if (pagamento.Descricao.Contains("Crédito:") || pagamento.Descricao.Contains("Débito:"))
                    {
                        var planoPagamento = await _planoPagamentoService.BuscarPorId(matricula.PlanoPagamentoAluno.PlanoPagamentoId.Value);

                        if (planoPagamento.QuantidadeParcela > 1)
                        {
                            var valorTotalParcela = planoPagamento.ValorParcela * planoPagamento.QuantidadeParcela;

                            dtoPagamentos.Add(new DtoPagamento { Descricao = $"{planoPagamento.QuantidadeParcela}x Parcelas de R$ {planoPagamento.ValorParcela.ToString("N2")}", ValorPago = valorTotalParcela, DataPagamento = pagamento.DataPagamento });
                        }
                        else
                        {
                            var valorTotalParcela = planoPagamento.ValorParcela * planoPagamento.QuantidadeParcela;

                            dtoPagamentos.Add(new DtoPagamento { Descricao = $"{planoPagamento.QuantidadeParcela}x Parcela de R$ {planoPagamento.ValorParcela.ToString("N2")}", ValorPago = valorTotalParcela, DataPagamento = pagamento.DataPagamento });
                        }

                        if (!planoPagamento.IsentarMatricula)
                        {
                            dtoPagamentos.Add(new DtoPagamento { Descricao = $"Taxa de Matricula", ValorPago = planoPagamento.ValorTaxaMatricula, DataPagamento = pagamento.DataPagamento = pagamento.DataPagamento });
                        }

                        if (!planoPagamento.IsentarMaterialDidatico)
                        {
                            dtoPagamentos.Add(new DtoPagamento { Descricao = $"Apostila", ValorPago = planoPagamento.ValorMaterialDidatico.Value, DataPagamento = pagamento.DataPagamento });
                        }

                        //dtoPagamentos.Add(pagamento);
                    }
                    else
                    {
                        dtoPagamentos.Add(pagamento);
                    }
                }

                report.RegisterData(dtoPagamentos.OrderBy(x => x.DataPagamento).Select(item => new Pagamento
                {
                    Descricao = item.Descricao,
                    ValorPago = $"R$ {item.ValorPago.ToString("N2", new CultureInfo("pt-BR"))}",
                    DataPagamento = item.DataPagamento.Value.ToString("dd/MM/yyyy")
                }).ToList(), "ItemPedido");

                var valorTotal = dtoPagamentos.Sum(x => x.ValorPago);

                report.SetParameterValue("ValorTotal", $"R$ { valorTotal.ToString("N2", new CultureInfo("pt-BR")) }");

                if (reciboPagamento.UsuarioLogadoId > 0)
                {
                    var usuario = await _usuarioService.BuscarPorId(reciboPagamento.UsuarioLogadoId);

                    if (usuario.Funcionario != null)
                    {
                        report.SetParameterValue("NomeUsuario", $"{usuario.Funcionario.Nome}");
                    }
                    else
                    {
                        report.SetParameterValue("NomeUsuario", $"Usuário do Sistema: {usuario.UserName}");
                    }
                }

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

                Stream stream = new MemoryStream(arquivo);

                List<Attachment> attachments = new List<Attachment>();

                attachments.Add(new Attachment(stream, $"recibo.pdf"));

                List<string> emails = new List<string>();

                emails.Add(aluno.Contato.Email);

                if (matricula.Curso.Descricao == "Alfabetização, Ensino Fundamental e Médio"||
                    matricula.Curso.Descricao == "Ensino Fundamental e Médio" ||
                    matricula.Curso.Descricao == "Ensino Fundamental" ||
                    matricula.Curso.Descricao == "Ensino Médio")
                {
                    await _emailSenderService.SendEmailAsync(emails.ToArray(), $"Recibo de pagamento", $"Olá {aluno.Nome}! <br />Segue em anexo seu recibo de pagamento.", attachments);
                }
                else
                {
                    await _emailSenderService.SendEmailAsync(emails.ToArray(), $"Recibo de pagamento", $"Olá {aluno.Nome}! <br />Segue em anexo seu recibo de pagamento.", attachments, true);
                }

                return Ok(arquivo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class Pagamento
    {
        public string Descricao { get; set; }
        public string ValorPago { get; set; }
        public string DataPagamento { get; set; }
    }
}