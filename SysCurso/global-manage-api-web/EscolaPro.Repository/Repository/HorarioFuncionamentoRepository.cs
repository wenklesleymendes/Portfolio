using EscolaPro.Core.Model;
using EscolaPro.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository
{
    public class HorarioFuncionamentoRepository : DomainRepository<HorarioFuncionamento>, IHorarioFuncionamentoRepository
    {
        public HorarioFuncionamentoRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<HorarioFuncionamento>> PorIdUnidade(int idUnidade)
        {
            IQueryable<HorarioFuncionamento> query = await Task.FromResult(GenerateQuery((x => x.UnidadeId == idUnidade), null));

            return query.ToList();
        }
    }
}
