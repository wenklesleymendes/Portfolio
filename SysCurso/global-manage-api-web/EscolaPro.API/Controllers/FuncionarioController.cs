using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using EscolaPro.API.Dto.Funcionarios;
using EscolaPro.Service.Dto.ControlePontoVO;
using EscolaPro.Service.Dto.FuncionarioVO;
using EscolaPro.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        IFuncionarioService _funcionarioService;
        IControlePontoService _controlePontoService;
        IFeriasFuncionarioService _feriasFuncionarioService;

        public FuncionarioController(
            IFuncionarioService funcionarioService,
            IControlePontoService controlePontoService,
            IFeriasFuncionarioService feriasFuncionarioService)
        {
            _funcionarioService = funcionarioService;
            _controlePontoService = controlePontoService;
            _feriasFuncionarioService = feriasFuncionarioService;
        }

        [Route("Cadastrar")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody]DtoFuncionario dtoFuncionario)
        {
            try
            {
                var retorno = await _funcionarioService.Inserir(dtoFuncionario);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("AtualizarPontoEletronico")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> AtualizarPontoEletronico([FromBody]DtoControlePontoHorario dtoControlePonto)
        {
            try
            {
                var retorno = await _controlePontoService.Atualizar(dtoControlePonto);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("CadastrarFerias")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> CadastrarFerias([FromBody]DtoFeriasFuncionario dtoFeriasFuncionario)
        {
            try
            {
                var retorno = await _feriasFuncionarioService.ConcederFerias(dtoFeriasFuncionario);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("BuscarTodos")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> BuscarTodosPorFiltro(FiltrarFuncionario filtrar)
        {
            try
            {
                var retorno = await _funcionarioService.BuscarTodosPorFiltro(filtrar.UnidadeId, filtrar.Nome, filtrar.Ativo, filtrar.CPF, filtrar.DataInicioTerminoContrato, filtrar.DataFimTerminoContrato);

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
        public async Task<IActionResult> BuscarPorId(int idFuncionario)
        {
            try
            {
                var retorno = await _funcionarioService.BuscarPorId(idFuncionario);

                return Ok(retorno);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        [Route("BuscarPorCPF")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarPorCPF(string cpf)
        {
            try
            {
                var retorno = await _funcionarioService.BuscarPorCPF(cpf);

                return Ok(retorno);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        [Route("BuscarPontoEletronico")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarPontoEletronico(string cpf, DateTime? dataInicio, DateTime? dataFim)
        {
            try
            {
                if (!dataInicio.HasValue)
                {
                    dataInicio = DateTime.Now.AddDays(-15);
                }

                if (!dataFim.HasValue)
                {
                    dataFim = DateTime.Now.AddDays(15);
                }

                var retorno = await _controlePontoService.BuscarPorCPF(cpf, dataInicio.Value, dataFim.Value);

                return Ok(retorno);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Route("BuscarFeriasPorFuncionario")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarFeriasPorFuncionario(int idFuncionario)
        {
            try
            {
                var retorno = await _feriasFuncionarioService.BuscarTodosPorFuncionario(idFuncionario);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("BuscarDetalhamentoFerias")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarDetalhamentoFerias(int idFuncionario)
        {
            try
            {
                var retorno = await _feriasFuncionarioService.BuscarDetalhamentoFerias(idFuncionario);

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
        public async Task<IActionResult> Excluir(int idFuncionario)
        {
            var retorno = await _funcionarioService.Deletar(idFuncionario);

            return Ok(retorno);
        }

        [Route("ExcluirFerias")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> ExcluirFerias(int idFerias)
        {
            var retorno = await _feriasFuncionarioService.DeletarFerias(idFerias);

            return Ok(retorno);
        }

        [Route("DesativarOuAtivar")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Desativar(int idFuncionario)
        {
            var returno = await _funcionarioService.AtivarOuDesativar(idFuncionario);

            return Ok(returno);
        }


       
    }
}

