using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.ArquivoRemessa
{
    public class ItauMulta
    {
        public string data_multa { get; set; }
        public int tipo_multa { get; set; }
        public string valor_multa { get; set; }
        public string percentual_multa { get; set; }
    }
}
