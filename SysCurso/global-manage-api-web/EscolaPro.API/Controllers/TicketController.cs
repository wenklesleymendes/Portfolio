using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscolaPro.Service.Dto.TicketVO;
using EscolaPro.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [Route("Cadastrar")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody]DtoTicket ticket)
        {
            try
            {
                var retorno = await _ticketService.Inserir(ticket);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("ResponderTicket")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> ResponderTicket([FromBody]DtoMensagemTicket mensagemTicket)
        {
            try
            {
                var retorno = await _ticketService.ResponderTicket(mensagemTicket);

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
        public async Task<IActionResult> BuscarPorId(int idTicket)
        {
            try
            {
                var retorno = await _ticketService.BuscarPorId(idTicket);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [Route("Filtrar")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Filtrar(DtoFiltrarTicket dtoFiltrarTicket)
        {
            try
            {
                var retorno = await _ticketService.Filtrar(dtoFiltrarTicket);

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
            try
            {
                var retorno = await _ticketService.BuscarTodos();

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
        public async Task<IActionResult> Excluir(int id)
        {
            var retorno = await _ticketService.Deletar(id);

            return Ok(retorno);
        }

        [Route("ConsultarDashBoard")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> ConsultarDashBoard(int usuarioLogadoId)
        {
            try
            {
                var retorno = await _ticketService.ConsultarDashBoard(usuarioLogadoId);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("BuscarMensagensTicket")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarMensagensTicket(int ticketId)
        {
            try
            {
                var retorno = await _ticketService.BuscarMensagens(ticketId);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}