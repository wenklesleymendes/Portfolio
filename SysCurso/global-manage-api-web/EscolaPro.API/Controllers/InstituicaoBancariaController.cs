using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscolaPro.Service.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
   // [EnableCors("CorsApi")]
    [ApiController]
    public class InstituicaoBancariaController : ControllerBase
    {
        IInstituicaoBancariaService _instituicaoBancariaService;
        public InstituicaoBancariaController(IInstituicaoBancariaService instituicaoBancariaService)
        {
            _instituicaoBancariaService = instituicaoBancariaService;
        }

        [Route("BuscarTodos")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            var retorno = await _instituicaoBancariaService.BuscarTodos();

            return Ok(retorno);
        }
    }
}