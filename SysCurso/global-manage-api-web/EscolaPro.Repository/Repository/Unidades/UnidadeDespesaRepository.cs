using EscolaPro.Core.Model;
using EscolaPro.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository
{
    public class UnidadeDespesaRepository : DomainRepository<UnidadeDespesa>, IUnidadeDespesaRepository
    {
        public UnidadeDespesaRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<UnidadeDespesa>> PorIdUnidade(int idUnidade)
        {
            IQueryable<UnidadeDespesa> query = await Task.FromResult(GenerateQuery((x => x.UnidadeId == idUnidade), null));

            return query.ToList();
        }
    }
}
