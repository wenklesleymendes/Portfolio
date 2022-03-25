using EscolaPro.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces
{
    public interface IApostilaOnlineRepository : IDomainRepository<ApostilaOnline>
    {
        Task<ApostilaOnline> BucarApostilaPorIdMateria(int materiaId);
        Task<List<ApostilaOnline>> BucarApostilaPorCursoId(int materiaId);
    }
}
