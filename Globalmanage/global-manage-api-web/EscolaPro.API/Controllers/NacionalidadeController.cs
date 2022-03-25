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
    public class NacionalidadeController : ControllerBase
    {
        INacionalidadeService _NacionalidadeService;

        public NacionalidadeController(INacionalidadeService NacionalidadeService)
        {
            _NacionalidadeService = NacionalidadeService;
        }

        [Route("Cadastrar")]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody]DtoNacionalidade Nacionalidade)
        {
            try
            {
                var usuario = await _NacionalidadeService.Inserir(Nacionalidade);

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                var json = JsonConvert.SerializeObject(Nacionalidade);
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
            var retorno = await _NacionalidadeService.BuscarTodos();

            return Ok(retorno);
        }

        [Route("Excluir")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Excluir(int NacionalidadeId)
        {
            var retorno = await _NacionalidadeService.Excluir(NacionalidadeId);

            return Ok(retorno);
        }
    }
}