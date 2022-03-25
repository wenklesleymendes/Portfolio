using EscolaPro.Core.Model;
using EscolaPro.Service.Dto.AlunosVO;
using EscolaPro.Service.Dto.UnidadeVO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.DocumentosAlunoVO
{
    public class DtoDocumentoAluno
    {
        public DtoAluno Aluno { get; set; }
        public DtoUnidadeResponse Unidade { get; set; }
        public DtoCurso Curso { get; set; }
        public IEnumerable<TipoAnexoEnum> Documentos { get; set; }
    }
}
