using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.Pagamentos
{
    public enum TipoSituacaoEnum
    {
        Pago = 1,
        Aberto = 2,
        Isento = 3,
        Inadimplente = 4,
        InadimplenteBloqueado = 5,
        Residual = 6
    }
}
