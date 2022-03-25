using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.DadosFuncionario
{
    public class CursoProfessor : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public string NomeCurso { get; set; }
        public ICollection<MateriaCursoProfessor?> MateriaCursoProfessor { get; set; }
        public int FuncionarioId { get; set; }
        public int IdCurso { get; set; }
    }
}

