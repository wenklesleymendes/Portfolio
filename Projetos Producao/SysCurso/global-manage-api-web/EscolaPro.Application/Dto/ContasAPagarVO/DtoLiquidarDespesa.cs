using EscolaPro.Core.Model.DadosFuncionario;
using EscolaPro.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.ContasAPagarVO
{
    public class DtoLiquidarDespesa
    {
        public int IdDespesa { get; set; }
        public DateTime DataPagamento { get; set; }
        public TipoPagamentoEnum TipoPagamento { get; set; }
        public decimal ValorPago { get; set; }
        public decimal? DescontoTaxa { get; set; }
        public decimal? Juros { get; set; }
        public decimal? Multa { get; set; }
        public decimal? ValorTotalPagar { get; set; }
        public string Observacao { get; set; }
        public int UnidadeId { get; set; }

        public string CodigoBanco { get; set; }
        public string NomeBanco { get; set; }
        public string NumeroAgencia { get; set; }
        public string NumeroConta { get; set; }
        public TipoContaBancariaEnum? TipoContaBancaria { get; set; }
    }
}
