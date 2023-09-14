using EscolaPro.Core.Model;
using EscolaPro.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository
{
    public class UnidadeTurmaRepository : DomainRepository<TurmaUnidade>, IUnidadeTurmaRepository
    {
        public UnidadeTurmaRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<TurmaUnidade>> BuscarPorIdTurma(int id)
        {
            IQueryable<TurmaUnidade> query = await Task.FromResult(GenerateQuery((x => x.TurmaId == id), null));

            return query;
        }

        public async Task Deletar(TurmaUnidade unidadeTurma)
        {
            dbContext.Entry(unidadeTurma).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            await dbContext.SaveChangesAsync();
        }
    }
}
