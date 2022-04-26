using EscolaPro.Core.Model.Enums;
using EscolaPro.Core.Model.Pagamentos;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.PagamentosVO
{
    public class DtoDadosCartao
    {
        public int Id { get; set; }
        public string NumeroCartao { get; set; }
        public string NomePessoa { get; set; }
        public string MesValidade { get; set; }
        public string AnoValidade { get; set; }
        public string BandeiraCartao { get; set; }
        public string CodigoSeguranca { get; set; }
        public AcquirersEnum AcquirersEnum { get; set; }
        public decimal ValorParcela { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorPago { get; set; }
        public int QuantidadeParcela { get; set; }
        public string TID { get; set; }
        public string CodigoAutorizacao { get; set; }
        public decimal Valor { get; set; }
        public decimal? Desconto { get; set; }
        public DateTime DataPagamento { get; set; }
        public TipoPagamentoEnum TipoPagamento { get; set; }
    }
}
