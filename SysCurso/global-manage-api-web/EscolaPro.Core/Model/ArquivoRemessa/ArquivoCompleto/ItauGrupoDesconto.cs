using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.ArquivoRemessa
{
    public class ItauGrupoDesconto
    {
        public string data_desconto { get; set; }
        public int tipo_desconto { get; set; }
        public string valor_desconto { get; set; }
        public string percentual_desconto { get; set; }
    }
}
