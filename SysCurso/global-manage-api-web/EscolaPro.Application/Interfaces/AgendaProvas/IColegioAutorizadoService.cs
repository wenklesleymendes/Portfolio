using EscolaPro.Service.Dto.AgendaProvaVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces.AgendaProvas
{
    public interface IColegioAutorizadoService
    {
        Task<DtoColegioAutorizado> Inserir(DtoColegioAutorizado dtoColegioAutorizado);
        Task<DtoColegioAutorizado> BuscarPorId(int colegioAutorizadoId);
        Task<IEnumerable<DtoColegioAutorizado>> BuscarTodos();
        Task<bool> Excluir(int colegioAutorizadoId);
    }
}
