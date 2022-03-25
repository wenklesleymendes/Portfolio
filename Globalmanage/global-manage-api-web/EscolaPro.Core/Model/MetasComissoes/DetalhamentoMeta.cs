using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.MetasComissoes
{
    public class DetalhamentoMeta : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public DateTime DataPeriodo { get; set; }
        public int Quantidade { get; set; }
        public int MetaId { get; set; }
    }
}
