using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscolaPro.Service.Dto.MatriculaAlunoVO;
using EscolaPro.Service.Interfaces;
using EscolaPro.Service.Interfaces.MatriculaAlunos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatriculaAlunoController : ControllerBase
    {
        IMatriculaAlunoService _matriculaAlunoService;
        IAlunoFinanceiroContratoService _alunoFinanceiroContratoService;
        ICursoService _cursoService;
        ITurmaService _turmaService;

        public MatriculaAlunoController(
            IMatriculaAlunoService matriculaAlunoService,
            IAlunoFinanceiroContratoService alunoFinanceiroContratoService,
            ICursoService cursoService,
            ITurmaService turmaService)
        {
            _matriculaAlunoService = matriculaAlunoService;
            _alunoFinanceiroContratoService = alunoFinanceiroContratoService;
            _cursoService = cursoService;
            _turmaService = turmaService;
        }

        [Route("MatricularAluno")]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody] DtoMatriculaAluno matriculaAluno)
        {
            try
            {
                var matriculas = await _matriculaAlunoService.BuscarMinhasMatriculas(matriculaAluno.AlunoId, matriculaAluno.UsuarioLogadoId);

                var curso = await _cursoService.BuscarPorId(matriculaAluno.CursoId);

                if (!curso.NacionatalTec)
                {
                    if (matriculas.Where(x => !x.NacionalTec && x.UnidadeId == matriculaAluno.UnidadeId && !string.IsNullOrEmpty(x.NumeroMatricula) && x.StatusMatricula).Count() > 0)
                    {
                        return Ok(false);
                    }
                    else
                    {
                        var dtoMatriculaAluno = await _matriculaAlunoService.MatricularAluno(matriculaAluno);

                        return Ok(dtoMatriculaAluno);
                    }
                }
                else
                {
                    var dtoMatriculaAluno = await _matriculaAlunoService.MatricularAluno(matriculaAluno);

                    return Ok(dtoMatriculaAluno);
                }
            }
            catch (Exception ex)
            {
                var json = JsonConvert.SerializeObject(matriculaAluno);
                RegistraLog.Log(ex.Message + "\n" + json, TipoResquisicao.Json, "MatricularAluno", "");
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, "MatricularAluno", "");
                throw ex;
            }
        }

        [Route("BuscarPorId")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarPorId(int matriculaId)
        {
            try
            {
                var retorno = await _matriculaAlunoService.BuscarPorId(matriculaId);

                var alunoFinanceiroGrid = await _alunoFinanceiroContratoService.ConsultarPainelFinanceiro(matriculaId);

                retorno.ExistePendenciaContrato = alunoFinanceiroGrid.ExistePendenciaContrato;
                retorno.ExistePendenciaFinanceira = !alunoFinanceiroGrid.ExistePendenciaFinanceira;

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
        public async Task<IActionResult> Excluir(int matriculaId)
        {
            var retorno = await _matriculaAlunoService.Excluir(matriculaId);

            return Ok(retorno);
        }

        [Route("BuscarMinhaTurma")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarMinhaTurma(int matriculaId)
        {
            var retorno = await _matriculaAlunoService.BuscarMinhaTurma(matriculaId);

            return Ok(retorno);
        }

        [Route("BuscarMinhasMatriculas")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarMinhasMatriculas(int alunoId, int usuarioLogadoId)
        {
            try
            {
                var retorno = await _matriculaAlunoService.BuscarMinhasMatriculas(alunoId, usuarioLogadoId);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("JaExistenteMatricula")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> JaExistenteMatricula(int alunoId, int usuarioLogadoId)
        {
            try
            {
                var retorno = await _matriculaAlunoService.JaExistenteMatricula(alunoId, usuarioLogadoId);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("ConsultarMeusProfessores")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> ConsultarMeusProfessores(int matriculaId)
        {
            try
            {
                var retorno = await _matriculaAlunoService.ConsultarMeusProfessores(matriculaId);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}