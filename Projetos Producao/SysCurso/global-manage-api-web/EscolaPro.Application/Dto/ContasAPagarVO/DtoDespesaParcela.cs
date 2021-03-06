using EscolaPro.Core.Model.Enums;
using EscolaPro.Core.Model.MetasComissoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.ContasAPagarVO
{
    public class DtoDespesaParcela
    {
        public int Id { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public decimal ValorParcela { get; set; }
        public TipoPagamentoEnum TipoPagamento { get; set; }
        public int DespesaId { get; set; }
        public StatusPagamentoEnum? StatusPagamento { get; set; }
        public decimal? ValorPago { get; set; }
        public double? DescontoTaxa { get; set; }
        public decimal? Juros { get; set; }
        public string Observacao { get; set; }
        public string CodigoBarras { get; set; }
        public bool IsActive { get; set; }
        public bool LancamentoManual { get; set; }
    }
}
