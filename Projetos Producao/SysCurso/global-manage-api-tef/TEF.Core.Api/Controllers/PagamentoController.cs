using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TEF.Core.Library;
using TEF.ImpressaoCupom;
using static TEF.Core.Library.InicializadorTEF;

namespace TEF.Core.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PagamentoController : ControllerBase
    {

        [Route("EfetuarPagamento")]
        [HttpPost]
        public async Task<IActionResult> EfetuarPagamento([FromBody] DtoTransacaoTEF dtoTransacao)
        {
            if (dtoTransacao.ValorTotal.ToString().Length == 1)
            {
                dtoTransacao.ValorTotal = decimal.Parse("0,0" + dtoTransacao.ValorTotal.ToString());
            }
            else if (dtoTransacao.ValorTotal.ToString().Length == 2)
            {
                dtoTransacao.ValorTotal = decimal.Parse("0," + dtoTransacao.ValorTotal.ToString());
            }
            else
            {
                string valor = dtoTransacao.ValorTotal.ToString();

                valor = valor.Insert(valor.Length - 2, ",");

                dtoTransacao.ValorTotal = Convert.ToDecimal(valor);
            }

            var codigo = await InicializadorTEF.EfetuarTransacao(dtoTransacao);

            return Ok(new TEFResponse { Codigo = codigo });
        }

        [Route("ImprimirComprovante")]
        [HttpPost]
        public async Task<IActionResult> ImprimirComprovante([FromBody] ImprimirComprovante imprimirComprovante)
        {
            try
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                var porta = configuration.GetSection("portaImpressora").Value;

                CupomImpressao.Imprimir(imprimirComprovante.Comprovante, porta);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("DesfazerTransacaoCartao")]
        [HttpPost]
        public async Task<IActionResult> DesfazerTransacao(DesfazerTransacao dtoTransacao)
        {
            try
            {
                int retorno = await InicializadorTEF.DesfazimentoUltimaTransacao(dtoTransacao.NumeroControle);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

    public class TEFResponse
    {
        public int Codigo { get; set; }
    }

    public class ImprimirComprovante
    {
        public string Comprovante { get; set; }
        public int PagamentoId { get; set; }
    }
}
