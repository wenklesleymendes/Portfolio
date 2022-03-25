using EscolaPro.Core.Model;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository
{
    public class CentroCustoRepository : DomainRepository<CentroCusto>, ICentroCustoRepository
    {
        public CentroCustoRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<CentroCusto>> BuscarPorUnidade(int idUnidade)
        {
            IQueryable<CentroCusto> query = await Task.FromResult(GenerateQuery((x => x.UnidadeId == idUnidade && !x.IsDelete), null));

            return query.ToList();
        }
    }
}
