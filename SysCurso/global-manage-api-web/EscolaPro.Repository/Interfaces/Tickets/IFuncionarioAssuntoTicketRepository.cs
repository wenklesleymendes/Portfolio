using EscolaPro.Core.Model.Tickets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.Tickets
{
    public interface IFuncionarioAssuntoTicketRepository : IDomainRepository<FuncionarioAssuntoTicket>
    {
        Task<List<FuncionarioAssuntoTicket>> BuscarPorAssuntoId(int idAssumto);
        Task ApagarFuncionariosAntigo(int assuntoId);
    }
}
