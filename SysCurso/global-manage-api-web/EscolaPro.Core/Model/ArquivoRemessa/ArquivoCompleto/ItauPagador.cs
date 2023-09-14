using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.ArquivoRemessa
{
    public class ItauPagador
    {
        public string cpf_cnpj_pagador { get; set; }
        public string nome_pagador { get; set; }
        public string logradouro_pagador { get; set; }
        public string bairro_pagador { get; set; }
        public string cidade_pagador { get; set; }
        public string uf_pagador { get; set; }
        public string cep_pagador { get; set; }
        public List<ItauGrupoEmailPagador> grupo_email_pagador { get; set; }
    }
}
