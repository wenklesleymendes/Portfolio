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
    public class AtendimentoAgendamentoController : ControllerBase
    {
        private readonly IAtendimentoAgendamentoService _atendimentoAgendamentoService;

        public AtendimentoAgendamentoController(IAtendimentoAgendamentoService atendimentoAgendamento)
        {
            _atendimentoAgendamentoService = atendimentoAgendamento;
        }

        [Route("Cadastrar")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody]DtoAtendimentoAgendamento dtoAtendimentoAgendamento)
        {
            try
            {
                var retorno = await _atendimentoAgendamentoService.Inserir(dtoAtendimentoAgendamento);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                var json = JsonConvert.SerializeObject(dtoAtendimentoAgendamento);
                RegistraLog.Log(ex.Message+"\n"+json, TipoResquisicao.Json, "Inserir", "atendimento");
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
                var retorno = await _atendimentoAgendamentoService.BuscarTodos();
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
        public async Task<IActionResult> GetById(int idAtendimentoAgendamento)
        {
            try
            {
                var retorno = await _atendimentoAgendamentoService.BuscarPorId(idAtendimentoAgendamento);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, $"GetById/{idAtendimentoAgendamento}", "atendimento");
                throw ex;
            }
        }

        [Route("DeletarPorId")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Detetar(int idAtendimentoAgendamento)
        {
            try
            {
                var retorno = await _atendimentoAgendamentoService.Deletar(idAtendimentoAgendamento);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, $"Detetar/{idAtendimentoAgendamento}", "atendimento");
                throw ex;
            }
        }

        [Route("BuscarPorUnidade")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarPorUnidade(int idUnidade)
        {
            try
            {
                var retorno = await _atendimentoAgendamentoService.BuscaPorUnidade(idUnidade);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("BuscarPorIdAtendimento")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarPorIdAtendimento(int idAtendimento)
        {
            try
            {
                var retorno = await _atendimentoAgendamentoService.BuscaPorIdAtendimento(idAtendimento);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("HistoricoAgendamentos")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> HistoricoAgendamentos(int idAtendimento)
        {
            try
            {
                var retorno = await _atendimentoAgendamentoService.HistoricoAgendamentos(idAtendimento);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("AtualizaAgendamento")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateAgendamento(DtoAtendimentoAgendamento dtoAtendimentoAgendamento)
        {
            try
            {            
                await _atendimentoAgendamentoService.UpdateAgendamento(dtoAtendimentoAgendamento);

                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("FiltraAgendamentosPorData")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> FiltraAgendamentosPorData(string data)
        {
            try
            {
                var dtoAtendimentoAgendamento = await _atendimentoAgendamentoService.FiltraAgendamentosPorData(data);

                return Ok(dtoAtendimentoAgendamento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}