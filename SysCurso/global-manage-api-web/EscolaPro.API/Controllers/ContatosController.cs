using EscolaPro.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    //[EnableCors("CorsApi")]
    [ApiController]
    public class ContatosController : ControllerBase
    {
        private readonly IContatosService _contatosService;

        public ContatosController(IContatosService contatosService)
        {
            _contatosService = contatosService;
        }

        [Route("BuscarPorCelular")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarPorCelular(string celular)
        {
            try
            {
                var retorno = await _contatosService.BuscarPorCelular(celular);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, $"{nameof(BuscarPorCelular)}", $"{nameof(ContatosController)}");
                throw ex;
            }
        }
    }
}
