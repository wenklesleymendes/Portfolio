using EscolaPro.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.ContasAPagarVO
{
    public class DtoGridDespesaResponse
    {
        public List<DtoGridDespesa> Despesa { get; set; }
        public decimal ValorTotalDespesa { get; set; }
    }
}
