using EscolaPro.Core.Model.Pagamentos;
using EscolaPro.Service.Dto.TicketVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface ITicketService
    {
        Task<DtoTicketRetorno> Inserir(DtoTicket ticket);
        Task<DtoMensagemTicket> ResponderTicket(DtoMensagemTicket mensagemTicket);
        Task<DtoDashboardTicket> ConsultarDashBoard(int usuarioLogadoId);
        Task<IEnumerable<DtoTicket>> BuscarTodos();
        Task<DtoTicketRetorno> BuscarPorId(int idTicket);
        Task<bool> Deletar(int idTicket);
        Task<IEnumerable<DtoGridTicket>> Filtrar(DtoFiltrarTicket dtoFiltrarTicket);
        Task<DtoTicketTimeline> BuscarMensagens(int ticketId);
        Task TicketErroBoletoItau(int matriculaId, int usuarioLogadoId, List<Pagamento> pagamentos);
        Task<IEnumerable<DtoTicket>> BuscarPorMatriculaId(int matriculaId, int assuntoTicketId = 0);
    }
}
