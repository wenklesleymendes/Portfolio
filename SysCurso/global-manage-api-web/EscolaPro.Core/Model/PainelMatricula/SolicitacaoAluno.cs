using EscolaPro.Core.Interfaces;
using EscolaPro.Core.Model.MetasComissoes;
using EscolaPro.Core.Model.Solicitacoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.PainelMatricula
{
    public class SolicitacaoAluno : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public StatusPagamentoEnum StatusPagamento { get; set; }
        public Solicitacao Solicitacao { get; set; }
        public int SolicitacaoId { get; set; }
        public SolicitacaoEmail SolicitacaoEmail { get; set; }
        public int MatriculaId { get; set; }
        public decimal Valor { get; set; }
        public int UsuarioLogadoId { get; set; }
    }
}
