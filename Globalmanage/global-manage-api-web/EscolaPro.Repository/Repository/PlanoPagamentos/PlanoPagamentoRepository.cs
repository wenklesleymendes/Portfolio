using EscolaPro.Core.Model;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Scripts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository
{
    public class PlanoPagamentoRepository : DomainRepository<PlanoPagamento>, IPlanoPagamentoRepository
    {
        public PlanoPagamentoRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        [Obsolete]
        public async Task<IEnumerable<PlanoPagamento>> BuscarPlanoPagamento(int formaPagamento, int? quantidadeParcela, int cursoId, int unidadeId)
        {
            try
            {
                string sqlQuery = PlanoPagamentoScript.Filtrar(formaPagamento, quantidadeParcela, cursoId, unidadeId);

                var query = dbSet.FromSql(sqlQuery).Select(x => new PlanoPagamento
                {
                    Id = x.Id,
                }).ToList();

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PlanoPagamento> BuscarPorId(int idPlanoPagamento)
        {
            try
            {
                IQueryable<PlanoPagamento> query = await Task.FromResult(GenerateQuery((x => x.Id == idPlanoPagamento), null)
                                                            .Include(x => x.PlanoPagamentoCurso)
                                                            .Include(x => x.PlanoPagamentoUnidade)
                                                            .AsNoTracking());

                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Obsolete]
        public async Task<IEnumerable<PlanoPagamento>> PorCursoUnidade(int cursoId, int unidadeId)
        {
            try
            {
                string sqlQuery = PlanoPagamentoScript.Filtrar(cursoId, unidadeId);

                var query = dbSet.FromSql(sqlQuery).Select(x => new PlanoPagamento
                {
                    Id = x.Id,
                }).ToList();

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}