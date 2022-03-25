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
    public class ItemProdutoEstoqueRepository : DomainRepository<ItemProduto>, IItemProdutoEstoqueRepository
    {
        public ItemProdutoEstoqueRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<ItemProduto> BuscarEstoqueDisponivel(int idProduto)
        {
            IQueryable<ItemProduto> query = await Task.FromResult(GenerateQuery((x => x.ProdutoId == idProduto && x.QuantidadeEntrada != x.QuantidadeSaida), null));

            var estoque = query.FirstOrDefault();

            if (estoque != null)
            {
                return query.FirstOrDefault();
            }
            else
            {
                query = await Task.FromResult(GenerateQuery((x => x.ProdutoId == idProduto), null));

                if (query.Count() > 0)
                {
                    var produtos = query.ToList();

                    return produtos.Last();
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<List<ItemProduto>> BuscarItemProdutoPorProdutoId(int idProduto)
        {
            IQueryable<ItemProduto> query = await Task.FromResult(GenerateQuery((x => x.ProdutoId == idProduto && !x.IsDelete), null));

            return query.ToList();
        }
    }
}
