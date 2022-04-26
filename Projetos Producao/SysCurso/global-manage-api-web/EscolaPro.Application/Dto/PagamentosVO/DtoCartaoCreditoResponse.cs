using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.PagamentosVO
{
    public class DtoCartaoCreditoResponse
    {
        public string Reference { get; set; }
        public string Tid { get; set; }
        public string Nsu { get; set; }
        public string AuthorizationCode { get; set; }
        public DateTime? DateTime { get; set; }
        public int Amount { get; set; }
        public int? Installments { get; set; }
        public string CardBin { get; set; }
        public string Last4 { get; set; }
        public object ThreeDSecure { get; set; }
        public List<Link> Links { get; set; }
        public string ReturnCode { get; set; }
        public string ReturnMessage { get; set; }
    }

    public class Link
    {
        public string Method { get; set; }
        public string Rel { get; set; }
        public string Href { get; set; }
    }


    public class DtoPagamentoCreditoResponse
    {
        public TipoRetornoCartaoCredito RetornoCartao { get; set; }
    }

}
