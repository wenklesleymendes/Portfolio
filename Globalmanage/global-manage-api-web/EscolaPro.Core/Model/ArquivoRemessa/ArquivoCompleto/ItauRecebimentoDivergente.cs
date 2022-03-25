using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.ArquivoRemessa
{
    public class ItauRecebimentoDivergente
    {
        public string tipo_autorizacao_recebimento { get; set; }
        public string tipo_valor_percentual_recebimento { get; set; }
        public string valor_minimo_recebimento { get; set; }
        public string percentual_minimo_recebimento { get; set; }
        public string valor_maximo_recebimento { get; set; }
        public string percentual_maximo_recebimento { get; set; }
    }
}
