using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using EscolaPro.Core.Model;
using EscolaPro.Service.Dto.AlunosVO;
using EscolaPro.Service.Interfaces;
using EscolaPro.Service.Interfaces.MatriculaAlunos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EscolaPro.API.Controllers
{
    //[EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        IAlunoService _alunoService;
        IMatriculaAlunoService _matriculaAlunoService;

        public AlunoController(IAlunoService alunoService, IMatriculaAlunoService matriculaAlunoService)
        {
            _alunoService = alunoService;
            _matriculaAlunoService = matriculaAlunoService;
        }

        [Route("Cadastrar")]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody] DtoAluno aluno)
        {
            try
            {
                var usuario = await _alunoService.Inserir(aluno);

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                var json = JsonConvert.SerializeObject(aluno);
                RegistraLog.Log(ex.Message + "\n" + json, TipoResquisicao.Json, "Inserir", "Anexo");
                throw ex;
            }
        }


        [Route("BuscarPorId")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetById(int idAluno)
        {
            try
            {
                var retorno = await _alunoService.BuscarPorId(idAluno);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [Route("BuscarPorCPF")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarPorCPF(string cpf)
        {
            try
            {
                var retorno = await _alunoService.BuscarPorCPF(cpf);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message + "\n", TipoResquisicao.Json, $"BuscarPorCPF(string {cpf})", "Aluno");
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
                var retorno = await _alunoService.BuscarTodos();

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message + "\n", TipoResquisicao.Json, "BuscarTodos()", "Aluno");
                throw ex;
            }
        }

        [Route("FiltrarAluno")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> FiltrarAluno(DtoFiltrarAluno filtrarAluno)
        {
            try
            {
                var retorno = await _alunoService.FiltrarAluno(filtrarAluno);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                var json = JsonConvert.SerializeObject(filtrarAluno);
                RegistraLog.Log(ex.Message + "\n" + json, TipoResquisicao.Json, "FiltrarAluno", "Aluno");
                throw ex;
            }
        }

        [Route("Excluir")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Excluir(int idAluno)
        {
            try
            {
                var retorno = await _alunoService.Excluir(idAluno);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("SelecionarFoto")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> SelecionarFoto(int idAluno)
        {
            try
            {
                var retorno = await _alunoService.SelecionarFoto(idAluno);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message + "\n" + idAluno.ToString(), TipoResquisicao.Json, "SelecionarFoto", "Aluno");
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
                var retorno = await _alunoService.UploadFoto(aluno.Foto, aluno.AlunoId.Value, aluno.Extensao);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                var json = JsonConvert.SerializeObject(aluno);
                RegistraLog.Log(ex.Message + "\n" + json, TipoResquisicao.Json, "UploadFoto", "Aluno");
                throw ex;
            }
        }

        [Route("RemoverFoto")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> RemoverFoto(int idAluno)
        {
            try
            {
                var retorno = await _alunoService.ExcluirFoto(idAluno);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                var json = JsonConvert.SerializeObject(idAluno);
                RegistraLog.Log(ex.Message + "\n" + json, TipoResquisicao.Json, "RemoverFoto", "Aluno");
                throw ex;
            }
        }
    }
}
