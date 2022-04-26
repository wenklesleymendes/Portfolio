using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EscolaPro.Core.Helpers;
using EscolaPro.Core.Model;
using EscolaPro.Core.Model.ArquivoRemessa.ArquivoSimples;
using EscolaPro.Core.Model.Pagamentos;
using EscolaPro.Repository.Interfaces.Pagamentos;
using EscolaPro.Service.Dto;
using EscolaPro.Service.Dto.MatriculaAlunoVO.PlanoAlunoVO;
using EscolaPro.Service.Dto.PagamentosVO;
using EscolaPro.Service.Interfaces;
using EscolaPro.Service.Interfaces.MatriculaAlunos;
using EscolaPro.Service.Interfaces.Pagamentos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace EscolaPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoFinanceiroContratoController : ControllerBase
    {
        IAlunoFinanceiroContratoService _alunoFinanceiroContratoService;
        IAlunoFinanceiroContratoRepository _alunoFinanceiroContratoRepository;
        private readonly AppSettings _appSettings;

        public AlunoFinanceiroContratoController(
            IAlunoFinanceiroContratoService alunoFinanceiroContratoService,
            IAlunoFinanceiroContratoRepository alunoFinanceiroContratoRepository, 
            IOptions<AppSettings> appSettings)
        {
            _alunoFinanceiroContratoService = alunoFinanceiroContratoService;
            _alunoFinanceiroContratoRepository = alunoFinanceiroContratoRepository;
            _appSettings = appSettings.Value;
        }


        [Route("ContratarPlano")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> ContratarPlano([FromBody] DtoContratarPlano dtoContratarPlano)
        {
            try
            {
                if (_appSettings.UsuarioLogadoTesteProcessos == 0)
                {
                    _appSettings.UsuarioLogadoTesteProcessos = 1;
                    var retorno = await _alunoFinanceiroContratoService.ContratarPlano(dtoContratarPlano);
                    return Ok(retorno);
                }
                else
                {
                    await Task.Delay(3000);
                    var retorno = await _alunoFinanceiroContratoService.ContratarPlano(dtoContratarPlano);
                    _appSettings.UsuarioLogadoTesteProcessos = 0;
                    return Ok(retorno);
                }
            }
            catch (ArgumentException ex)
            {
                RegistraLog.Log(JsonConvert.SerializeObject(ex), TipoResquisicao.Json, "ContratarPlano", "");
                RegistraLog.Log(JsonConvert.SerializeObject(ex), TipoResquisicao.Error, "ContratarPlano", "");
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(JsonConvert.SerializeObject(ex), TipoResquisicao.Json, "ContratarPlano", "");
                RegistraLog.Log(JsonConvert.SerializeObject(ex), TipoResquisicao.Error, "ContratarPlano", "");
                Debug.Write(ex);
                throw ex;
            }
        }

        [Route("GerarBoletoResidual")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> GerarBoletoResidual([FromBody] DtoGerarBoletoRequest dtoGerarBoleto)
        {
            try
            {
                var retorno = await _alunoFinanceiroContratoService.GerarPagamentoResidual(dtoGerarBoleto);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("ConsultarPainelFinanceiro")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> ConsultarPainelFinanceiro(int matriculaId)
        {
            try
            {
                var retorno = await _alunoFinanceiroContratoService.ConsultarPainelFinanceiro(matriculaId);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("ConsultarEmail")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> ConsultarEmail(int pagamentoId)
        {
            try
            {
                var retorno = await _alunoFinanceiroContratoService.ConsultarEmail(pagamentoId);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                var json = JsonConvert.SerializeObject(pagamentoId);
                RegistraLog.Log(ex.Message + "\n" + json, TipoResquisicao.Json, "EnviarBoletoPorEmailOuRecalcular", "");
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, "EnviarBoletoPorEmailOuRecalcular", "");
                throw ex;
            }
        }

        [Route("EnviarBoletoPorEmail")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> EnviarBoletoPorEmail([FromBody] DtoGerarBoletoRequest dtoGerarBoleto)
        {
            try
            {
                var retorno = await _alunoFinanceiroContratoService.GerarBoletoEnviarPorEmail(dtoGerarBoleto.AlunoId, dtoGerarBoleto.PagamentoIds, TipoAcaoBoletoEnum.EnviarPorEmail);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message + "\n", TipoResquisicao.Json, "Inserir", "Anexo");
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, "Inserir", "Anexo");
                throw ex;
            }
        }

        [Route("EnviarBoletoPorEmailOuRecalcular")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> EnviarBoletoPorEmailOuRecalcular([FromBody] DtoGerarBoletoRequest dtoGerarBoleto)
        {
            try
            {
                var retorno = await _alunoFinanceiroContratoService.EnviarBoletoPorEmailOuRecalcular(dtoGerarBoleto);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                var json = JsonConvert.SerializeObject(dtoGerarBoleto);
                RegistraLog.Log(ex.Message + "\n" + json, TipoResquisicao.Json, "EnviarBoletoPorEmailOuRecalcular", "");
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, "EnviarBoletoPorEmailOuRecalcular", "");
                throw;
            }
        }

        [Route("EfetuarPagamentoCartaoCredito")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> EfetuarPagamentoAPIAdquirente([FromBody] DtoPagamentoCartaoCredito dtoPagamentoCartaoCredito)
        {
            try
            {
                var retorno = await _alunoFinanceiroContratoService.EfetuarPagamentoAPIAdquirente(dtoPagamentoCartaoCredito);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message + "\n", TipoResquisicao.Json, "Inserir", "Anexo");
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, "Inserir", "Anexo");
                throw ex;
            }
        }

        [Route("BuscarDetalhePagamento")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> BuscarDetalhePagamento(int pagamentoId)
        {
            try
            {
                var retorno = await _alunoFinanceiroContratoService.BuscarDetalhePagamento(pagamentoId);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("EfetuarPagamentoTEF")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> EfetuarPagamentoTEF([FromBody] DtoPagamentoCartaoCredito dtoPagamentoCartaoCredito)
        {
            try
            {
                List<DtoPagamento> pagamentos = new List<DtoPagamento>();

                foreach (var pagamentoId in dtoPagamentoCartaoCredito.PagamentoIds)
                {
                    pagamentos.Add(new DtoPagamento { Id = pagamentoId });
                }

                var retorno = await _alunoFinanceiroContratoService.GerarPagamentos(
                    new DtoAlunoFinanceiroGrid
                    {
                        Pagamento = pagamentos,
                        Credito = dtoPagamentoCartaoCredito.Credito,
                        TEF = dtoPagamentoCartaoCredito.TEF,
                        Total = dtoPagamentoCartaoCredito.ValorTotal,
                        NumeroCartao = dtoPagamentoCartaoCredito.NumeroCartao,
                        NumeroControle = dtoPagamentoCartaoCredito.NumeroControle,
                       QuantidadeParcela = dtoPagamentoCartaoCredito.QuantidadeParcela,
                       ComprovanteCartao = dtoPagamentoCartaoCredito.ComprovanteCartao
                    });


                await _alunoFinanceiroContratoService.TicketEnviar(dtoPagamentoCartaoCredito.MatriculaId.Value, dtoPagamentoCartaoCredito.UsuarioLogadoId, dtoPagamentoCartaoCredito.PagamentoIds.ToArray());

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                RegistraLog.Log(ex.Message + "\n", TipoResquisicao.Json, "Inserir", "Anexo");
                RegistraLog.Log(ex.Message, TipoResquisicao.Error, "Inserir", "Anexo");
                throw ex;
            }
        }


        [Route("ConsultarComprovante")]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> ConsultarComprovante(int pagamentoId)
        {
            try
            {
                var retorno = await _alunoFinanceiroContratoService.ConsultarComprovante(pagamentoId);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}