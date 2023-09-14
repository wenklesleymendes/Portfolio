using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.FuncionarioVO
{
    public class DtoCursoProfessor
    {
        public int IdCurso { get; set; }
        public string NomeCurso { get; set; }
        public ICollection<DtoMateriaCursoProfessor?> MateriaCursoProfessor { get; set; }
        public bool IsDelete { get; set; }
    }
}
