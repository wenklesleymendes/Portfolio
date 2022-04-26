using EscolaPro.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces
{
    public interface IUnidadeTurmaRepository : IDomainRepository<TurmaUnidade>
    {
        Task<IEnumerable<TurmaUnidade>> BuscarPorIdTurma(int id);
        Task Deletar(TurmaUnidade unidadeTurma);
    }
}
