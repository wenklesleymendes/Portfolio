using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.CampanhaVO
{
    public class DtoCampanhaFormaPagamento
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public int QuantidadeParcela { get; set; }
        public int CampanhaId { get; set; }
    }
}
