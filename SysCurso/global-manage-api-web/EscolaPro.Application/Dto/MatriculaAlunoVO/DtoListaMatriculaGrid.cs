using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.MatriculaAlunoVO
{
    public class DtoListaMatriculaGrid
    {
        public int MatriculaId { get; set; }
        public string NumeroMatricula { get; set; }
        public string Unidade { get; set; }
        public int UnidadeId { get; set; }
        public int AlunoId { get; set; }
        public bool StatusMatricula { get; set; }
        public string Curso { get; set; }
        public string Ano { get; set; }
        public string Semestre { get; set; }
        public string Financeiro { get; set; }
        public int CursoId { get; set; }
        public int TurmaId { get; set; }
        public bool NacionalTec { get; set; }
        public string Logo { get; set; }
    }
}
