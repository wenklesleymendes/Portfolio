using EscolaPro.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.CampanhaVO
{
    public class DtoCampanhaPlanoPagamento
    {
        public int Id { get; set; }
        public TipoPagamentoEnum TipoPagamento { get; set; }
        public int CampanhaId { get; set; }
    }
}
