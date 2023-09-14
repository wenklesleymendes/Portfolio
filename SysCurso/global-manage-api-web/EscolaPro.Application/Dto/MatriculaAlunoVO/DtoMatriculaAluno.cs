using EscolaPro.Service.Dto.AlunosVO;
using EscolaPro.Service.Dto.UnidadeVO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.MatriculaAlunoVO
{
    public class DtoMatriculaAluno
    {
        public int Id { get; set; }
        //public DtoAluno Aluno { get; set; }
        //public DtoCurso Curso { get; set; }
        //public DtoTurma Turma { get; set; }
        public string NumeroMatricula { get; set; }
        public DateTime DataMatricula { get; set; }
        public bool Status { get; set; }
        public ICollection<DtoAnexo> Documentos { get; set; }
        public ICollection<DtoProvaAluno> ProvaAluno { get; set; }
        public ICollection<DtoCertificadoProva> Certificado { get; set; }
        public int CursoId { get; set; }
        public int TurmaId { get; set; }
        public int AlunoId { get; set; }
        public int UnidadeId { get; set; }
        public DtoUnidadeResponse Unidade { get; set; }
        public int UsuarioLogadoId { get; set; }
    }
}
