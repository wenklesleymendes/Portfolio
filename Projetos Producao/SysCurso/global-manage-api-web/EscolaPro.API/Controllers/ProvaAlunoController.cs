using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscolaPro.Service.Dto.MatriculaAlunoVO;
using EscolaPro.Service.Interfaces.ProvasCertificados;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvaAlunoController : ControllerBase
    {
        IProvaAlunoService _provaAlunoService;
        IProvaMateriaAlunoService _provaMateriaAlunoService;

        public ProvaAlunoController(IProvaAlunoService provaAlunoService,
                                    IProvaMateriaAlunoService provaMateriaAlunoService)
        {
            _provaAlunoService = provaAlunoService;
            _provaMateriaAlunoService = provaMateriaAlunoService;
        }

        [Route("Cadastrar")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody] DtoProvaAlunoRequest dtoProvaAluno)
        {
            try
            {
                var retorno = await _provaAlunoService.Inserir(dtoProvaAluno);

                await _provaAlunoService.TicketEnviar(dtoProvaAluno.MatriculaAlunoId, dtoProvaAluno.UsuarioLogadoId);

                return Ok(retorno);
            }
            catch
            {
                throw;
            }
        }

        [Route("AtualizarStatusProva")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> AtualizarStatusProva(DtoProvaAluno dtoProvaAluno)
        {
            try
            {
                var retorno = await _provaAlunoService.AtualizarStatusProva(dtoProvaAluno);
                DateTime dataAt = DateTime.Now;
                foreach (var item in dtoProvaAluno.ProvaMateriaAluno)
                {
                    item.ProvaAlunoId = retorno.Id;
                    item.UpdatedAt = dataAt;
                }
                if (await _provaMateriaAlunoService.Inserir(dtoProvaAluno.ProvaMateriaAluno) != dtoProvaAluno.ProvaMateriaAluno.Count())
                    new Exception();

                return Ok(retorno);
            }
            catch
            {
                throw;
            }
        }

        [Route("BuscarPorId")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetById(int provaAlunoId)
        {
            try
            {
                var retorno = await _provaAlunoService.BuscarPorId(provaAlunoId);

                return Ok(retorno);
            }
            catch
            {
                throw;
            }
        }

        [Route("BuscarPorMatriculaId")]
        //[Authorize]
        [HttpGet]
        public IActionResult BuscarPorMatriculaId(int matriculaId)
        {
            var retorno = _provaAlunoService.BuscarPorMatriculaId(matriculaId);

            return Ok(retorno);
        }

        [Route("InformacoesCadastro")]
        [HttpGet]
        public async Task<IActionResult> InformacoesCadastro(int matriculaId)
        {
            var retorno = await _provaAlunoService.InformacoesCadastro(matriculaId);
            return Ok(retorno);
        }

        [Route("CadastrarCredenciais")]
        [HttpPost]
        public async Task<IActionResult> CadastrarCredenciais(DtoProvaAlunoRequest credenciais)
        {
            var retorno = await _provaAlunoService.CadastrarCredenciais(credenciais);
            return Ok(retorno);
        }

        [Route("BuscarProvasDisponiveis")]
        [HttpGet]
        public IActionResult BuscarProvasDisponiveis(int colegioId, int cursoId, int unidadeId)
        {
            var ret = _provaAlunoService.BuscarProvasDisponiveis(colegioId, cursoId, unidadeId);
            return Ok(ret);
        }

        [Route("CancelarInscricao")]
        [HttpGet]
        public async Task<IActionResult> CancelarInscricao(int provaAlunoId)
        {
            await _provaMateriaAlunoService.ExcluirProvaMateria(provaAlunoId);

            var retorno = await _provaAlunoService.CancelarInscricao(provaAlunoId);

            return Ok(retorno);
        }

        [Route("ImprimirFormulario")]
        [HttpGet]
        public async Task<IActionResult> ImprimirFormulario(int provaAlunoId)
        {
            var retorno = await _provaAlunoService.ImprimirFormulario(provaAlunoId);

            return Ok(retorno);
        }

        [Route("BuscarProvasRealizadas")]
        [HttpGet]
        public IActionResult BuscarProvasRealizadas(int matriculaId)
        {
            var retorno = _provaAlunoService.BuscarProvasRealizadas(matriculaId);
            foreach (var item in retorno)
            {
                if (item.UnidadeTransporteProva != null && item.UnidadeTransporteProva.provaAlunos != null)
                    item.UnidadeTransporteProva.provaAlunos.Clear();
            }

            return Ok(retorno);
        }
    }
}