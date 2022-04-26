using EscolaPro.Core.Model;
using EscolaPro.Core.Model.Tickets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.Tickets
{
    public interface ITicketRepository : IDomainRepository<Ticket>
    {
        Task<Ticket> BuscarPorId(int idTicket);
        Task<IEnumerable<Ticket>> Filtrar(FiltrarTicket filtrarTicket, bool encaminhados = false);
        Task<IEnumerable<Ticket>> BuscarPorMatriculaId(int? matriculaId);
        Task<IEnumerable<Ticket>> TickestsUsuario(Usuario usuario, StatusTicketEnum statusTicket = StatusTicketEnum.Todos);
        Task<IEnumerable<Ticket>> BuscarPorMatriculaId(int matriculaId, int assuntoTicketId=0);
    }
}
