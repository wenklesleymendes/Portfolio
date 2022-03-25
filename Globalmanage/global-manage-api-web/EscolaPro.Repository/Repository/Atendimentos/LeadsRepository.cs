using EscolaPro.Core.Model.Atendimentos;
using EscolaPro.Repository.Interfaces.Atendimentos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository.Atendimentos
{
    public class LeadsRepository : DomainRepository<Leads>, ILeadsRepository
    {
        public LeadsRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Leads>> BuscaLeadsPorStatus(int status)
        {
            IQueryable<Leads> leads = await Task.FromResult(GenerateQuery((l => l.Status == status), null));

            return leads;
        }
    }
}
