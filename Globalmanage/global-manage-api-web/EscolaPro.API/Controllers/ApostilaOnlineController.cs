using EscolaPro.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApostilaOnlineController : Controller
    {

        private readonly IApostilaOnlineService _apostilaOnlineService;

        public ApostilaOnlineController(IApostilaOnlineService apostilaOnlineService)
        {
            _apostilaOnlineService = apostilaOnlineService;
        }
        [Route("BucarApostilaPorIdMateria")]
        [HttpGet]
        public async Task<IActionResult> BucarApostilaPorIdMateria(int materiaId)
        {
            var retorno = await _apostilaOnlineService.BucarApostilaPorIdMateria(materiaId);
            return Ok(retorno);
        }

        [Route("BucarApostilaPorCursoId")]
        [HttpGet]
        public async Task<IActionResult> BucarApostilaPorCursoId(int cursoId)
        {
            var retorno = await _apostilaOnlineService.BucarApostilaPorCursoId(cursoId);
            return Ok(retorno);
        }
    }
}
