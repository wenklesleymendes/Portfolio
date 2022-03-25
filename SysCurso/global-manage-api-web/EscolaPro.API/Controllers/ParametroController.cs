using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscolaPro.Core.Model;
using EscolaPro.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParametroController : ControllerBase
    {
        IParametroService _parametroService;

        public ParametroController(IParametroService parametroService)
        {
            _parametroService = parametroService;
        }

        [Route("Atualizar")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody]Parametro parametro)
        {
            var retorno = await _parametroService.Atualizar(parametro);

            return Ok(retorno);
        }

        [Route("BuscarParametroPorChave")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarParametroPorChave(string chave)
        {
            var retorno = await _parametroService.BuscarParametroPorChave(chave);

            return Ok(retorno);
        }
    }
}