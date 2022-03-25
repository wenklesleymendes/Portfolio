using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscolaPro.Service.Dto.AulaOnlineVO;
using EscolaPro.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerguntaController : ControllerBase
    {
        IPerguntaService _perguntaService;

        public PerguntaController(IPerguntaService perguntaService)
        {
            _perguntaService = perguntaService;
        }

        [Route("Cadastrar")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody]DtoPergunta dtoPergunta)
        {
            try
            {
                var retorno = await _perguntaService.Inserir(dtoPergunta);

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
        public async Task<IActionResult> GetById(int perguntaId)
        {
            try
            {
                var retorno = await _perguntaService.BuscarPorId(perguntaId);

                return Ok(retorno);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        [Route("BuscarPorVideoAula")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarPorVideoAula(int videoAulaId)
        {
            try
            {
                var retorno = await _perguntaService.BuscarPorVideoAula(videoAulaId);

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
        public async Task<IActionResult> Excluir(int perguntaId)
        {
            var retorno = await _perguntaService.Excluir(perguntaId);

            return Ok(retorno);
        }
    }
}