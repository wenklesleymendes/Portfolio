using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscolaPro.Service.Dto.MetasComissoesVO;
using EscolaPro.Service.Dto.MetasComissoesVO.Dashboard;
using EscolaPro.Service.Dto.TicketVO;
using EscolaPro.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetasController : ControllerBase
    {
        IMetasService _metasService;

        public MetasController(IMetasService metasService)
        {
            _metasService = metasService;
        }

        [Route("Cadastrar")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody]DtoMetas metas)
        {
            try
            {
                var retorno = await _metasService.Inserir(metas);

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
            var retorno = await _metasService.BuscarTodos();

            return Ok(retorno);
        }


        [Route("ListaNomeMetas")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> ListaNomeMetas()
        {
            var retorno = await _metasService.ListaNomeMetas();

            return Ok(retorno);
        }

        [Route("BuscarPorId")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarPorId(int idMeta)
        {
            try
            {
                var retorno = await _metasService.BuscarPorId(idMeta);

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
        public async Task<IActionResult> Filtrar(DtoFiltrarMeta filtrar)
        {
            try
            {
                var retorno = await _metasService.Filtrar(filtrar);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("ConsultarDashboard")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> ConsultarDashboard(DtoFiltrarMeta filtrar)
        {
            try
            {
                var retorno = await _metasService.ConsultarDashboard(filtrar);

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
        public async Task<IActionResult> Excluir(int idMeta)
        {
            var retorno = await _metasService.Excluir(idMeta);

            return Ok(retorno);
        }
    }
}