using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model
{
    public class PlanoPagamentoCurso : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public int PlanoPagamentoId { get; set; }
        public int CursoId { get; set; }
    }
}
