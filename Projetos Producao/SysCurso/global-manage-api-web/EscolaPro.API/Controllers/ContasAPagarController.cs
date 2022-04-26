using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using EscolaPro.Core.Model.ContasPagar;
using EscolaPro.Core.Model.Fornecedores;
using EscolaPro.Service.Dto.ContasAPagarVO;
using EscolaPro.Service.Dto.FolhaPagamentoVO;
using EscolaPro.Service.Dto.FornecedorVO;
using EscolaPro.Service.Interfaces;
using FastReport;
using FastReport.Export.PdfSimple;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContasAPagarController : ControllerBase
    {
        IContasPagarService _contasPagarService;
        private readonly IWebHostEnvironment _webHostingEnvironment;

        public ContasAPagarController(
            IContasPagarService contasPagarService,
            IWebHostEnvironment webHostingEnvironment)
        {
            _contasPagarService = contasPagarService;
            _webHostingEnvironment = webHostingEnvironment;
        }

        [Route("Cadastrar")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody]DtoDespesa dtoDespesa)
        {
            try
            {
                var retorno = await _contasPagarService.Inserir(dtoDespesa);

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
            var retorno = await _contasPagarService.BuscarTodos(filtrarBusca);

            return Ok(retorno);
        }

        [Route("BuscarPorId")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetById(int idDespesa)
        {
            var retorno = await _contasPagarService.BuscarPorId(idDespesa);

            return Ok(retorno);
        }


        [Route("LiquidarPagamento")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> LiquidarPagamento(DtoLiquidarDespesa dtoLiquidarDespesa)
        {
            var retorno = await _contasPagarService.LiquidarPagamento(dtoLiquidarDespesa);

            return Ok(retorno);
        }

        [Route("Excluir")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Excluir(int idDespesa)
        {
            var retorno = await _contasPagarService.Excluir(idDespesa);

            return Ok(retorno);
        }

        [Route("CancelarPagamento")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> CancelarPagamento(DtoDespesaCancelar despesaCancelar)
        {
            var retorno = await _contasPagarService.CancelarPagamento(despesaCancelar);

            return Ok(retorno);
        }


        [Route("BuscarDetalheDespesa")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarDetalheDespesa(int idDespesa)
        {
            try
            {
                var retorno = await _contasPagarService.BuscarDetalheDespesa(idDespesa);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("ImprimirRecibo")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> ImprimirRecibo(DtoImprimirRecibo imprimirRecibo)
        {
            var despesa = await _contasPagarService.BuscarPorId(imprimirRecibo.IdDespesa);

            Report report = new Report();
            report.Load($"{_webHostingEnvironment.ContentRootPath}/App_Data/reciboPagamento.frx");

            report.SetParameterValue("DataPagamento", imprimirRecibo.Data.ToString("dd/MM/yyyy"));

            if (despesa.DataEmissao.HasValue)
            {
                report.SetParameterValue("DataEmissao", despesa.DataEmissao.Value.ToString("dd/MM/yyyy"));
            }

            report.SetParameterValue("Unidade", despesa.Unidade.Nome);

            if (despesa.TipoDespesa != TipoDespesaEnum.GuiaGPS && despesa.TipoDespesa != TipoDespesaEnum.GuiaDARF)
            {
                report.SetParameterValue("Nome", despesa.Fornecedor.RazaoSocial);
                report.SetParameterValue("Endereco", $"{despesa.Fornecedor.Endereco.Rua}, nº{despesa.Fornecedor.Endereco.Numero}");
                report.SetParameterValue("CEP", despesa.Fornecedor.Endereco.CEP);
                report.SetParameterValue("Cidade", despesa.Fornecedor.Endereco.Cidade);
                report.SetParameterValue("Estado", despesa.Fornecedor.Endereco.Estado);
                report.SetParameterValue("CNPJCPF", despesa.Fornecedor.CpfCnpj);
            }

            report.SetParameterValue("DescricaoCompra", despesa.Observacao);
            report.SetParameterValue("Correspondente", imprimirRecibo.Descricao);
            report.SetParameterValue("ValorTotal", despesa.DespesaParcela.Count() > 0 ? $"R$ {despesa.DespesaParcela.FirstOrDefault().ValorParcela.ToString("N2")}" : "R$ 0,00" );

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
    }
}