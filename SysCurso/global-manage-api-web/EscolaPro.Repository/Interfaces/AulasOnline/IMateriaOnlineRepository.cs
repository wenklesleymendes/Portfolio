using EscolaPro.Core.Model.AulasOnline;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.AulasOnline
{
    public interface IMateriaOnlineRepository : IDomainRepository<MateriaOnline>
    {
        Task<IEnumerable<MateriaOnline>> BuscarPorAulaOnline(int aulaOnlineId);
        Task<IEnumerable<MateriaOnline>> BuscarPorMateria(int materiaId);
    }
}
