using EscolaPro.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces
{
    public interface ICentroCustoRepository : IDomainRepository<CentroCusto>
    {
        Task<IEnumerable<CentroCusto>> BuscarPorUnidade(int idUnidade);
    }
}
