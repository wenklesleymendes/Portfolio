using EscolaPro.Core.Model.Enums;
using EscolaPro.Core.Model.Pagamentos;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.Solicitacoes
{
    public class SolicitacaoEfetuar
    {
        public int? SolicitacaoId { get; set; }
        public TipoPagamentoEnum TipoPagamento { get; set; }
        public bool TEF { get; set; }
        public decimal ValorPago { get; set; }
        public int QuantidadeParcela { get; set; }
        public decimal ValorTotal { get; set; }
        public DadosCartao DadosCartaoAluno { get; set; }
        public bool Credito { get; set; }
        public string NumeroCartao { get; set; }
        public string NumeroControle { get; set; }
        public int MatriculaId { get; set; }
        public int UsuarioLogadoId { get; set; }
        public string ComprovanteCartao { get; set; }
    }
}
