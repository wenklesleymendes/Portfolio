using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.ArquivoRemessa
{
    public class ItauDebito
    {
        public string agencia_debito { get; set; }
        public string conta_debito { get; set; }
        public string digito_verificador_conta_debito { get; set; }
    }
}
