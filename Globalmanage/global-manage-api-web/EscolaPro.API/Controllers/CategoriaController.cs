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
    public class CategoriaController : ControllerBase
    {
        ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [Route("Cadastrar")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody]DtoCategoria dtoCategoria)
        {
            try
            {
                var retorno = await _categoriaService.Inserir(dtoCategoria);

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
        public async Task<IActionResult> GetById(int idCategoria)
        {
            var retorno = await _categoriaService.BuscarPorId(idCategoria);

            return Ok(retorno);
        }


        [Route("BuscarTodos")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            var retorno = await _categoriaService.BuscarTodos();

            return Ok(retorno);
        }

        [Route("Excluir")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Excluir(int idCategoria)
        {
            var retorno = await _categoriaService.Excluir(idCategoria);

            return Ok(retorno);
        }
    }
}