using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using EscolaPro.Core.Helpers;
using EscolaPro.Service.Dto.MatriculaAlunoVO;
using EscolaPro.Service.Dto.MatriculaAlunoVO.SolicitacaoVO;
using EscolaPro.Service.Interfaces;
using EscolaPro.Service.Interfaces.MatriculaAlunos;
using FastReport;
using FastReport.Export.PdfSimple;
using FastReport.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitacaoAlunoController : ControllerBase
    {
        ISolicitacaoService _solicitacaoService;
        ISolicitacaoAlunoService _solicitacaoAlunoService;
        IMatriculaAlunoService _matriculaAlunoService;
        IUnidadeService _unidadeService;
        IAlunoService _alunoService;
        IUsuarioService _usuarioService;
        IEmailSenderService _emailSenderService;

        private readonly IWebHostEnvironment _webHostingEnvironment;

        public SolicitacaoAlunoController(
            ISolicitacaoService solicitacaoService,
            ISolicitacaoAlunoService solicitacaoAlunoService,
            IMatriculaAlunoService matriculaAlunoService,
            IUnidadeService unidadeService,
            IAlunoService alunoService,
            IUsuarioService usuarioService,
            IEmailSenderService emailSenderService,
            IWebHostEnvironment webHostingEnvironment)
        {
            _solicitacaoService = solicitacaoService;
            _solicitacaoAlunoService = solicitacaoAlunoService;
            _matriculaAlunoService = matriculaAlunoService;
            _webHostingEnvironment = webHostingEnvironment;
            _unidadeService = unidadeService;
            _alunoService = alunoService;
            _usuarioService = usuarioService;
            _emailSenderService = emailSenderService;
        }

        [Route("EfetuarSolicitacao")]
        [HttpPost]
        public async Task<IActionResult> EfetuarSolicitacao([FromBody] DtoSolicitacaoEfetuar dtoSolicitacao)
        {
            try
            {
                var solicitacao = await _solicitacaoAlunoService.EfetuarSolicitacao(dtoSolicitacao);

                return Ok(solicitacao);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, "EfetuarSolicitacao", "SolicitacaoAluno");
                throw ex;
            }
        }

        [Route("HistoricoSolicitacao")]
        [HttpGet]
        public async Task<IActionResult> HistoricoSolicitacao(int matriculaId)
        {
            try
            {
                var solicitacoes = await _solicitacaoAlunoService.BuscarHistorico(matriculaId);

                return Ok(solicitacoes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("GerarSolicitacao")]
        [HttpGet]
        public async Task<IActionResult> GerarSolicitacao(int solicitacaoId, int usuarioLogadoId, int matriculaId)
        {
            try
            {
                var retorno = await _solicitacaoAlunoService.GerarReportByte(solicitacaoId, usuarioLogadoId, matriculaId);

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
