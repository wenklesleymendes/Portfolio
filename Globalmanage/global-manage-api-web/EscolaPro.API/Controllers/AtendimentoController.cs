using EscolaPro.Core.Model;
using EscolaPro.Core.Model.Atendimentos;
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

        [Route("Atualizar")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateAtendimento([FromBody] DtoAtendimento atendimento)
        {
            try
            {
                await _atendimentoService.UpdateAtendimento(atendimento);
                
                return Ok();
            }
            catch (Exception ex)
            {
                var json = JsonConvert.SerializeObject(atendimento);
                RegistraLog.Log(ex.Message + "\n" + json, TipoResquisicao.Json, "Inserir", "atendimento");
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, "Inserir", "atendimento");
                throw ex;
            }
        }

        [Route("Editar")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> EditarAtendimento([FromBody] DtoAtendimento atendimento)
        {
            try
            {
                await _atendimentoService.EditaAtendimento(atendimento);
                return Ok();
            }
            catch (Exception ex)
            {
                var json = JsonConvert.SerializeObject(atendimento);
                RegistraLog.Log(ex.Message + "\n" + json, TipoResquisicao.Json, "Inserir", "atendimento");
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, "Inserir", "atendimento");
                throw ex;
            }
        }

        [Route("AutualizarStatus")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> AutualizarStatus(int idAtendimento)
        {
            try
            {
                var codigoEmExecucao = StatusAtendimento.EmExecucao.GetHashCode();
                var retorno  = await _atendimentoService.AtualizaStatus(idAtendimento, codigoEmExecucao);
                return Ok();
            }
            catch (Exception ex)
            {
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

        [Route("BuscaIdPorNumerodeCelular")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetByCelular(string celularCliente)
        {
            try
            {
                var retorno = await _atendimentoService.BuscaIdPorNumerodeCelular(celularCliente);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, $"GetByCelular/{celularCliente}", "atendimento");
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

        [Route("BuscaNumeroDeAtendimentos")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscaNumeroDeAtendimentos(int idUnidade)
        {
            try
            {
                var retorno = await _atendimentoService.ContaAtendimentosExecutar(idUnidade);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, 
                    $"BuscaNumeroDeAtendimentos/{idUnidade}", "atendimento");
                throw ex;
            }
        }

        [Route("FiltraAtendimentos")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> FiltraAtendimentos(FiltroAtendimentos filtro)
        {
            try
            {
                var retorno = await _atendimentoService.FiltraAtendimentos(filtro);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, $"FiltraAtendimentos/{filtro}", "atendimento");
                throw ex;
            }
        }
    }
}