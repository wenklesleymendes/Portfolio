using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscolaPro.Service.Dto.EstoqueVO;
using EscolaPro.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstoqueController : ControllerBase
    {
        IEstoqueService _estoqueService;

        public EstoqueController(IEstoqueService estoqueService)
        {
            _estoqueService = estoqueService;
        }

        [Route("Cadastrar")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody]DtoProduto dtoProduto)
        {
            try
            {
                var retorno = await _estoqueService.Inserir(dtoProduto);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("AdicionarItem")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> AdicionarItem([FromBody]DtoItemProduto dtoItemProduto)
        {
            try
            {
                var retorno = await _estoqueService.AdicionarItem(dtoItemProduto);

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
        public async Task<IActionResult> GetById(int idProduto)
        {
            var retorno = await _estoqueService.BuscarPorId(idProduto);

            return Ok(retorno);
        }


        [Route("BuscarTodos")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            var retorno = await _estoqueService.BuscarTodos();

            return Ok(retorno);
        }

        [Route("BuscarHistoricoPorEstoque")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarHistoricoPorEstoque(int idEstoque)
        {
            var retorno = await _estoqueService.BuscarHistoricoPorEstoque(idEstoque);

            return Ok(retorno);
        }

        [Route("BuscarItemProdutoPorEstoque")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarItemProdutoPorEstoque(int idEstoque)
        {
            var retorno = await _estoqueService.BuscarItemProdutoPorEstoque(idEstoque);

            return Ok(retorno);
        }

        [Route("Excluir")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Excluir(int idProduto)
        {
            var retorno = await _estoqueService.Excluir(idProduto);

            return Ok(retorno);
        }

        [Route("ExcluirItem")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> ExcluirItem(int idItemProduto)
        {
            var retorno = await _estoqueService.ExcluirItem(idItemProduto);

            return Ok(retorno);
        }

        [Route("RetiradaApostila")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> RetiradaApostila(int idProduto)
        {
            var retorno = await _estoqueService.RetiradaApostila(idProduto);

            return Ok(retorno);
        }
    }
}