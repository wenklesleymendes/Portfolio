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
    public class EstoqueRepository : DomainRepository<Produto>, IEstoqueRepository
    {
        public EstoqueRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<Produto> BuscarPorId(int idProduto)
        {
            IQueryable<Produto> query = await Task.FromResult(GenerateQuery((x => x.Id == idProduto), null)
                .Include(x => x.Unidade));

            return query.FirstOrDefault();
        }
    }
}
