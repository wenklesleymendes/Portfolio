using EscolaPro.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.ContasAPagarVO
{
    public class DtoDetalheDespesa
    {
        public int IdDespesa { get; set; }
        public string NomeDespesa { get; set; }
        public string Fornecedor { get; set; }
        public TipoParcelaEnum TipoParcela { get; set; }
        public string ProximoVencimento { get; set; }
        public decimal ValorDespesa { get; set; }
        public string QuantidadeParcela { get; set; }
        public IEnumerable<DtoHistoricoDespesa> HistoricoDespesa { get; set; }
        public IEnumerable<DtoDespesaParcela> DespesaParcelas { get; set; }
        public bool Quitado { get; set; }
        public bool BaixaManual { get; set; }
    }
}
