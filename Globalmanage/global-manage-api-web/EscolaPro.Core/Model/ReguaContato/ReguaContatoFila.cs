using EscolaPro.Core.Interfaces;
using EscolaPro.Core.Model.Pagamentos;
using EscolaPro.Core.Model.PainelMatricula;
using System;

namespace EscolaPro.Core.Model.ReguaContato
{
    public class ReguaContatoFila : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public int ReguaContatoRegrasId { get; set; }
        public int AlunoId { get; set; }
        public int? UnidadeId { get; set; }
        public int PagamentoId { get; set; }
        public int Prioridade { get; set; }
        public StatusFila StatusFila { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime DataEnvio { get; set; }
        public string MensagemErro { get; set; }
        public bool EnviadaComSucesso { get; set; }
        public ReguaContatoRegra ReguaContatoRegras { get; set; }
        public Unidade Unidade { get; set; }
        public Aluno Aluno { get; set; }
        public Pagamento Pagamento { get; set; }
        public MatriculaAluno MatriculaAluno { get; set; }
    }
}
