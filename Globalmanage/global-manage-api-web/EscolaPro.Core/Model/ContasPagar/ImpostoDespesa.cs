using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.ContasPagar
{
    public class ImpostoDespesa : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public string NomeContribuinte { get; set; }
        public DateTime PeriodoApuracao { get; set; }
        public decimal ValorINSS { get; set; }
        public decimal ValorOutrasEntidades { get; set; }
        public decimal AtualizacaoMonetariaJurosMora { get; set; }
        public decimal TotalReconhecer { get; set; }
        public string IdentificacaoComprovante { get; set; }
        public string ReferenciaEmpresa { get; set; }
        public string CodigoPagamento { get; set; }
        public string CodigoReceita { get; set; }
        public decimal ValorPrincipal { get; set; }
        public decimal ValorMulta { get; set; }
        public string NumeroReferencia { get; set; }
    }
}
