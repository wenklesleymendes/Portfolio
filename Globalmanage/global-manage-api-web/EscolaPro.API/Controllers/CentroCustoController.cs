using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscolaPro.Service.Dto.UnidadeVO;
using EscolaPro.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CentroCustoController : ControllerBase
    {
        private readonly ICentroCustoService _centroCustoService;

        public CentroCustoController(
            ICentroCustoService centroCustoService)
        {
            _centroCustoService = centroCustoService;
        }


        [Route("Cadastrar")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody]DtoCentroCusto dtoCentroCusto)
        {
            var retorno = await _centroCustoService.Inserir(dtoCentroCusto);

            return Ok(retorno);
        }

        [Route("BuscarPorUnidade")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarPorUnidade(int idUnidade)
        {
            var retorno = await _centroCustoService.BuscarPorUnidade(idUnidade);

            return Ok(retorno);
        }

        [Route("Deletar")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Detetar(int idCentroCusto)
        {
            var retorno = await _centroCustoService.Deletar(idCentroCusto);

            return Ok(retorno);
        }
    }
}