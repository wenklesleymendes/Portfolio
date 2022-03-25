using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.Atendimentos
{
    public class Leads : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public string Origem { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataProcessamento { get; set; }
        public int Status { get; set; }
    }
}
