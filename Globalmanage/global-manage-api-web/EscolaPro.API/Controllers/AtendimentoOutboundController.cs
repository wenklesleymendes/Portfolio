using EscolaPro.Core.Model;
using EscolaPro.Service.Dto.AtendimentoVO;
using EscolaPro.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    //[EnableCors("CorsApi")]
    [ApiController]
    public class AtendimentoOutboundController : ControllerBase
    {
        private readonly IAtendimentoOutboundService _atendimentoOutbound;

        public AtendimentoOutboundController(IAtendimentoOutboundService atendimentoOutbound)
        {
            _atendimentoOutbound = atendimentoOutbound;
        }

        [Route("Cadastrar")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody] DtoAtendimentoOutbound outbound)
        {
            try
            {
                var retorno = await _atendimentoOutbound.Inserir(outbound);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                var json = JsonConvert.SerializeObject(outbound);
                RegistraLog.Log(ex.Message + "\n" + json, TipoResquisicao.Json, "Inserir", "AtendimentoOutbound");
                throw ex;
            }
        }

        [Route("CarregarOutbounds")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Outbound(int idUnidade)
        {
            try
            {
                var retorno = await _atendimentoOutbound.ObtenhaOutbound(idUnidade);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, 
                    $"{nameof(Outbound)}", $"{nameof(AtendimentoOutboundController)}");

                throw ex;
            }
        }

        [Route("HistoricoTentativas")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> HistoricoTentativas(int idAtendimento)
        {
            try
            {
                var retorno = await _atendimentoOutbound.HistoricoTentativas(idAtendimento);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, "BuscarAtendimentoFila()", "AtendimentoOutbound");
                throw ex;
            }
        }

        [Route("Contar")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> ContarContatos(int idAtendimento)
        {
            try
            {
                var retorno = await _atendimentoOutbound.ContarContatos(idAtendimento);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, $"ContarContatos(string {idAtendimento})", "AtendimentoOutbound");
                throw ex;
            }
        }
    }
}