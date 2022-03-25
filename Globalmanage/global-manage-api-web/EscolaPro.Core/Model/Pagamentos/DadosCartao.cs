using EscolaPro.Core.Interfaces;
using EscolaPro.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.Pagamentos
{
    public class DadosCartao : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public string NumeroCartao { get; set; }
        public string NomePessoa { get; set; }
        public string MesValidade { get; set; }
        public string AnoValidade { get; set; }
        public string BandeiraCartao { get; set; }
        public AcquirersEnum AcquirersEnum { get; set; }
        public decimal ValorParcela { get; set; }
        public decimal ValorTotal { get; set; }
        public int QuantidadeParcela { get; set; }
        public string TID { get; set; }
        public string CodigoAutorizacao { get; set; }
        public TipoPagamentoEnum TipoPagamento { get; set; }
    }
}
