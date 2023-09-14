using EscolaPro.Service.Dto;
using EscolaPro.Service.Dto.AulaOnlineVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces.AulasOnlineVimeo
{
    public interface IMateriaOnlineService
    {
        Task<DtoMateriaOnline> Inserir(DtoMateriaOnline dtoMateriaOnline);
        Task<IEnumerable<DtoMateriaOnline>> BuscarTodos();
        Task<DtoGridGeneric<DtoMateriaOnline>> BuscarPorAulaOnline(int aulaOnlineId);
        Task<IEnumerable<DtoMateriaOnline>> BuscarPorMateria(int materiaId);
        Task<DtoMateriaOnline> BuscarPorId(int materiaId);
        Task<bool> Excluir(int materiaId);        
        Task<IEnumerable<DtoMateria>> BuscarMateriasPorCurso(int cursoId);
    }
}
