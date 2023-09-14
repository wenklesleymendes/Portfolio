using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscolaPro.Core.Model;
using EscolaPro.Service.Dto;
using EscolaPro.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurmaController : ControllerBase
    {
        ITurmaService _turmaService;

        public TurmaController(ITurmaService turmaService)
        {
            _turmaService = turmaService;
        }

        [Route("Cadastrar")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody] DtoTurma turma)
        {
            try
            {
                var retorno = await _turmaService.Inserir(turma);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                var json = JsonConvert.SerializeObject(turma);
                RegistraLog.Log(ex.Message + "\n" + json, TipoResquisicao.Json, "Inserir", "Turma");
                throw ex;
            }
        }

        [Route("TransferirDeTurma")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> TransferirDeTurma([FromBody] DtoTransferirTurma turma)
        {
            try
            {
                var retorno = await _turmaService.TransferirDeTurma(turma);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                var json = JsonConvert.SerializeObject(turma);
                RegistraLog.Log(ex.Message + "\n" + json, TipoResquisicao.Json, "TransferirDeTurma", "Turma");
                throw ex;
            }
        }

        [Route("Filtrar")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Filtrar(DtoTurmaFiltrar turma)
        {
            try
            {
                var retorno = await _turmaService.Filtrar(turma);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                var json = JsonConvert.SerializeObject(turma);
                RegistraLog.Log(ex.Message + "\n" + json, TipoResquisicao.Json, "Filtrar", "Turma");
                throw ex;
            }
        }

        [Route("BuscarPorId")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetById(int idTurma)
        {
            try
            {
                var retorno = await _turmaService.BuscarPorId(idTurma);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message + "\n" + idTurma.ToString(), TipoResquisicao.Json, "Filtrar", "Turma");
                throw ex;
            }
        }

        [Route("BuscarTurmasDisponiveis")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarPorCursoId(int cursoId, int unidadeId, int? usuarioLogadoId)
        {
            try
            {
                var retorno = await _turmaService.BuscarPorCursoId(cursoId, unidadeId, usuarioLogadoId);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message + "\n", TipoResquisicao.Json, "Filtrar", "Turma");
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
                var retorno = await _turmaService.BuscarTodos();

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message + "\n", TipoResquisicao.Json, "BuscarTodos", "Turma");
                throw ex;
            }
        }

        [Route("Excluir")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Excluir(int id)
        {
            try
            {
                var retorno = await _turmaService.Deletar(id);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message + "\n", TipoResquisicao.Json, "Excluir", "Turma");
                throw ex;
            }
        }
    }
}