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
    public class PlanoPagamentoCursoRepository : DomainRepository<PlanoPagamentoCurso>, IPlanoPagamentoCursoRepository
    {
        public PlanoPagamentoCursoRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<PlanoPagamentoCurso>> BuscarPorCursoId(int idCurso)
        {
            IQueryable<PlanoPagamentoCurso> query = await Task.FromResult(GenerateQuery((x => x.CursoId == idCurso), null));

            return query;
        }

        public async Task<IEnumerable<PlanoPagamentoCurso>> BuscarPorPlanoPagamento(int idPlanoPagamento)
        {
            IQueryable<PlanoPagamentoCurso> query = await Task.FromResult(GenerateQuery((x => x.PlanoPagamentoId == idPlanoPagamento), null).AsNoTracking());

            return query;
        }

        public async Task Deletar(PlanoPagamentoCurso planoPagamentoCurso)
        {
            dbContext.Entry(planoPagamentoCurso).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            await dbContext.SaveChangesAsync();
        }
    }
}
