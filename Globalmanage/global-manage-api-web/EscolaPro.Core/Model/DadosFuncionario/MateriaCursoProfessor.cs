using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.DadosFuncionario
{
    public class MateriaCursoProfessor : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public string NomeMateria { get; set; }
        public int CursoProfessorId { get; set; }
        public int IdMateria { get; set; }
    }
}
