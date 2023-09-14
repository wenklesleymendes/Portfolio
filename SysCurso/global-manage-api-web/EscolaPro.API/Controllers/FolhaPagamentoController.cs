using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EscolaPro.API.Dto;
using EscolaPro.Core.Model.Tickets;
using EscolaPro.Service.Dto.FolhaPagamentoVO;
using EscolaPro.Service.Interfaces;
using FastReport;
using FastReport.Data;
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
    public class FolhaPagamentoController : ControllerBase
    {
        IFolhaPagamentoService _folhaPagamentoService;
        private readonly IWebHostEnvironment _webHostingEnvironment;

        public FolhaPagamentoController(
            IFolhaPagamentoService folhaPagamentoService,
            IWebHostEnvironment webHostingEnvironment)
        {
            _folhaPagamentoService = folhaPagamentoService;
            _webHostingEnvironment = webHostingEnvironment;
        }

        [Route("Cadastrar")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody]DtoFolhaPagamento dtoFolhaPagamento)
        {
            try
            {
                var retorno = await _folhaPagamentoService.Inserir(dtoFolhaPagamento);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return Ok(new RetornoApi { Erro = ex.Message });
            }
        }

        [Route("BuscarPorId")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetById(int idFolhaPagamento)
        {
            try
            {
                var retorno = await _folhaPagamentoService.BuscarPorId(idFolhaPagamento);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [Route("Filtrar")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> BuscarTodos(DtoFiltrarBusca filtrarBusca)
        {
            try
            {
                var retorno = await _folhaPagamentoService.BuscarTodos(filtrarBusca);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("DownloadComprovanteBancario")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> DownloadComprovanteBancario(int idFolhaPagamento)
        {
            try
            {
                var retorno = await _folhaPagamentoService.BuscarPorId(idFolhaPagamento);

                return await ImprimirReciboPagamento(idFolhaPagamento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("Excluir")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Excluir(int idFolhaPagamento)
        {
            var retorno = await _folhaPagamentoService.Excluir(idFolhaPagamento);

            return Ok(retorno);
        }


        [Route("ImprimirReciboPagamento")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> ImprimirReciboPagamento(int idFolhaPagamento)
        {
            try
            {
                var folhaPagamento = await _folhaPagamentoService.ImprimirReciboPagamento(idFolhaPagamento);

                if (folhaPagamento == null)
                    return Ok();


                Report report = new Report();
                report.Load($"{_webHostingEnvironment.ContentRootPath}/App_Data/Holerite.frx");

                report.SetParameterValue("DataImpressao", DateTimeOffset.Now.ToString("dd/MM/yyyy"));
                report.SetParameterValue("Nome", folhaPagamento.Funcionario.Nome);
                report.SetParameterValue("Matricula", folhaPagamento.Funcionario.DadosContratacao.Matricula);
                report.SetParameterValue("Endereco", $"{folhaPagamento.Funcionario.Endereco.Rua}, nº {folhaPagamento.Funcionario.Endereco.Numero}");
                report.SetParameterValue("Bairro", folhaPagamento.Funcionario.Endereco.Bairro);
                report.SetParameterValue("Cidade", folhaPagamento.Funcionario.Endereco.Cidade);
                report.SetParameterValue("RegimeContratacao", folhaPagamento.Funcionario.DadosContratacao.TipoRegimeContratacao.GetDisplayName());
                report.SetParameterValue("Telefone", folhaPagamento.Funcionario.Contato.TelefoneFixo);
                report.SetParameterValue("DataPagamento", folhaPagamento.Competencia.HasValue ? folhaPagamento.Competencia.Value.ToString("dd/MM/yyyy") : "");

                report.SetParameterValue("RazaoSocial", folhaPagamento.Unidade.RazaoSocial);
                report.SetParameterValue("CNPJ", folhaPagamento.Unidade.CNPJ);
                report.SetParameterValue("EnderecoUnidade", $"{folhaPagamento.Unidade.Endereco.Rua}, {folhaPagamento.Unidade.Endereco.Numero}, {folhaPagamento.Unidade.Endereco.Cidade}, {folhaPagamento.Unidade.Endereco.Estado}");
                report.SetParameterValue("NomeFantasia", folhaPagamento.Unidade.NomeFantasia);

                report.SetParameterValue("TotalFinal", "R$ "+folhaPagamento.ValorTotalAPagar.ToString("N2", new CultureInfo("pt-BR")));
                report.SetParameterValue("TotalDesconto", "R$ "+folhaPagamento.TotalDesconto.ToString("N2", new CultureInfo("pt-BR")));

                report.SetParameterValue("Banco", folhaPagamento.Funcionario.DadosBancario.NomeBanco);
                report.SetParameterValue("Agencia", folhaPagamento.Funcionario.DadosBancario.NumeroAgencia);
                report.SetParameterValue("Conta", folhaPagamento.Funcionario.DadosBancario.NumeroConta);

                report.RegisterData(folhaPagamento.ListaPagamento.Select(item => new Pagamento
                {
                    Descricao = item.Descricao,
                    Valor =  $"R$ {item.Valor.ToString("N2", new CultureInfo("pt-BR"))}"
                }).ToList(), "ItemPedido");
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
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, "Folha de Pagamento", "ImprimirReciboPagamento");
                throw ex;
            }
        }

        public class Pagamento
        {
            public string Descricao { get; set; }
            public string Valor { get; set; }
        }
    }
}