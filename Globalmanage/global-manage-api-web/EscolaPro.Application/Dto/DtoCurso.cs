using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto
{
    public class DtoCurso
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int Duracao { get; set; }
        public bool IsDelete { get; set; }
        public IEnumerable<DtoMateria> Materia { get; set; }
        public bool NacionatalTec { get; set; }
    }

    public class DtoCursoMatricula
    {
        public int Id { get; set; }
        public string Descricao => !NacionatalTec ? "Supletivo Preparatório" : Descricao;
        public bool IsDelete { get; set; }
        public IEnumerable<DtoMateria> Materia { get; set; }
        public bool NacionatalTec { get; set; }
    }
}
