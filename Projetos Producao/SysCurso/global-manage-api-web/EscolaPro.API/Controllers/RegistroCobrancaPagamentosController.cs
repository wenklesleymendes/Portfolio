using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoletoNetCore;
using EscolaPro.ControleBoleto;
using EscolaPro.Core.Model.ArquivoRemessa;
using EscolaPro.Core.Model.ArquivoRemessa.ArquivoSimples;
using EscolaPro.Service.Dto.PagamentosVO;
using EscolaPro.Service.Dto.RegistroCobrancaVO;
using EscolaPro.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroCobrancaPagamentosController : ControllerBase
    {
        IRegistroCobrancaService _registroCobrancaService;
        IAuthItauService _authItauService;

        public RegistroCobrancaPagamentosController(
            IRegistroCobrancaService registroCobrancaService,
            IAuthItauService authItauService)
        {
            _registroCobrancaService = registroCobrancaService;
            _authItauService = authItauService;
        }

        [Route("GerarBoleto")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody]ItauSimplesCorpoCobranca dtoCorpoCobranca)
        {
            try
            {
                var retorno = await _registroCobrancaService.GerarBoleto(dtoCorpoCobranca);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("ImprimirBoletoPdf")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> GerarBoleto([FromBody]DtoCorpoCobranca dtoCorpoCobranca)
        {
            try
            {
                var retorno = await _registroCobrancaService.BoletoImpressoPdf(dtoCorpoCobranca, null, null, null);

                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("UploadArquivoRemessa")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> UploadArquivoRemessa([FromBody]List<DtoBoleto> boletos)
        {
            try
            {
                await _registroCobrancaService.UploadArquivoRemessa(boletos);
                
                return Ok();
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message + "\n", TipoResquisicao.Json, "UploadArquivoRemessa", "UploadArquivoRemessa");
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, "UploadArquivoRemessa", "UploadArquivoRemessa");
                throw;
            }
        }
    }
}