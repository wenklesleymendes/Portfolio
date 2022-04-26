using EscolaPro.Core.Model.PainelMatricula.Enums;
using EscolaPro.Service.Dto.MatriculaAlunoVO;
using EscolaPro.Service.Interfaces.MatriculaAlunos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CancelamentoMatriculaController : Controller
    {
        ICancelamentoMatriculaService _cancelamentoMatriculaService;
        public CancelamentoMatriculaController(ICancelamentoMatriculaService cancelamentoMatriculaService)
        {
            _cancelamentoMatriculaService = cancelamentoMatriculaService;

        }

        [HttpGet]
        [Route("BuscarPorMatricula")]
        public async Task<IActionResult> BuscarPorMatricula(int matriculaId)
        {
            DtoCancelamentoMatriculaResult cancelamentoMatricula = await _cancelamentoMatriculaService.BuscarPorMatricula(matriculaId);
            return Ok(cancelamentoMatricula);
        }

        [HttpPost]
        [Route("EfetuarCancelamento")]
        public async Task<IActionResult> EfetuarCancelamento([FromBody] DtoCancelamentoMatriculaRequest cancelamentoMatricula)
        {
            var _cancelamentoMatricula = await _cancelamentoMatriculaService.EfetuarCancelamento(cancelamentoMatricula);

            if (cancelamentoMatricula.Validar)
            {
                string mensagem = await _cancelamentoMatriculaService.ValidarCancelamento(_cancelamentoMatricula);
                return Ok(new { cancelamentoMatricula = _cancelamentoMatricula, mensagem = mensagem });
            }
            else
                return Ok(_cancelamentoMatricula);
        }

        [HttpPost]
        [Route("SalvarAutorizacaoIsencao")]
        public async Task<IActionResult> SalvarAutorizacaoIsencao([FromBody] DtoCancelamentoAutorizacaoIsencao cancelamentoMatricula)
        {
            var _cancelamentoMatricula = await _cancelamentoMatriculaService.SalvarAutorizacaoIsencao(cancelamentoMatricula);

            return Ok(_cancelamentoMatricula);
        }

        [HttpPost]
        [Route("GerarMultaCancelamento")]
        public async Task<IActionResult> GerarMultaCancelamento([FromBody] DtoCancelamentoMatriculaRequest cancelamentoMatricula)
        {
            var _cancelamentoMatricula = await _cancelamentoMatriculaService.GerarMultaCancelamento(cancelamentoMatricula);

            return Ok(_cancelamentoMatricula);
        }


        [Route("GerarCartaCancelamento")]
        [HttpGet]
        public async Task<IActionResult> GerarCartaCancelamento(int matriculaId, int usuarioLogadoId, MotivoCancelamento motivoCancelamento)
        {
            try
            {
                var retorno = await _cancelamentoMatriculaService.GerarReportByte(matriculaId, usuarioLogadoId, motivoCancelamento);

                if (retorno == null)
                    return Ok();

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, "GerarSolicitacao", "Solicitação Aluno");
                throw ex;
            }
        }
    }
}
