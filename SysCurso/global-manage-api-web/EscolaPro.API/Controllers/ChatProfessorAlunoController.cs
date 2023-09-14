using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscolaPro.Service.Dto.PortalAlunoProfessorVO;
using EscolaPro.Service.Interfaces.PortalAluno;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatProfessorAlunoController : ControllerBase
    {
        IMensagemAlunoProfessorService _mensagemAlunoProfessorService;

        public ChatProfessorAlunoController(IMensagemAlunoProfessorService mensagemAlunoProfessorService)
        {
            _mensagemAlunoProfessorService = mensagemAlunoProfessorService;
        }

        [Route("Cadastrar")]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody]DtoMensagemAlunoProfessor mensagemAlunoProfessor)
        {
            try
            {
                var usuario = await _mensagemAlunoProfessorService.Inserir(mensagemAlunoProfessor);

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
        public async Task<IActionResult> GetById(int mensagemId)
        {
            try
            {
                var retorno = await _mensagemAlunoProfessorService.BuscarPorId(mensagemId);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("BuscarPorMatricula")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarPorMatricula(int matriculaId)
        {
            try
            {
                var retorno = await _mensagemAlunoProfessorService.BuscarPorMatricula(matriculaId);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("BuscarPorProfessor")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarPorProfessor(int professorId)
        {
            try
            {
                var retorno = await _mensagemAlunoProfessorService.BuscarPorProfessor(professorId);

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
        public async Task<IActionResult> Excluir(int mensagemId)
        {
            try
            {
                var retorno = await _mensagemAlunoProfessorService.Excluir(mensagemId);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}