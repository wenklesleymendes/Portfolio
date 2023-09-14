using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscolaPro.Service.Dto.AulaOnlineVO;
using EscolaPro.Service.Interfaces.AulasOnlineVimeo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriaOnlineController : ControllerBase
    {
        IMateriaOnlineService _materiaOnlineService;

        public MateriaOnlineController(IMateriaOnlineService materiaOnlineService)
        {
            _materiaOnlineService = materiaOnlineService;
        }

        [Route("Cadastrar")]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody]DtoMateriaOnline dtoMateriaOnline)
        {
            try
            {
                var usuario = await _materiaOnlineService.Inserir(dtoMateriaOnline);

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                var json = JsonConvert.SerializeObject(dtoMateriaOnline);
                RegistraLog.Log(ex.Message + "\n" + json, TipoResquisicao.Json, "Inserir", "Anexo");
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, "Inserir", "Anexo");
                throw ex;
            }
        }


        [Route("BuscarPorId")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetById(int materiaOnlineId)
        {
            var retorno = await _materiaOnlineService.BuscarPorId(materiaOnlineId);

            return Ok(retorno);
        }


        [Route("BuscarPorAulaOnline")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarPorAulaOnline(int aulaOnlineId)
        {
            var retorno = await _materiaOnlineService.BuscarPorAulaOnline(aulaOnlineId);

            return Ok(retorno);
        }

        [Route("BuscarPorMateria")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarPorMateria(int materiaId)
        {
            var retorno = await _materiaOnlineService.BuscarPorMateria(materiaId);

            return Ok(retorno);
        }

        [Route("BuscarTodos")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            try
            {
                var retorno = await _materiaOnlineService.BuscarTodos();

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
        public async Task<IActionResult> Excluir(int materiaId)
        {
            var retorno = await _materiaOnlineService.Excluir(materiaId);

            return Ok(retorno);
        }


        [Route("BuscarMateriasPorCurso")]
        [HttpGet]
        public async Task<IActionResult> BuscarMateriasPorCurso(int cursoId)
        {
            var retorno = await _materiaOnlineService.BuscarMateriasPorCurso(cursoId);

            return Ok(retorno);
        }
    }
}