using EscolaPro.Service.Dto.TicketVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface IAssuntoTicketService
    {
        Task<DtoAssuntoTicket> Inserir(DtoAssuntoTicket ticket);
        Task<IEnumerable<DtoAssuntoTicket>> BuscarTodos();
        Task<IEnumerable<DtoAssuntoTicket>> BuscarPorUnidadeDepartamento(int? idUnidade, int? idDepartamento);
        Task<DtoAssuntoTicket> BuscarPorId(int idTicket);
        Task<bool> Deletar(int idTicket);
    }
}
