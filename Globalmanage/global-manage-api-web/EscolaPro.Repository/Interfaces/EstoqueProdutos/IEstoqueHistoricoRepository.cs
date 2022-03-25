using EscolaPro.Core.Model.EstoqueProdutos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.EstoqueProdutos
{
    public interface IEstoqueHistoricoRepository : IDomainRepository<HistoricoEstoque>
    {
        Task<IEnumerable<HistoricoEstoque>> BuscarPorIdEstoque(int idEstoque);
    }
}
