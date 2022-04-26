using EscolaPro.Service.Dto.AgendaProvaVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface IAgendaProvaService
    {
        Task<DtoAgendaProva> Inserir(DtoAgendaProva dtoAgendaProva);
        Task<IEnumerable<DtoAgendaGrid>> BuscarTodos();
        Task<DtoAgendaProva> BuscarPorId(int idAgendaProva);
        Task<bool> Excluir(int idAgendaProva);
        Task<IEnumerable<DtoAgendaProva>> BuscarProvasDisponiveis(int colegioId, int unidadeId, int cursoId);
    }
}
