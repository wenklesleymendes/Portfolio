using EscolaPro.Service.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface IApostilaOnlineService
    {
        Task<DtoApostilaOnline> BucarApostilaPorIdMateria(int materiaId);
        Task<List<DtoApostilaOnline>> BucarApostilaPorCursoId(int cursoId);
    }
}
