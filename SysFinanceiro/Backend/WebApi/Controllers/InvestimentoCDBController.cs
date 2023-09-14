using Domain.Interfaces.ICategoria;
using Domain.Interfaces.IInvestimentoCDB;
using Domain.Interfaces.InterfaceServicos;
using Entities.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
    
namespace WebApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InvestimentoCDBController : ControllerBase
    {
        private readonly IInvestimentoCDBServico _investimentoService;
        private readonly InterfaceInvestimentoCDB interfaceInvestimentoCDB;
        public InvestimentoCDBController(IInvestimentoCDBServico investimentoService)
        {
            _investimentoService = investimentoService;
        }

        [HttpGet("/api/ListarInvestimentosUsuario")]
        [Produces("application/json")]
        public async Task<object> ListarInvestimentoUsuario(string emailUsuario)
        {
            return await interfaceInvestimentoCDB.ListarInvestimentosUsuario(emailUsuario);
        }

        [HttpPost("/api/CalcularRendimento")]
        [Produces("application/json")]
        public IActionResult CalcularRendimento(DadosCDB dados)
        {
            try
            {
                var retorno = _investimentoService.CalculaCDB(dados);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
