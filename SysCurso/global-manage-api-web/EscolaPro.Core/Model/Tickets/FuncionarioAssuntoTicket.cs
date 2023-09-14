using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.Tickets
{
    public class FuncionarioAssuntoTicket : BaseEntity, IIdentityEntity
    {
        public int Id { get; set; }
        public int? UsuarioId { get; set; }
        public int? AssuntoTicketId { get; set; }
        public Usuario Usuario { get; set; }

    }
}
