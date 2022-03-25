using EscolaPro.Core.Model.AulasOnline;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.AulasOnline
{
    public interface IAulaOnlineRepository : IDomainRepository<AulaOnline>
    {
        Task<AulaOnline> BuscarPorCurso(int cursoId);
        Task<AulaOnline> BuscarPorId(int aulaOnlineId);
        Task Atualizar(AulaOnline aulaOnline);
    }
}
