using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscolaPro.Service.Dto.DocumentosAlunoVO;
using EscolaPro.Service.Interfaces.Documentos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InconsistenciaDocumentoController : ControllerBase
    {
        IInconsistenciaDocumentoService _inconsistenciaDocumentoService;

        public InconsistenciaDocumentoController(IInconsistenciaDocumentoService inconsistenciaDocumentoService)
        {
            _inconsistenciaDocumentoService = inconsistenciaDocumentoService;
        }

        [Route("Cadastrar")]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody] DtoInconsistenciaDocumentoRequest dtoInconsistencia)
        {
            try
            {
                var usuario = await _inconsistenciaDocumentoService.Inserir(dtoInconsistencia);

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                var json = JsonConvert.SerializeObject(dtoInconsistencia);
                RegistraLog.Log(ex.Message + "\n" + json, TipoResquisicao.Json, "Inserir", "InconsistenciaDocumento");
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, "Inserir", "InconsistenciaDocumento");
                throw ex;
            }
        }

        [Route("BuscarTodos")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarTodos(int id)
        {
            try
            {
                var retorno = await _inconsistenciaDocumentoService.BuscarTodos(id);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("BuscarPorMatriculaId")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarPorMatriculaId(int matriculaId)
        {
            try
            {
                var retorno = await _inconsistenciaDocumentoService.BuscarPorMatriculaId(matriculaId);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("Excluir")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Excluir(int inconsistenciaDocumentoId)
        {
            try
            {
                var retorno = await _inconsistenciaDocumentoService.Excluir(inconsistenciaDocumentoId);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
