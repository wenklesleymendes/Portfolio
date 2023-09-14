using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EscolaPro.Core.Model.PainelMatricula.Enums
{
    public enum StatusCancelamento
    {
        [Description("A Confirmar")]
        AConfirmar = 1,
        [Description("Aguardando Pagamento")]
        AguardandoPagamento = 2,
        [Description("Efetivado")]
        Efetivado = 3,
    }
}
