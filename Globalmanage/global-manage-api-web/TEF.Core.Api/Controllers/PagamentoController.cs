using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using TEF.Core.Api.Models;
using TEF.Core.Library;
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
            try
            {
                if (dtoTransacao.PagamentoIds == null)
                {
                    string valor = dtoTransacao.ValorTotal.ToString();

                    valor = valor.Insert(valor.Length - 2, ",");

                    dtoTransacao.ValorTotal = Convert.ToDecimal(valor);
                }
                else
                {
                    dtoTransacao.ValorTotal = Math.Round(dtoTransacao.ValorTotal, 2, MidpointRounding.AwayFromZero);
                }

                var codigo = await InicializadorTEF.EfetuarTransacao(dtoTransacao);
                
                return Ok(new TEFResponse { Codigo = codigo });
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
}
