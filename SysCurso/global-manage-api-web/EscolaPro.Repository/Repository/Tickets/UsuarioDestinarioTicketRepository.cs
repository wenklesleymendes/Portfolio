using EscolaPro.Core.Model.Tickets;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Interfaces.Tickets;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository.Tickets
{
    public class UsuarioDestinarioTicketRepository : DomainRepository<UsuarioDestinarioTicket>, IUsuarioDestinarioTicketRepository
    {
        public UsuarioDestinarioTicketRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<UsuarioDestinarioTicket>> BuscarPorDestinatarioTicket(int destinatarioTicketId)
        {
            IQueryable<UsuarioDestinarioTicket> query = await Task.FromResult(GenerateQuery((x => x.DestinatarioTicketId == destinatarioTicketId), null));

            return query.ToList();
        }
    }
}
