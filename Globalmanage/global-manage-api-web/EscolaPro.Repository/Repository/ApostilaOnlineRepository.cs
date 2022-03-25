using EscolaPro.Core.Model;
using EscolaPro.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository
{
    public class ApostilaOnlineRepository : DomainRepository<ApostilaOnline>, IApostilaOnlineRepository
    {
        public ApostilaOnlineRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<ApostilaOnline> BucarApostilaPorIdMateria(int materiaId)
        {
            IQueryable<ApostilaOnline> query = await Task.FromResult(GenerateQuery((x => x.MateriaId == materiaId && !x.IsDelete), null).AsNoTracking());

            return query.FirstOrDefault();
        }

        public async Task<List<ApostilaOnline>> BucarApostilaPorCursoId(int cursoId)
        {
            IQueryable<ApostilaOnline> query = await Task.FromResult(GenerateQuery((x => x.CursoId == cursoId && !x.IsDelete), null).AsNoTracking());

            return query.ToList();
        }
    }
}
