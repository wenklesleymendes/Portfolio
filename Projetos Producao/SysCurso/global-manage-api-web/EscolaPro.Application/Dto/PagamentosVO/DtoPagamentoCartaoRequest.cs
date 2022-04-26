using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.PagamentosVO
{
    public class DtoPagamentoCartaoRequest
    {
        public bool capture { get; set; }
        public string reference { get; set; }
        public string amount { get; set; }
        public string cardholderName { get; set; }
        public string cardNumber { get; set; }
        public int expirationMonth { get; set; }
        public int expirationYear { get; set; }
        public string securityCode { get; set; }
        public int installments { get; set; }
    }
}
