using EscolaPro.Core.Model.AulasOnline;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Interfaces.AulasOnline;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository.AulasOnline
{
    public class MateriaOnlineRepository : DomainRepository<MateriaOnline>, IMateriaOnlineRepository
    {
        public MateriaOnlineRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<MateriaOnline>> BuscarPorAulaOnline(int aulaOnlineId)
        {
            try
            {
                IQueryable<MateriaOnline> query = await Task.FromResult(GenerateQuery((x => x.AulaOnlineId == aulaOnlineId), null));

                return query.AsNoTracking().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<MateriaOnline>> BuscarPorMateria(int materiaId)
        {
            try
            {
                IQueryable<MateriaOnline> query = await Task.FromResult(GenerateQuery((x => x.Id == materiaId), null));

                return query.AsNoTracking().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
