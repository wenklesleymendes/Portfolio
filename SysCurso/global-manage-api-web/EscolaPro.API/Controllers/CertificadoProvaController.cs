using EscolaPro.Service.Dto.MatriculaAlunoVO;
using EscolaPro.Service.Interfaces.ProvasCertificados;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificadoProvaController : ControllerBase
    {
        ICertificadoProvaService _certificadoProvaService;
        public CertificadoProvaController(ICertificadoProvaService certificadoProvaService)
        {
            _certificadoProvaService = certificadoProvaService;
        }

        [Route("Cadastrar")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Inserir(DtoCertificadoProva dtoProvaAluno)
        {
            try
            {
                var retorno = await _certificadoProvaService.Inserir(dtoProvaAluno);

                return Ok(retorno);
            }
            catch
            {
                throw;
            }
        }

        [Route("BuscarSolicitacaoAtual")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarSolicitacaoAtual(int matriculaId)
        {
            try
            {
                var retorno = await _certificadoProvaService.BuscarSolicitacaoAtual(matriculaId);

                return Ok(retorno);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        [Route("BuscarPorMatriculaId")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarPorMatriculaId(int matriculaId)
        {
            try
            {
                var retorno = await _certificadoProvaService.BuscarPorMatriculaId(matriculaId);

                return Ok(retorno);
            }
            catch
            {
                throw;
            }
        }

    }
}
