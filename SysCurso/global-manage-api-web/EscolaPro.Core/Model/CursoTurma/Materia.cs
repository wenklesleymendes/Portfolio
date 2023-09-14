using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.CursoTurma
{
    public class Materia : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public string NomeMateria { get; set; }
        public int CursoId { get; set; }
        public int Ordenacao { get; set; }
    }
}
