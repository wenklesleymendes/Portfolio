using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.PagamentosVO
{
    public class DtoContratarPlano
    {
        public int MatriculaId { get; set; }
        public int PlanoPagamentoId { get; set; }
        public bool TemApostila { get; set; }
        public int? CampanhaId { get; set; }
        public DateTime PrimeiroPagamento { get; set; }
        public DateTime? SegundoPagamento { get; set; }

        //Multi-Cartões
        public int? QuantidadeParcela { get; set; }
        public decimal? ValorParcela { get; set; }
        public DtoDadosCartao CartaoCredito { get; set; }
        public bool TEF { get; set; }
        public string NumeroCartao { get; set; }
        public string NumeroControle { get; set; }
        public decimal ValorTotal { get; set; }
        public string ComprovanteCartao { get; set; }
        public int UsuarioLogadoId { get; set; }
    }
}
