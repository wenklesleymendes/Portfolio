using EscolaPro.Core.Model;
using EscolaPro.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository
{
    public class HistoricoOcorrenciasRepository : DomainRepository<HistoricoOcorrencias>, IHistoricoOcorrenciasRepository
    {
        public HistoricoOcorrenciasRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<HistoricoOcorrencias>> PorIdUnidade(int idUnidade)
        {
            IQueryable<HistoricoOcorrencias> query = await Task.FromResult(GenerateQuery((x => x.UnidadeId == idUnidade), null));

            return query.ToList();
        }
    }
}