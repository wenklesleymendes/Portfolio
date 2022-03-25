using EscolaPro.Core.Interfaces;
using EscolaPro.Core.Model.Pagamentos;
using EscolaPro.Core.Model.PainelMatricula.Enums;
using EscolaPro.Core.Model.PainelMatricula.PlanoAluno;
using EscolaPro.Core.Model.PortalAlunoProfessor;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.PainelMatricula
{
    public class MatriculaAluno : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public string NumeroMatricula { get; set; }
        public bool Status { get; set; }
        public Aluno Aluno { get; set; }
        public Curso Curso { get; set; }
        public Turma Turma { get; set; }
        public Unidade Unidade { get; set; }
        public PlanoPagamentoAluno PlanoPagamentoAluno { get; set; }
        public DateTime DataMatricula { get; set; }
        public ICollection<Anexo> Documentos { get; set; }
        public ICollection<ProvaAluno> ProvaAluno { get; set; }
        public ICollection<CertificadoProva> Certificado { get; set; }
        public int CursoId { get; set; }
        public int TurmaId { get; set; }
        public int AlunoId { get; set; }
        public int? UnidadeId { get; set; }
        public int? PlanoPagamentoAlunoId { get; set; }
        public ICollection<SolicitacaoAluno> SolicitacaoAluno { get; set; }
        public ICollection<MensagemAlunoProfessor> Mensagens { get; set; }
        public ICollection<InconsistenciaDocumento> InconsistenciaDocumento { get; set; }
        public TipoSituacaoCertificadoEnum TipoSituacaoCertificado { get; set; }
    }
}
