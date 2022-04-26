using EscolaPro.Service.Dto.AlunosVO;
using EscolaPro.Service.Dto.UnidadeVO;
using EscolaPro.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    //[EnableCors("CorsApi")]
    [ApiController]
    public class UnidadeController : ControllerBase
    {
        IUnidadeService _unidadeService;
        public UnidadeController(IUnidadeService unidadeService)
        {
            _unidadeService = unidadeService;
        }

        [Route("Cadastrar")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody]DtoUnidadeRequest unidade)
        {
            try
            {
                var retorno = await _unidadeService.Inserir(unidade);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                var json = JsonConvert.SerializeObject(unidade);
                RegistraLog.Log(ex.Message+"\n"+json, TipoResquisicao.Json, "Inserir", "Unidade");
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, "Inserir", "Unidade");
                throw ex;
            }
        }

        [Route("BuscarTodos")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarTodos(int usuarioLogadoId)
        {
            try
            {
                var retorno = await _unidadeService.BuscarTodos(usuarioLogadoId);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, "BuscarTodos", "Unidade");
                throw ex;
            }
        }

        [Route("BuscarUnidadesTicket")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarUnidadesTicket(int usuarioLogadoId)
        {
            try
            {
                var retorno = await _unidadeService.BuscarUnidadesTicket(usuarioLogadoId);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, "BuscarTodos", "Unidade");
                throw ex;
            }
        }

        [Route("BuscarPorId")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetById(int idUnidade)
        {
            try
            {
                var retorno = await _unidadeService.BuscarPorId(idUnidade);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, $"GetById/{idUnidade}", "Unidade");
                throw ex;
            }
        }

        [Route("DeletarPorId")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Detetar(int idUnidade)
        {
            try
            {
                var retorno = await _unidadeService.Deletar(idUnidade);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, $"Detetar/{idUnidade}", "Unidade");
                throw ex;
            }
        }


        [Route("SelecionarFoto")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> SelecionarFoto(int unidadeId)
        {
            try
            {
                var retorno = await _unidadeService.SelecionarFoto(unidadeId);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("UploadFoto")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> UploadFoto(DtoAlunoFoto unidade)
        {
            try
            {
                var retorno = await _unidadeService.UploadFoto(unidade.Foto, unidade.UnidadeId.Value, unidade.Extensao);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("RemoverFoto")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> RemoverFoto(int unidadeId)
        {
            try
            {
                var retorno = await _unidadeService.ExcluirFoto(unidadeId);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}