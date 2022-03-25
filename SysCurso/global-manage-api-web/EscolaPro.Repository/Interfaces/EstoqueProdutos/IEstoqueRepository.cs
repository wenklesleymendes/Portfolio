using EscolaPro.Core.Model.EstoqueProdutos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.EstoqueProdutos
{
    public interface IEstoqueRepository : IDomainRepository<Produto>
    {
        Task<Produto> BuscarPorId(int idProduto);
    }
}
