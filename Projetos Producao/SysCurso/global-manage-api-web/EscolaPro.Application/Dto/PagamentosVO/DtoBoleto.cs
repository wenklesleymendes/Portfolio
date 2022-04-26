using System;

namespace EscolaPro.Service.Dto.PagamentosVO
{
    public class DtoBoleto
    {
        public string NossoNumero { get; set; }
        public DateTime DataCredito { get; set; }
        public decimal ValorPagoCredito { get; set; }
        public decimal ValorTarifas { get; set; }
        public decimal ValorDesconto { get; set; }
        public string DescricaoMovimentoRetorno { get; set; }
    }
}
