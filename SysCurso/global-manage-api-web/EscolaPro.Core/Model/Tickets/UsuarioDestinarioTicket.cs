using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.Tickets
{
    public class UsuarioDestinarioTicket : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public int? FuncionarioId { get; set; }
        public int? DestinatarioTicketId { get; set; }
       // public int? UsuarioDestinarioTicketId { get; set; }
    }
}
