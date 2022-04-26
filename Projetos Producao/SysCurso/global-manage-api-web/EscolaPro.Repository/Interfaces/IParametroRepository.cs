using EscolaPro.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces
{
    public interface IParametroRepository : IDomainRepository<Parametro>
    {
        Task<string> BuscarParametroPorId(int id);
        Task<string> BuscarParametroPorChave(string chave);
    }
}
