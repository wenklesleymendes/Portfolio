using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagamentoController : ControllerBase
    {
        //IPagamentoService _alunoService;

        //public AlunoController(IAlunoService alunoService)
        //{
        //    _alunoService = alunoService;
        //}

        //[Route("Cadastrar")]
        //[HttpPost]        
        //public async Task<IActionResult> Inserir([FromBody]DtoAluno aluno)
        //{
        //    try
        //    {
        //        var usuario = await _alunoService.Inserir(aluno);

        //        return Ok(usuario);
        //    }
        //    catch (Exception ex)
        //    {
        //        var json = JsonConvert.SerializeObject(aluno);
        //        RegistraLog.Log(ex.Message + "\n" + json, TipoResquisicao.Json, "Inserir", "Anexo");
        //        RegistraLog.Log(ex.Message, TipoResquisicao.Error, "Inserir", "Anexo");
        //        throw ex;
        //    }
        //}
    }
}