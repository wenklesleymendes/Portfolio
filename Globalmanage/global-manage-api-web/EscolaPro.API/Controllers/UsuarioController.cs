using EscolaPro.API.Dto.Usuario;
using EscolaPro.Core.Extensions;
using EscolaPro.Service.Dto.ControleUsuarioVO;
using EscolaPro.Service.Dto.UsuarioVO;
using EscolaPro.Service.Interfaces;
using EscolaPro.Service.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [Route("Login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Login([FromBody] DtoLoginRequest dto)
        {
            try
            {
                var usuario = await _usuarioService.Login(dto.UserName.ToLower(), dto.Senha.ToLower());

                if (usuario == null || usuario.Id == 0)
                    //return new StatusCodeResult(401);
                    return NotFound(new { message = "Usuário ou senha inválidos" });

                var token = TokenService.GenerateToken(usuario);

                usuario.Password = "";

                if (string.IsNullOrEmpty(token))
                {
                    return new StatusCodeResult(401);
                }

                return new
                {
                    user = usuario,
                    token = token,
                };
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [Route("Cadastrar")]
        [HttpPost]
        public async Task<ActionResult<dynamic>> InserirUsuario([FromBody] DtoUsuarioRequest dtoUsuario)
        {
            try
            {
                string senha = string.Empty;

                if (dtoUsuario.Id > 0)
                {
                    if (!string.IsNullOrEmpty(dtoUsuario.Password))
                    {
                        senha = Criptografia.CreateMD5(dtoUsuario.Password);
                    }
                }
                else
                {
                    senha = Criptografia.CreateMD5(dtoUsuario.Password);
                }

                dtoUsuario.Password = senha;

                var usuario = await _usuarioService.Inserir(dtoUsuario, false);

                if (usuario == null)
                    return new StatusCodeResult(401);
                //return NotFound(new { message = "Usuário ou senha inválidos" });

                var token = TokenService.GenerateToken(usuario);
                usuario.Password = "";

                if (string.IsNullOrEmpty(token))
                {
                    return new StatusCodeResult(401);
                }

                return new { user = usuario, token = token };
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Usuário já existente"))
                {
                    return Ok(false);
                }
                else
                {
                    return Ok(ex.Message);
                }
            }
        }


        [Route("BuscarTodos")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarTodos(int idUsuario)
        {
            var usuarios = await _usuarioService.BuscarTodos(idUsuario);

            return Ok(usuarios);
        }

        [Route("FiltrarUsuario")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> FiltrarUsuario(DtoFiltrarUsuario dtoFiltrarUsuario)
        {
            try
            {
                var usuario = await _usuarioService.FiltrarUsuario(dtoFiltrarUsuario);

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("BuscarPorUnidade")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> BuscarPorUnidade(DtoFiltrarUsuario dtoFiltrarUsuario)
        {
            try
            {
                var usuario = await _usuarioService.BuscarUsuarioPorUnidade(dtoFiltrarUsuario);

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("BuscarPorId")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetById(int idUsuario)
        {
            try
            {
                var retorno = await _usuarioService.BuscarPorId(idUsuario);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("DesativarOuAtivar")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Desativar(int idUsuario)
        {
            var returno = await _usuarioService.DesativarOuAtivar(idUsuario);

            return Ok(returno);
        }

        [Route("Excluir")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Excluir(int id)
        {
            var retorno = await _usuarioService.Excluir(id);

            return Ok(retorno);
        }


        [Route("EsqueciMinhaSenha")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> EsqueciMinhaSenha(string email)
        {
            try
            {
                var returno = await _usuarioService.EsqueciMinhaSenha(email);

                return Ok(returno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}