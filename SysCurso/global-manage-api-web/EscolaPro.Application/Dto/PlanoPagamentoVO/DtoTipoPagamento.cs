using EscolaPro.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.PlanoPagamentoVO
{
    public class DtoTipoPagamento
    {
        public int Id { get; set; }
        public TipoPagamentoEnum TipoPagamento { get; set; }
    }
}
