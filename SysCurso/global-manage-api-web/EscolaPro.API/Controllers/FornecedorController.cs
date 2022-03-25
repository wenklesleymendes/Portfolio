using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscolaPro.Service.Dto.FornecedorVO;
using EscolaPro.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FornecedorController : ControllerBase
    {
        IFornecedorService _fornecedorService;

        public FornecedorController(IFornecedorService fornecedorService)
        {
            _fornecedorService = fornecedorService;
        }

        [Route("Cadastrar")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody]DtoFornecedor dtoFornecedor)
        {
            try
            {
                var retorno = await _fornecedorService.Inserir(dtoFornecedor);

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
        public async Task<IActionResult> GetById(int idFornecedor)
        {
            var retorno = await _fornecedorService.BuscarPorId(idFornecedor);

            return Ok(retorno);
        }


        [Route("BuscarTodos")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            var retorno = await _fornecedorService.BuscarTodos();

            return Ok(retorno);
        }

        [Route("Excluir")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Excluir(int idFornecedor)
        {
            var retorno = await _fornecedorService.Excluir(idFornecedor);

            return Ok(retorno);
        }

        [Route("DesativarOuAtivar")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Desativar(int idFornecedor)
        {
            var returno = await _fornecedorService.DesativarOuAtivar(idFornecedor);

            return Ok(returno);
        }
    }
}