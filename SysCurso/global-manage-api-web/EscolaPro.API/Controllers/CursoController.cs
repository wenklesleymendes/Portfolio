using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscolaPro.Core.Model;
using EscolaPro.Core.Model.CursoTurma;
using EscolaPro.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        ICursoService _cursoService;

        public CursoController(ICursoService cursoService)
        {
            _cursoService = cursoService;
        }

        [Route("Cadastrar")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody] Curso curso)
        {
            var retorno = await _cursoService.Inserir(curso);

            return Ok(retorno);
        }

        [Route("CadastrarMaterias")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> CadastrarMaterias([FromBody] Materia materia)
        {
            var retorno = await _cursoService.InserirMateria(materia);

            return Ok(retorno);
        }

        [Route("BuscarTodos")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            try
            {
                var retorno = await _cursoService.BuscarTodos();

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message + "\n", TipoResquisicao.Json, "Inserir", "Unidade");
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, "Inserir", "Unidade");
                throw ex;
            }
        }

        [Route("BuscarPorId")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarPorId(int idCurso)
        {
            try
            {
                var retorno = await _cursoService.BuscarPorId(idCurso);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [Route("DesativarOuAtivar")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Desativar(int idCurso)
        {
            var returno = await _cursoService.AtivarOuDesativar(idCurso);

            return Ok(returno);
        }

        [Route("BuscarCursosComMaterias")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarCursosComMateria()
        {
            var retorno = await _cursoService.BuscarCursosComMateria();

            return Ok(retorno);
        }

        [Route("Excluir")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Excluir(int id)
        {
            var retorno = await _cursoService.Deletar(id);

            return Ok(retorno);
        }

        [Route("ExcluirMateria")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> ExcluirMateria(int idMateria)
        {
            var retorno = await _cursoService.DeletarMateria(idMateria);

            return Ok(retorno);
        }

        [Route("BuscarPorUnidade")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarPorUnidade(int unidadeId, int? usuarioLogadoId)
        {
            try
            {
                var retorno = await _cursoService.BuscarPorUnidade(unidadeId, usuarioLogadoId);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message + "\n", TipoResquisicao.Json, "CursoController", "BuscarPorUnidade");
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, "CursoController", "BuscarPorUnidade");
                throw ex;
            }
        }
    }
}