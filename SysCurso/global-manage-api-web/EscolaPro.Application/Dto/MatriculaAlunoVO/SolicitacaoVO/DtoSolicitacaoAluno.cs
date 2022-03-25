using EscolaPro.Core.Model.MetasComissoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.MatriculaAlunoVO
{
    public class DtoSolicitacaoAluno
    {
        public int Id { get; set; }
        public string Descricao => Solicitacao != null ? Solicitacao.Descricao : "";
        public decimal Valor { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public StatusPagamentoEnum StatusPagamento { get; set; }
        public DtoSolicitacao Solicitacao { get; set; }
        public DtoSolicitacaoEmail SolicitacaoEmail { get; set; }
        public int UsuarioLogadoId { get; set; }
        public int AnexoId { get; set; }
        public int MatriculaId { get; set; }
    }
}
