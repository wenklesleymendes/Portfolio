using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.RegistroCobrancaVO
{
    public class DtoDebito
    {
        public string agencia_debito { get; set; }
        public string conta_debito { get; set; }
        public string digito_verificador_conta_debito { get; set; }
    }
}
