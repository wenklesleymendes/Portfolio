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
    public class PlanoPagamentoUnidadeRepository : DomainRepository<PlanoPagamentoUnidade>, IPlanoPagamentoUnidadeRepository
    {
        public PlanoPagamentoUnidadeRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<PlanoPagamentoUnidade>> BuscarPorPlanoPagamento(int idPlanoPagamento)
        {
            IQueryable<PlanoPagamentoUnidade> query = await Task.FromResult(GenerateQuery((x => x.PlanoPagamentoId == idPlanoPagamento), null).AsNoTracking());

            return query;
        }

        public async Task Deletar(PlanoPagamentoUnidade planoPagamentoUnidade)
        {
            dbContext.Entry(planoPagamentoUnidade).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            await dbContext.SaveChangesAsync();
        }
    }
}
