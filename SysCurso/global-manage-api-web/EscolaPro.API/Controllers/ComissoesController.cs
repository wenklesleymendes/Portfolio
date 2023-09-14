using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscolaPro.Service.Dto.MetasComissoesVO;
using EscolaPro.Service.Dto.TicketVO;
using EscolaPro.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComissoesController : ControllerBase
    {
        IComissoesService _service;

        public ComissoesController(IComissoesService service)
        {
            _service = service;
        }

        [Route("Cadastrar")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody]DtoComissao comissao)
        {
            try
            {
                var retorno = await _service.Inserir(comissao);

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
            var retorno = await _service.BuscarTodos();

            return Ok(retorno);
        }


        [Route("Filtrar")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Filtrar(DtoFiltrar filtrar)
        {
            try
            {
                var retorno = await _service.Filtrar(filtrar);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("DashboardMinhasComissoes")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> DashboardMinhasComissoes(DtoFiltrar filtrar)
        {
            try
            {
                var retorno = await _service.DashboardMinhasComissoes(filtrar);

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
        public async Task<IActionResult> BuscarPorId(int idComissoes)
        {
            try
            {
                var retorno = await _service.BuscarPorId(idComissoes);

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
        public async Task<IActionResult> Excluir(int idComissoes)
        {
            var retorno = await _service.Excluir(idComissoes);

            return Ok(retorno);
        }
    }
}