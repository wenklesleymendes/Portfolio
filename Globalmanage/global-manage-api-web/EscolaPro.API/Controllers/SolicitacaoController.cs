using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscolaPro.Service.Dto.AlunosVO;
using EscolaPro.Service.Dto.MatriculaAlunoVO;
using EscolaPro.Service.Interfaces.MatriculaAlunos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitacaoController : ControllerBase
    {
        ISolicitacaoService _solicitacaoService;

        public SolicitacaoController(ISolicitacaoService solicitacaoService)
        {
            _solicitacaoService = solicitacaoService;
        }

        [Route("Cadastrar")]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody]DtoSolicitacao dtoSolicitacao)
        {
            try
            {
                var usuario = await _solicitacaoService.Inserir(dtoSolicitacao);

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                var json = JsonConvert.SerializeObject(dtoSolicitacao);
                RegistraLog.Log(ex.Message + "\n" + json, TipoResquisicao.Json, "Solicitacao", "Anexo");
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, "Solicitacao", "Anexo");
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
                var retorno = await _solicitacaoService.BuscarTodos();

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("BuscarPorCursoId")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarPorCursoId(int cursoId, int matriculaId, int usuarioId)
        {
            try
            {
                var retorno = await _solicitacaoService.BuscarPorCursoId(cursoId, matriculaId, usuarioId);

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
        public async Task<IActionResult> GetById(int solicitacaoId)
        {
            var retorno = await _solicitacaoService.BuscarPorId(solicitacaoId);

            return Ok(retorno);
        }


        [Route("Excluir")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Excluir(int solicitacaoId)
        {
            var retorno = await _solicitacaoService.Excluir(solicitacaoId);

            return Ok(retorno);
        }

        [Route("SelecionarFoto")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> SelecionarFoto(int solicitacaoId)
        {
            try
            {
                var retorno = await _solicitacaoService.SelecionarFoto(solicitacaoId);

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
        public async Task<IActionResult> UploadFoto(DtoAlunoFoto aluno)
        {
            try
            {
                var retorno = await _solicitacaoService.UploadFoto(aluno.Foto, aluno.SolicitacaoId.Value, aluno.Extensao);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                var json = JsonConvert.SerializeObject(aluno);
                RegistraLog.Log(ex.Message + "\n" + json, TipoResquisicao.Json, this.GetType().FullName, "");
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, this.GetType().FullName, "");
                throw ex;
            }
        }

        [Route("RemoverFoto")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> RemoverFoto(int solicitacaoId)
        {
            try
            {
                var retorno = await _solicitacaoService.ExcluirFoto(solicitacaoId);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}