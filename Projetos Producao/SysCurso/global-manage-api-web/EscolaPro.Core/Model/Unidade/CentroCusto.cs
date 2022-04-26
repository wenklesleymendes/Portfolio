using EscolaPro.Core.Interfaces;
using System;

namespace EscolaPro.Core.Model
{
    public class CentroCusto : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int UnidadeId { get; set; }
    }
}