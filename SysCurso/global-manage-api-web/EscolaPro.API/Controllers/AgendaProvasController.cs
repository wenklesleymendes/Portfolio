using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EscolaPro.Service.Dto.AgendaProvaVO;
using EscolaPro.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendaProvasController : ControllerBase
    {
        IAgendaProvaService _agendaProvaService;

        public AgendaProvasController(IAgendaProvaService agendaProvaService)
        {
            _agendaProvaService = agendaProvaService;
        }

        [Route("Cadastrar")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody]DtoAgendaProva dtoAgendaProva)
        {
            try
            {
                var retorno = await _agendaProvaService.Inserir(dtoAgendaProva);

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
        public async Task<IActionResult> GetById(int idAgendaProva)
        {
            var retorno = await _agendaProvaService.BuscarPorId(idAgendaProva);

            return Ok(retorno);
        }

        [Route("BuscarProvasDisponiveis")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarProvasDisponiveis(int colegioId, int unidadeId, int cursoId)
        {
            var retorno = await _agendaProvaService.BuscarProvasDisponiveis(colegioId, unidadeId, cursoId);

            return Ok(retorno);
        }

        [Route("BuscarTodos")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            try
            {
                var retorno = await _agendaProvaService.BuscarTodos();

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
        public async Task<IActionResult> Excluir(int idAgendaProva)
        {
            var retorno = await _agendaProvaService.Excluir(idAgendaProva);

            return Ok(retorno);
        }
    }
}