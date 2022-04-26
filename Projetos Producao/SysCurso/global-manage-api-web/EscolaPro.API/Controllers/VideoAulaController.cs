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
    public class VideoAulaController : ControllerBase
    {
        private readonly IVideoAulaService _videoAulaService;
        private readonly IMapper _mapper;

        public VideoAulaController(
            IVideoAulaService videoAulaService,
            IMapper mapper)
        {
            _videoAulaService = videoAulaService;
            _mapper = mapper;
        }

        [Route("Cadastrar")]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody]DtoVideoAula videoAula)
        {
            try
            {
                var usuario = await _videoAulaService.Inserir(videoAula);

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
        public async Task<IActionResult> BuscarPorCurso(int aulaOnlineId)
        {
            var retorno = await _videoAulaService.BuscarPorId(aulaOnlineId);

            return Ok(retorno);
        }

        [Route("BuscarPorMateria")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarTodos(int materiaId)
        {
            try
            {
                var retorno = await _videoAulaService.BuscarPorMateria(materiaId);

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
        public async Task<IActionResult> Excluir(int aulaOnlineId)
        {
            try
            {
                var retorno = await _videoAulaService.Excluir(aulaOnlineId);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("BuscarUltimaSessao")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarUltimaSessao(int matriculaId)
        {
            try
            {
                var retorno = await _videoAulaService.BuscarUltimaSessao(matriculaId);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [Route("SalvarUltimaSessao")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> SalvarUltimaSessao([FromBody]DtoVideoPausado dtoVideoPausado)
        {
            try
            {
                var retorno = await _videoAulaService.SalvarUltimaSessao(dtoVideoPausado);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}