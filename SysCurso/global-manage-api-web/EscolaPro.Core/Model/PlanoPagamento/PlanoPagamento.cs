using EscolaPro.Core.Interfaces;
using EscolaPro.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model
{
    public class PlanoPagamento : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public TipoPagamentoEnum TipoPagamento { get; set; }
        public int QuantidadeParcela { get; set; }
        public decimal ValorParcela { get; set; }
        public decimal ValorTotalPlano { get; set; }
        public decimal PorcentagemDescontoPontualidade { get; set; }
        public decimal ValorTotalInscricaoProva { get; set; }
        public decimal ValorMaterialDidatico { get; set; }
        public bool IsentarMaterialDidatico { get; set; }
        public decimal ValorTaxaMatricula { get; set; }
        public bool IsentarMatricula { get; set; }
        public ICollection<PlanoPagamentoCurso?> PlanoPagamentoCurso { get; set; }
        public ICollection<PlanoPagamentoUnidade?> PlanoPagamentoUnidade { get; set; }

    }
}
