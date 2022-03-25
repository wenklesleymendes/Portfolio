using EscolaPro.Core.Model.Tickets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.Tickets
{
    public interface IMensagemTicketRepository : IDomainRepository<DestinatarioTicket>
    {
        Task<IEnumerable<DestinatarioTicket>> BuscarPorTicket(int idTicket);
    }
}
