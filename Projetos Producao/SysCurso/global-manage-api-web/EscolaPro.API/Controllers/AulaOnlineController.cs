using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EscolaPro.Service.Dto.AulaOnlineVO;
using EscolaPro.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AulaOnlineController : ControllerBase
    {
        private readonly IAulaOnlineService _aulaOnlineService;
        private readonly IMapper _mapper;

        public AulaOnlineController(
            IAulaOnlineService aulaOnlineService,
            IMapper mapper)
        {
            _aulaOnlineService = aulaOnlineService;
            _mapper = mapper;
        }

        [Route("Cadastrar")]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody] DtoAulaOnline aulaOnline)
        {
            try
            {
                var usuario = await _aulaOnlineService.Inserir(aulaOnline);

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [Route("BuscarPorId")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarPorId(int aulaOnlineId)
        {
            var retorno = await _aulaOnlineService.BuscarPorId(aulaOnlineId);

            return Ok(retorno);
        }

        [Route("BuscarMaterias")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarMaterias(int aulaOnlineId)
        {
            try
            {
                var retorno = await _aulaOnlineService.BuscarMaterias(aulaOnlineId);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [Route("BuscarTodos")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            var retorno = await _aulaOnlineService.BuscarTodos();

            return Ok(retorno);
        }

        [Route("BuscarPorCurso")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarPorCurso(int cursoId)
        {
            try
            {
                var retorno = await _aulaOnlineService.BuscarPorCurso(cursoId);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("MinhasAulasOnline")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> MinhasAulasOnline(int matriculaId)
        {
            try
            {
                var retorno = await _aulaOnlineService.MinhasAulasOnline(matriculaId);

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
        public async Task<IActionResult> Excluir(int cursoId)
        {
            var retorno = await _aulaOnlineService.Excluir(cursoId);

            return Ok(retorno);
        }
    }
}