using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.RegistroCobrancaVO
{
    public class DtoBeneficiario
    {
        //public string cpf_cnpj_beneficiario { get; set; }
        //public string agencia_beneficiario { get; set; }
        //public string conta_beneficiario { get; set; }
        //public string digito_verificador_conta_beneficiario { get; set; }


        public string codigo_banco_beneficiario { get; set; }
        public string digito_verificador_banco_beneficiario { get; set; }
        public string agencia_beneficiario { get; set; }
        public string conta_beneficiario { get; set; }
        public string digito_verificador_conta_beneficiario { get; set; }
        public string cpf_cnpj_beneficiario { get; set; }
        public string nome_razao_social_beneficiario { get; set; }
        public string logradouro_beneficiario { get; set; }
        public string bairro_beneficiario { get; set; }
        public string complemento_beneficiario { get; set; }
        public string cidade_beneficiario { get; set; }
        public string uf_beneficiario { get; set; }
        public string cep_beneficiario { get; set; }
    }
}
