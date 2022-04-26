using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.RegistroCobrancaVO
{
    public class DtoRecebimentoDivergente
    {
        public string tipo_autorizacao_recebimento { get; set; }
        public string tipo_valor_percentual_recebimento { get; set; }
        public string valor_minimo_recebimento { get; set; }
        public string percentual_minimo_recebimento { get; set; }
        public string valor_maximo_recebimento { get; set; }
        public string percentual_maximo_recebimento { get; set; }
    }
}
