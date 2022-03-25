using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EscolaPro.Core.Model.Enums
{
    public enum TipoPagamentoEnum
    {
        [Description("Cartão de Crédito")]
        CartaoCredito = 1,

        [Description("Cartão de Débito")]
        CartaoDebito = 2,

        [Description("Boleto Bancário")]
        BoletoBancario = 3,

        [Description("Transferência Bancária")]
        TransferenciaBancaria = 4,

        [Description("Dinheiro")]
        Dinheiro = 5,

        [Description("Guia de Pagamento")]
        GuiaPagamento = 6,

        [Description("Cobrança Recorrente")]
        CobrancaRecorrente = 7
    }
}
