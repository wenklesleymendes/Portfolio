using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TEF.Core.Api.Models
{
    public class DtoTransacao
    {
        public decimal Valor { get; set; }
        public bool Credito { get; set; }
        public int[] PagamentoIds { get; set; }

        // Caso de Matricula Nova
        public int MatriculaId { get; set; }
        public int PlanoPagamentoId { get; set; }
        public bool TemApostila { get; set; }
        public int? CampanhaId { get; set; }
    }
}
