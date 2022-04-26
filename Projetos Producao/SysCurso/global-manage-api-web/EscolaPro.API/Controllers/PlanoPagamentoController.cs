using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscolaPro.Service.Dto.PlanoPagamentoVO;
using EscolaPro.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanoPagamentoController : ControllerBase
    {
        IPlanoPagamentoService _planoPagamentoService;
        public PlanoPagamentoController(IPlanoPagamentoService planoPagamentoService)
        {
            _planoPagamentoService = planoPagamentoService;
        }

        [Route("Cadastrar")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody]DtoPlanoPagamento planoPagamento)
        {
            try
            {

                var retorno = await _planoPagamentoService.Inserir(planoPagamento);

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
        public async Task<IActionResult> GetById(int idPlanoPagamento)
        {
            var retorno = await _planoPagamentoService.BuscarPorIdAlterar(idPlanoPagamento);

            return Ok(retorno);
        }

        [Route("BuscarPorIdCurso")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetByIdCurso(int idCurso)
        {
            var retorno = await _planoPagamentoService.BuscarPorIdCurso(idCurso);

            return Ok(retorno);
        }

        [Route("BuscarPlanoPagamento")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarPlanoPagamento(int formaPagamento, int? quantidadeParcela, int cursoId, int unidadeId)
        {
            try
            {
                var retorno = await _planoPagamentoService.BuscarPlanoPagamento(formaPagamento, quantidadeParcela, cursoId, unidadeId);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("BuscarPorCursoUnidade")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarPorCursoUnidade(int cursoId, int unidadeId)
        {
            try
            {
                var retorno = await _planoPagamentoService.BuscarPorCursoUnidade(cursoId, unidadeId);

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
            var retorno = await _planoPagamentoService.BuscarTodos();

            return Ok(retorno);
        }


        [Route("DesativarOuAtivar")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Desativar(int idPlanoPagamento)
        {
            var returno = await _planoPagamentoService.AtivarOuDesativar(idPlanoPagamento);

            return Ok(returno);
        }

        [Route("Excluir")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Excluir(int id)
        {
            var retorno = await _planoPagamentoService.Deletar(id);

            return Ok(retorno);
        }
    }
}