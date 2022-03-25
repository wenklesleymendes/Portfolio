using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscolaPro.Service.Dto.AlunosVO;
using EscolaPro.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NaturalidadeController : ControllerBase
    {
        INaturalidadeService _naturalidadeService;

        public NaturalidadeController(INaturalidadeService naturalidadeService)
        {
            _naturalidadeService = naturalidadeService;
        }

        [Route("Cadastrar")]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody]DtoNaturalidade naturalidade)
        {
            try
            {
                var usuario = await _naturalidadeService.Inserir(naturalidade);

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                var json = JsonConvert.SerializeObject(naturalidade);
                RegistraLog.Log(ex.Message + "\n" + json, TipoResquisicao.Json, "Inserir", "Anexo");
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, "Inserir", "Anexo");
                throw ex;
            }
        }

        [Route("BuscarTodos")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            var retorno = await _naturalidadeService.BuscarTodos();

            return Ok(retorno);
        }

        [Route("Excluir")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Excluir(int naturalidadeId)
        {
            var retorno = await _naturalidadeService.Excluir(naturalidadeId);

            return Ok(retorno);
        }
    }
}