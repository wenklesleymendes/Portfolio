using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.Solicitacoes
{
    public class SolicitacaoEmail : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public DateTime DataEnvio { get; set; }
        public string CorpoEmail { get; set; }
    }
}
