using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.ContasAPagarVO
{
    public class DtoGridDespesa
    {
        public int IdDespesa { get; set; }
        public string Unidade { get; set; }
        public string NomeDespesa { get; set; }
        public string Categoria { get; set; }
        public string FormasPagamentos { get; set; }
        public string Fornecedor { get; set; }
        public DateTime Vencimento { get; set; }
        public string NumeroParcelas { get; set; }
        public decimal ValorParcela { get; set; }
        public StatusDespesaEnum StatusDespesa { get; set; }
        public bool Quitado { get; set; }
    }
}
