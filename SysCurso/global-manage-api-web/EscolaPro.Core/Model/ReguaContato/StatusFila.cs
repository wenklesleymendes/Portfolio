using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.ReguaContato
{
    public enum StatusFila
    {
        NaoEnviado=1,
        EmEmpera = 2,
        Enviando = 3,
        EnviadoComSucesso =4,
        ErroNoEnvio =5
    }
}
