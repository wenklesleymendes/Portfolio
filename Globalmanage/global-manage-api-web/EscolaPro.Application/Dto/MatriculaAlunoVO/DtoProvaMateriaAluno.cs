using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.MatriculaAlunoVO
{
    public class DtoProvaMateriaAluno
    {
        public int Id { get; set; }
        public string NomeMateria { get; set; }
        public int ProvaAlunoId { get; set; }
        public bool Aprovado { get; set; }
        public bool IsDelete { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
