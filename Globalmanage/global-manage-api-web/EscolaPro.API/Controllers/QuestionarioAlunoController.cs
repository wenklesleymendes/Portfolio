using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscolaPro.Service.Dto.AlunoQuestionarioProvaVO;
using EscolaPro.Service.Interfaces.AulasOnlineVimeo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionarioAlunoController : ControllerBase
    {
        IAlunoQuestionarioService _alunoQuestionarioService; 

        public QuestionarioAlunoController(IAlunoQuestionarioService alunoQuestionarioService)
        {
            _alunoQuestionarioService = alunoQuestionarioService;
        }

        [Route("ResponderQuestionario")]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody]List<DtoAlunoQuestionario> dtoAlunoQuestionarios)
        {
            try
            {
                var usuario = await _alunoQuestionarioService.Inserir(dtoAlunoQuestionarios);

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("BuscarPorPerguntaId")]
        [HttpGet]
        public async Task<IActionResult> BuscarPorPerguntaId(int perguntaId)
        {
            try
            {
                var usuario = await _alunoQuestionarioService.BuscarPorPerguntaId(perguntaId);

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}