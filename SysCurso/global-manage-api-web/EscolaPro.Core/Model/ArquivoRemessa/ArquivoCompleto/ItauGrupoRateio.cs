using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.ArquivoRemessa
{
    public class ItauGrupoRateio
    {
        public string agencia_grupo_rateio { get; set; }
        public string conta_grupo_rateio { get; set; }
        public string digito_verificador_conta_grupo_rateio { get; set; }
        public int tipo_rateio { get; set; }
        public string valor_percentual_rateio { get; set; }
    }
}
