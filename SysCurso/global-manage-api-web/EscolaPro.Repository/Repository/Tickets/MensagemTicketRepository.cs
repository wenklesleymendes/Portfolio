using EscolaPro.Core.Model.Tickets;
using EscolaPro.Repository.Interfaces.Tickets;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository.Tickets
{
    public class MensagemTicketRepository : DomainRepository<DestinatarioTicket>, IMensagemTicketRepository
    {
        public MensagemTicketRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<DestinatarioTicket>> BuscarPorTicket(int idTicket)
        {
            IQueryable<DestinatarioTicket> query = await Task.FromResult(GenerateQuery((x => x.TicketId == idTicket), null));

            return query.ToList();
        }
    }
}
