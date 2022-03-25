using EscolaPro.Core.Model;
using EscolaPro.Core.Model.Enums;
using EscolaPro.Service.Dto.UnidadeVO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.PlanoPagamentoVO
{
    public class DtoPlanoPagamento : BaseResponse
    {
        public int Id { get; set; }
        public TipoPagamentoEnum TipoPagamento { get; set; }
        public int QuantidadeParcela { get; set; }
        public decimal ValorParcela { get; set; }
        public decimal ValorTotalPlano { get; set; }
        public decimal? PorcentagemDescontoPontualidade { get; set; }
        public decimal? ValorTotalInscricaoProva { get; set; }
        public decimal? ValorMaterialDidatico { get; set; }
        public bool IsentarMaterialDidatico { get; set; }
        public decimal ValorTaxaMatricula { get; set; }
        public bool IsentarMatricula { get; set; }
        public bool IsActive { get; set; }
        public List<DtoCurso> Curso { get; set; }
        public List<DtoUnidadeTurma> Unidade { get; set; }
    }
}