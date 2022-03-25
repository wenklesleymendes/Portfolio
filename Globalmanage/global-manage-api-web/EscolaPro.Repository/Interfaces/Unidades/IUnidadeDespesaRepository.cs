using EscolaPro.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces
{
    public interface IUnidadeDespesaRepository : IDomainRepository<UnidadeDespesa>
    {
        Task<List<UnidadeDespesa>> PorIdUnidade(int idUnidade);
    }
}
