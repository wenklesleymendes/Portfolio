using EscolaPro.Core.Interfaces;
using EscolaPro.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.BolsaConvenio
{
    public class CampanhaTipoPagamento : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public TipoPagamentoEnum TipoPagamento { get; set; }
        public int CampanhaId { get; set; }
    }
}
