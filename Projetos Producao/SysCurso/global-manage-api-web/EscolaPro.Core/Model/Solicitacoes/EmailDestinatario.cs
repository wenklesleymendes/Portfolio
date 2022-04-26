using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.Solicitacoes
{
    public class EmailDestinatario : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int SolicitacaoId { get; set; }
    }
}
