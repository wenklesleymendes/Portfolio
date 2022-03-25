using EscolaPro.Service.Interfaces.AgendaProvas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadeTransporteProvaController : ControllerBase
    {
        private readonly IUnidadeTransporteProvaService _unidadeTransporteProvaService;

        public UnidadeTransporteProvaController(IUnidadeTransporteProvaService unidadeTransporteProvaService)
        {
            _unidadeTransporteProvaService = unidadeTransporteProvaService;
        }

        [Route("BuscarProximoOnibus")]
        [HttpGet]
        public IActionResult BuscarProximoOnibus(int agendaProvaId, int unidadeId)
        {
            var retorno = _unidadeTransporteProvaService.BuscarProximoOnibus(agendaProvaId, unidadeId);

            return Ok(retorno);
        }

        [Route("BuscarOnibus")]
        [HttpGet]
        public async Task<IActionResult> BuscarOnibus(int UnidadeTransporteProvaId)
        {
            var retorno = await _unidadeTransporteProvaService.BuscarOnibus(UnidadeTransporteProvaId);

            return Ok(retorno);
        }
    }
}
