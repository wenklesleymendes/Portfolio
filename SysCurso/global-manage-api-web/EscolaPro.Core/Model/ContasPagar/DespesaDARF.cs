using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.ContasPagar
{
    public class DespesaDARF : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public string NomeContribuinte { get; set; }
        public DateTime PeriodoApuracao { get; set; }
        public string CnpjCpf { get; set; }
        public string CodigoReceita { get; set; }
        public string NumeroReferencia { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal? ValorPrincipal { get; set; }
        public decimal? ValorMulta { get; set; }
        public decimal? ValorJurosEncargos { get; set; }
        public decimal? ValorTotal { get; set; }
        public DateTime? DataPagamento { get; set; }
        public string ReferenciaEmpresa { get; set; }
        public string IdentificacaoComprovante { get; set; }
    }
}
