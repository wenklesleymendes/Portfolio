using EscolaPro.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.MatriculaAlunoVO.PlanoAlunoVO
{
    public class DtoPlanoPagamentoAluno
    {
        public int Id { get; set; }
        public TipoPagamentoEnum TipoPagamento { get; set; }
        public int? PlanoPagamentoId { get; set; }
        public int? CampanhaId { get; set; }
        public DateTime? DataPrimeiraParcela { get; set; }
        public DateTime? DataSegundaParcela { get; set; }
        public bool? TemApostila { get; set; }
    }
}
