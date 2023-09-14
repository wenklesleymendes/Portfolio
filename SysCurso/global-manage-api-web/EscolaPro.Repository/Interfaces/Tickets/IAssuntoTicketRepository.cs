using EscolaPro.Core.Model.Tickets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.Tickets
{
    public interface IAssuntoTicketRepository : IDomainRepository<AssuntoTicket>
    {
        Task<AssuntoTicket> BuscarAssuntoSolicitacao();
        Task<AssuntoTicket> BuscarPorId(int idTicket);
        Task<List<AssuntoTicket>> BuscarPorUnidadeDepartamento(int? idUnidade, int? idDepartamento);
        AssuntoTicket BuscarBaixaBoleto();
        AssuntoTicket BuscarAnaliseDocumentacaoProva();
        AssuntoTicket BuscarAuditoriaCancelamento();
    }
}
