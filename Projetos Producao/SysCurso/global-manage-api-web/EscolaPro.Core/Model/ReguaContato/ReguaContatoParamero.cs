using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.ReguaContato
{
   public class ReguaContatoParametro : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Valor { get; set; }
        public TipoValor TipoValor { get; set; }
        public int ReguaContatoRegraId { get; set; }
        public ReguaContatoRegra ReguaContatoRegra { get; set; }
    }
}
