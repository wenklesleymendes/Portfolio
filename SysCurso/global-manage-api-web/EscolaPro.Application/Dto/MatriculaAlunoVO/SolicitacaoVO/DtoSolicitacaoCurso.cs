using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.MatriculaAlunoVO.SolicitacaoVO
{
    public class DtoSolicitacaoCurso
    {
        public int Id { get; set; }
        public DtoCurso Curso { get; set; }
        public int CursoId { get; set; }
        public int SolicitacaoId { get; set; }
    }
}
