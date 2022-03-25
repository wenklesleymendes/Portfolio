using EscolaPro.Service.Dto.AtendimentoVO;
using EscolaPro.Service.Interfaces.Atendimentos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    //[EnableCors("CorsApi")]
    [ApiController]
    public class LeadsController : ControllerBase
    {
        private readonly ILeadsService _leadsService;

        public LeadsController(ILeadsService leadsService)
        {
            _leadsService = leadsService;
        }

        [Route("Cadastrar")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody] dynamic leads)
        {
            try
            {
                string json = JsonSerializer.Serialize(leads);

                var retorno = await _leadsService.Inserir(json);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                var json = JsonSerializer.Serialize(leads);
                RegistraLog.Log(ex.Message + "\n" + json, TipoResquisicao.Json, "Inserir", "leadster");
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, "Inserir", "leadster");
                throw ex;
            }
        }

        [Route("BuscarTodos")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            try
            {
                var retorno = await _leadsService.BuscarTodos();

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message + "\n", TipoResquisicao.Json, "BuscarTodos()", "Leads");
                throw ex;
            }
        }
    }
}
