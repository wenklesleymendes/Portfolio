using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscolaPro.Service.Concretes;
using EscolaPro.Service.Dto.TicketVO;
using EscolaPro.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssuntoTicketController : ControllerBase
    {
        IAssuntoTicketService _assuntoTicketService;

        public AssuntoTicketController(IAssuntoTicketService assuntoTicketService)
        {
            _assuntoTicketService = assuntoTicketService;
        }

        [Route("Cadastrar")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody]DtoAssuntoTicket assuntoTicket)
        {
            try
            {
                var retorno = await _assuntoTicketService.Inserir(assuntoTicket);

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
        public async Task<IActionResult> BuscarPorId(int idAssuntoTicket)
        {
            try
            {
                var retorno = await _assuntoTicketService.BuscarPorId(idAssuntoTicket);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [Route("BuscarPorUnidadeDepartamento")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarPorUnidadeDepartamento(int? idUnidade= null, int? idDepartamento=null)
        {
            try
            {
                var retorno = await _assuntoTicketService.BuscarPorUnidadeDepartamento(idUnidade,idDepartamento);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Route("BuscarTodos")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            var retorno = await _assuntoTicketService.BuscarTodos();

            return Ok(retorno);
        }

        [Route("Excluir")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Excluir(int id)
        {
            var retorno = await _assuntoTicketService.Deletar(id);

            return Ok(retorno);
        }
    }
}