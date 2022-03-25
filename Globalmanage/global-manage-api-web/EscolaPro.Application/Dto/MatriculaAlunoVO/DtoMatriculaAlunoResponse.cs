using EscolaPro.Core.Model;
using EscolaPro.Service.Dto.AlunosVO;
using EscolaPro.Service.Dto.MatriculaAlunoVO.PlanoAlunoVO;
using EscolaPro.Service.Dto.UnidadeVO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.MatriculaAlunoVO
{
    public class DtoMatriculaAlunoResponse
    {
        public int Id { get; set; }
        public string NumeroMatricula { get; set; }
        public bool Status { get; set; }
        public DateTime DataMatricula { get; set; }

        public DateTime DataTermino
        {
            get { return DataMatricula.AddMonths(this?.Curso.Duracao ?? 11); }
        }

        public DtoAluno Aluno { get; set; }
        public DtoCurso Curso { get; set; }
        public DtoTurma Turma { get; set; }
        public DtoUnidadeTurma Unidade { get; set; }
        public DtoPlanoPagamentoAluno PlanoPagamentoAluno { get; set; }
        public int CursoId { get; set; }
        public int TurmaId { get; set; }
        public int AlunoId { get; set; }
        public int UnidadeId { get; set; }
        public bool MaterialLiberado { get; set; }


        // Contadores das abas do aluno
        public int QuantidadeDocumentosPendentes { get; set; }
        public bool ExistePendenciaFinanceira { get; set; }
        public bool ExistePendenciaContrato { get; set; }
        public bool ExistePendenciaSolicitacaoAnexo { get; set; }

        public int? UsuarioLogadoId { get; set; }
    }

    public class DtoDocumentosPendentes
    {
        public  IEnumerable<TipoAnexoEnum> DocumentosPendentes { get; set; }
        public bool DeclaracaoPendenciaDocumental { get; set; }
    }
}
