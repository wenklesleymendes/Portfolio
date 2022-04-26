using EscolaPro.API.Dto;
using EscolaPro.Core.Model.PainelMatricula;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Service.Concretes;
using EscolaPro.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricoProvasController : ControllerBase
    {
        IHistoricoProvasService _historicoProvasService;

        public HistoricoProvasController(IHistoricoProvasService historicoProvasService)
        {
            _historicoProvasService = historicoProvasService;
        }

        [Route("ListaColegioAutorizadoExcel")]
        [HttpPost]
        public async Task<IActionResult> ListaColegioAutorizadoExcel(FiltroHistoricoProvas? filtro)
        {
            try
            {
                var retorno = await _historicoProvasService.ListaColegioAutorizadoExcel(filtro);

                return Ok(retorno);
            }
            catch (ArgumentException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message + "\n", TipoResquisicao.Json, "HistoricoProvas", "ListaColegioAutorizadoExcel");
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, "HistoricoProvas", "ListaColegioAutorizadoExcel");
                throw ex;
            }
        }

        [Route("ListaGeralDeInscritosParaProvaExcel")]
        [HttpPost]
        public async Task<IActionResult> ListaGeralDeInscritosParaProvaExcel(FiltroHistoricoProvas? filtro)
        {
            try
            {
                var retorno = await _historicoProvasService.ListaGeralDeInscritosParaProvaExcel(filtro);

                return Ok(retorno);
            }
            catch (ArgumentException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message + "\n", TipoResquisicao.Json, "HistoricoProvas", "ListaGeralDeInscritosParaProvaExcel");
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, "HistoricoProvas", "ListaGeralDeInscritosParaProvaExcel");
                throw ex;
            }
        }

        [Route("ListaDeChamadaOnibusExcel")]
        [HttpPost]
        public async Task<IActionResult> ListaDeChamadaOnibusExcel(FiltroHistoricoProvas? filtro)
        {
            try
            {
                var retorno = await _historicoProvasService.ListaDeChamadaOnibusExcel(filtro);

                return Ok(retorno);
            }
            catch (ArgumentException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message + "\n", TipoResquisicao.Json, "HistoricoProvas", "ListaDeChamadaOnibusExcel");
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, "HistoricoProvas", "ListaDeChamadaOnibusExcel");
                throw ex;
            }
        }


        [Route("NumeroOnibus")]
        [HttpGet]
        public IActionResult NumeroOnibus(int idUnidade, string dataInicioMatricula, string dataFimMatricula)
        {
            try
            {
                var retorno = _historicoProvasService.NumeroOnibus(idUnidade, dataInicioMatricula, dataFimMatricula);

                return Ok(retorno);
            }
            catch (ArgumentException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message + "\n", TipoResquisicao.Json, "HistoricoProvas", "ListaDeChamadaOnibusExcel");
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, "HistoricoProvas", "ListaDeChamadaOnibusExcel");
                throw ex;
            }
        }
    }
}
