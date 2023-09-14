using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.Provas
{
    public class AgendaCurso : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public Curso Curso { get; set; }
        public int CursoId { get; set; }
        public int AgendaProvaId { get; set; }
    }
}
