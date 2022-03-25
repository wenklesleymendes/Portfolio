using EscolaPro.Core.Model.Enums;
using EscolaPro.Core.Model.Pagamentos;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.PagamentosVO
{
   public class BaixaManualDto
    {
        public List<int> PagamentoIds { get; set; }
        public decimal ValorPago { get; set; }
        public DateTime DataPagamento { get; set; }
        public TipoPagamentoEnum TipoPagamento { get; set; }
        public string NumeroCartao { get; set; }
        public string BandeiraCartao { get; set; }
        public string CodigoAutorizacao { get; set; }
        public AcquirersEnum? Acquirer { get; set; }
        public int? QuantidadeParcela { get; set; }
        public decimal? ValorParcela { get; set; }

    }
}
