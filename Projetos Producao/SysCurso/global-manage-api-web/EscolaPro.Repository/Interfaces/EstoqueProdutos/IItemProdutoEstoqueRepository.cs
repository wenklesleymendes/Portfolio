using EscolaPro.Core.Model.EstoqueProdutos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.EstoqueProdutos
{
    public interface IItemProdutoEstoqueRepository : IDomainRepository<ItemProduto>
    {
        Task<List<ItemProduto>> BuscarItemProdutoPorProdutoId(int idProduto);
        Task<ItemProduto> BuscarEstoqueDisponivel(int idProduto);
    }
}
