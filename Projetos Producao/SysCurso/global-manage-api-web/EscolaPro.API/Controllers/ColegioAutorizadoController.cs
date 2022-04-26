using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscolaPro.Service.Dto.AgendaProvaVO;
using EscolaPro.Service.Interfaces.AgendaProvas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColegioAutorizadoController : ControllerBase
    {
        IColegioAutorizadoService _colegioAutorizadoService;

        public ColegioAutorizadoController(IColegioAutorizadoService colegioAutorizadoService)
        {
            _colegioAutorizadoService = colegioAutorizadoService;
        }

        [Route("Cadastrar")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody]DtoColegioAutorizado dtoColegioAutorizado)
        {
            try
            {
                var retorno = await _colegioAutorizadoService.Inserir(dtoColegioAutorizado);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("BuscarPorId")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetById(int idColegioAutorizado)
        {
            var retorno = await _colegioAutorizadoService.BuscarPorId(idColegioAutorizado);

            return Ok(retorno);
        }


        [Route("BuscarTodos")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            var retorno = await _colegioAutorizadoService.BuscarTodos();

            return Ok(retorno);
        }

        [Route("Excluir")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Excluir(int idColegioAutorizado)
        {
            var retorno = await _colegioAutorizadoService.Excluir(idColegioAutorizado);

            return Ok(retorno);
        }
    }
}