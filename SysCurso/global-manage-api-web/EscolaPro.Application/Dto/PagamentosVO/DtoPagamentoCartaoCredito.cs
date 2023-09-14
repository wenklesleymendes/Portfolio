using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.PagamentosVO
{
    public class DtoPagamentoCartaoCredito
    {
        public List<int> PagamentoIds { get; set; }
        public int QuantidadeParcela { get; set; }
        public decimal ValorTotal { get; set; }
        public DtoDadosCartao DadosCartaoAluno { get; set; }
        public bool Credito { get; set; }
        public bool TEF { get; set; }
        public string NumeroCartao { get; set; }
        public string NumeroControle { get; set; }
        public int? SolicitacaoId { get; set; }
        public int? MatriculaId { get; set; }
        public int UsuarioLogadoId { get; set; }
        public string ComprovanteCartao { get; set; }
    }
}
