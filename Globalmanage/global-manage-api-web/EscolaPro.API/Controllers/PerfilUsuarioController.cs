using EscolaPro.Service.Dto.ControleUsuarioVO;
using EscolaPro.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilUsuarioController : ControllerBase
    {
        IPerfilUsuarioService _perfilUsuarioService;

        public PerfilUsuarioController(IPerfilUsuarioService perfilUsuarioService)
        {
            _perfilUsuarioService = perfilUsuarioService;
        }

        [Route("Cadastrar")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody] DtoPerfilUsuario perfil)
        {
            try
            {
                if (perfil.Id == 0)
                {
                    if (!await _perfilUsuarioService.ConsultarPerfilExistente(perfil.PerfilSistemaEnum))
                    {
                        var retorno = await _perfilUsuarioService.Inserir(perfil);

                        return Ok(retorno);
                    }
                    else
                    {
                        return Ok(false);
                    }
                }
                else
                {
                    var retorno = await _perfilUsuarioService.Inserir(perfil);

                    return Ok(retorno);
                }
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
            var retorno = await _perfilUsuarioService.BuscarTodos();

            return Ok(retorno);
        }


        [Route("BuscarAtivos")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarTodosAtivos()
        {
            var retorno = await _perfilUsuarioService.BuscarTodosAtivos();

            return Ok(retorno);
        }

        [Route("BuscarPorId")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetById(int idPerfil)
        {
            var retorno = await _perfilUsuarioService.BuscarPorId(idPerfil);

            return Ok(retorno);
        }


        [Route("DesativarOuAtivar")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Desativar(int idPerfil)
        {
            var returno = await _perfilUsuarioService.DesativarOuAtivar(idPerfil);

            return Ok(returno);
        }

        [Route("Excluir")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Excluir(int idPerfil)
        {
            var retorno = await _perfilUsuarioService.Excluir(idPerfil);

            return Ok(retorno);
        }
    }
}