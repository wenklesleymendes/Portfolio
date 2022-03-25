using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.MetasComissoes
{
    public class ComissaoParcela : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public int NumeroParcela { get; set; }
        public decimal Valor { get; set; }
        public int ComissoesId { get; set; }
    }
}
