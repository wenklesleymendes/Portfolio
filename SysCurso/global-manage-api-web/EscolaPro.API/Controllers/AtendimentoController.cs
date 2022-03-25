using EscolaPro.Service.Dto.AtendimentoVO;
using EscolaPro.Service.Interfaces.Atendimentos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    //[EnableCors("CorsApi")]
    [ApiController]
    public class AtendimentoController : ControllerBase
    {
        private readonly IAtendimentoService _atendimentoService;
        public AtendimentoController(IAtendimentoService atendimentoService)
        {
            _atendimentoService = atendimentoService;
        }

        [Route("Cadastrar")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody] DtoAtendimento dtoAtendimento)
        {
            try
            {
                var retorno = await _atendimentoService.Inserir(dtoAtendimento);
                // Gravar Autobaud
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                var json = JsonConvert.SerializeObject(dtoAtendimento);
                RegistraLog.Log(ex.Message + "\n" + json, TipoResquisicao.Json, "Inserir", "atendimento");
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, "Inserir", "atendimento");
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
                var retorno = await _atendimentoService.BuscarTodos();
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, "BuscarTodos", "atendimento");
                throw ex;
            }
        }

        [Route("BuscarPorId")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetById(int idAtendimento)
        {
            try
            {
                var retorno = await _atendimentoService.BuscarPorId(idAtendimento);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, $"GetById/{idAtendimento}", "atendimento");
                throw ex;
            }
        }

        [Route("BuscarAgendamentos")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarAgendamentos(int idUnidade)
        {
            try
            {
                var retorno = await _atendimentoService.BuscarAgendamentos(idUnidade);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, $"BuscarAgendamentos/{idUnidade}", "agendamento");
                throw ex;
            }
        }


        [Route("DeletarPorId")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Detetar(int idAtendimento)
        {
            try
            {
                var retorno = await _atendimentoService.Deletar(idAtendimento);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, $"Detetar/{idAtendimento}", "atendimento");
                throw ex;
            }
        }
    }
}