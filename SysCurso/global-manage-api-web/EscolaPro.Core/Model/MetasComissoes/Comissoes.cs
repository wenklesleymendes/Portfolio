using EscolaPro.Core.Interfaces;
using EscolaPro.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.MetasComissoes
{
    public class Comissoes : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public TipoPagamentoEnum TipoPagamento { get; set; }
        public bool TipoComissao { get; set; }
        public DateTime? DataInicioVirgencia { get; set; }
        public DateTime? DataFimVirgencia { get; set; }
        public bool PeriodoIndeterminado { get; set; }
        public bool TotalParcelas { get; set; }
        public ICollection<ComissaoParcela> ComissaoParcelas { get; set; }
        public int UnidadeId { get; set; }
        public StatusPagamentoEnum StatusPagamento { get; set; }
    }
}
