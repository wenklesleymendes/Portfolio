using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EscolaPro.Core.Model.Enums
{
    public enum TipoParcelaEnum
    {
        [Description("Única")]
        Unica = 1,
        [Description("Parcela")]
        Parcelada = 2,
        [Description("Despesa Recorrente")]
        DespesaRecorrente = 3
    }
}
