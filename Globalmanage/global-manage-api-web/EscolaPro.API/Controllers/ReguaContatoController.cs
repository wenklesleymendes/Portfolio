using EscolaPro.Core.Model.ReguaContato;
using EscolaPro.Service.Dto.ReguaContato;
using EscolaPro.Service.Interfaces.ReguaContato;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReguaContatoController : ControllerBase
    {
        private readonly IReguaContatoService _reguaContatoService;

        public ReguaContatoController(IReguaContatoService reguaContatoService)
        {
            _reguaContatoService = reguaContatoService;
        }

        [Route("CarregaReguaContatoCobranca")]
        [HttpGet]
        public async Task<IActionResult> CarregaReguaContatoCobranca()
        {
            try
            {
                await _reguaContatoService.CarregaReguaContatoCobranca();

                return Ok();
            }
            catch (Exception ex)
            {

                RegistraLog.Log(ex.Message, TipoResquisicao.Json, "ReguaContato", "CarregaReguaContatoCobranca");
                throw ex;
            }
        }

        [Route("RetornaContatoFilas")]
        [HttpPost]
        public async Task<IActionResult> RetornaContatoFilas([FromBody] TipoMensagem tipoMensagem)
        {
            try
            {
                IEnumerable<ReguaContatoFila> contatosFila = await _reguaContatoService.GetContatoFilas(tipoMensagem);

                return Ok(contatosFila);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message, TipoResquisicao.Json, "ReguaContato", "RetornaContatoFilas");
                throw ex;
            }
        }

        [Route("DispararContato")]
        [HttpPost]
        public async Task<IActionResult> DispararContato([FromBody] ReguaContatoFilaDto reguaContatoFila)
        {
            try
            {
                await _reguaContatoService.DispararContato(reguaContatoFila);
                return Ok(true);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message, TipoResquisicao.Json, "ReguaContato", "DispararContato");
                return Ok(false);
            }
        }

    }
}
