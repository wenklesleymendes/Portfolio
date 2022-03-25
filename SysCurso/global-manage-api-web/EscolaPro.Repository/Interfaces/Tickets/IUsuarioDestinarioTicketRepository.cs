using EscolaPro.Core.Model.Tickets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.Tickets
{
    public interface IUsuarioDestinarioTicketRepository : IDomainRepository<UsuarioDestinarioTicket>
    {
        Task<IEnumerable<UsuarioDestinarioTicket>> BuscarPorDestinatarioTicket(int destinatarioTicketId);
    }
}
