using Microsoft.AspNetCore.Mvc;
using ServidorDePontos.Aplication.Interface;
using ServidorDePontos.Core.Model;

namespace ServidorDePontos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameResultController : Controller
    {
        private readonly IGameResultService gameResultService;

        public GameResultController(IGameResultService gameResultService)
        {
            this.gameResultService = gameResultService;
        }

        [Route("EnviadadosAoServidor")]
        [HttpPost]
        public async Task<IActionResult> PersistiDados(List<GameResult> dados)
        {
            await gameResultService.PersisteDadosNaMemoria(dados);

            return Ok();
        }
    }
}
