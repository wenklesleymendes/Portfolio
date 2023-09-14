using EscolaPro.Service.Dto;
using EscolaPro.Service.Dto.AulaOnlineVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface IAulaOnlineService
    {
        Task<DtoAulaOnline> Inserir(DtoAulaOnline dtoAulaOnline);
        Task<IEnumerable<DtoAulaOnline>> BuscarTodos();
        Task<DtoAulaOnline> BuscarPorCurso(int cursoId);
        Task<bool> Excluir(int aulaOnlineId);
        Task<DtoAulaOnline> BuscarPorId(int aulaOnlineId);
        Task<DtoGridMateriaOnline> BuscarMaterias(int aulaOnlineId);
        Task<DtoGridMateriaOnline> MinhasAulasOnline(int matriculaId);
    }
}
