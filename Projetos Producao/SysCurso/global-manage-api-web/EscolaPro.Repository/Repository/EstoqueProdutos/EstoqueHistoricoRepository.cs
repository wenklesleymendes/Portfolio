using EscolaPro.Core.Model.EstoqueProdutos;
using EscolaPro.Repository.Interfaces.EstoqueProdutos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository.EstoqueProdutos
{
    public class EstoqueHistoricoRepository : DomainRepository<HistoricoEstoque>, IEstoqueHistoricoRepository
    {
        public EstoqueHistoricoRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<HistoricoEstoque>> BuscarPorIdEstoque(int idEstoque)
        {
            IQueryable<HistoricoEstoque> query = await Task.FromResult(GenerateQuery((x => x.IdEstoque == idEstoque), null));

            return query.ToList();
        }
    }
}
