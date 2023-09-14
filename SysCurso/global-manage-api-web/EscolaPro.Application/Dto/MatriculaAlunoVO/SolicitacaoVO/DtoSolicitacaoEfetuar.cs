using EscolaPro.Core.Model.Enums;
using EscolaPro.Service.Dto.PagamentosVO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.MatriculaAlunoVO.SolicitacaoVO
{
    public class DtoSolicitacaoEfetuar
    {
        public int? SolicitacaoId { get; set; }
        public TipoPagamentoEnum TipoPagamento { get; set; }
        public bool TEF { get; set; }
        public decimal ValorPago { get; set; }
        public int QuantidadeParcela { get; set; }
        public decimal ValorTotal { get; set; }
        public DtoDadosCartao DadosCartaoAluno { get; set; }
        public bool Credito { get; set; }
        public string NumeroCartao { get; set; }
        public string NumeroControle { get; set; }
        public int MatriculaId { get; set; }
        public int UsuarioLogadoId { get; set; }
        public string ComprovanteCartao { get; set; }
    }
}
