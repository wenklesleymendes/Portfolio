using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.MetasComissoes
{
    public class MetaUnidade : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public int UnidadeId { get; set; }
        public int MetaId { get; set; }
    }
}
