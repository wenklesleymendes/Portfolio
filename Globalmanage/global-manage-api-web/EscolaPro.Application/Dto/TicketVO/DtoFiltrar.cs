using EscolaPro.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.TicketVO
{
    public class DtoFiltrar
    {
        public int? UnidadeId { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public TipoPagamentoEnum? TipoPagamento { get; set; }
    }
}
