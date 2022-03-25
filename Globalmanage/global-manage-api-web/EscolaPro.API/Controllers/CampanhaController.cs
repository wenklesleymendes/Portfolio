using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscolaPro.Core.Model;
using EscolaPro.Service.Dto.CampanhaVO;
using EscolaPro.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampanhaController : ControllerBase
    {
        ICampanhaService _campanhaService;

        public CampanhaController(ICampanhaService campanhaService)
        {
            _campanhaService = campanhaService;
        }

        [Route("Cadastrar")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody]DtoCampanha dtoCampanha)
        {
            try
            {
                var retorno = await _campanhaService.Inserir(dtoCampanha);

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
        public async Task<IActionResult> GetById(int idCampanha)
        {
            var retorno = await _campanhaService.BuscarPorId(idCampanha);

            return Ok(retorno);
        }


        [Route("BuscarTodos")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            var retorno = await _campanhaService.BuscarTodos();

            return Ok(retorno);
        }


        [Route("BuscarCampanhaVigente")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarCampanhaVigente(int unidadeId, int cursoId, int tipoPagamento)
        {
            try
            {
                var retorno = await _campanhaService.BuscarCampanhaVigente(unidadeId, cursoId, tipoPagamento);

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
            var retorno = await _campanhaService.Deletar(id);

            return Ok(retorno);
        }

        [Route("DesativarOuAtivar")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Desativar(int idCampanha)
        {
            var returno = await _campanhaService.AtivarOuDesativar(idCampanha);

            return Ok(returno);
        }
    }
}