using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.ArquivoRemessa
{
    public class ItauBeneficiario
    {
        public string cpf_cnpj_beneficiario { get; set; }
        public string agencia_beneficiario { get; set; }
        public string conta_beneficiario { get; set; }
        public string digito_verificador_conta_beneficiario { get; set; }
    }
}
