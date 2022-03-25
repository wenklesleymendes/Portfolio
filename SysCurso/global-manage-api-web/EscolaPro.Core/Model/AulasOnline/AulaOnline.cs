using EscolaPro.Core.Interfaces;
using EscolaPro.Core.Model.CursoTurma;
using EscolaPro.Core.Model.DadosFuncionario;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.AulasOnline
{
    public class AulaOnline : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public string NomeAulaOnline { get; set; }
        public ICollection<CursoOnline> Curso { get; set; }
        public ICollection<MateriaOnline> Materia { get; set; }
    }
}
