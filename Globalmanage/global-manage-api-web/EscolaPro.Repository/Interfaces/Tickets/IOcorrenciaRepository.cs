using EscolaPro.Core.Model;
using EscolaPro.Core.Model.Tickets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.Tickets
{
    public interface IOcorrenciaRepository : IDomainRepository<Ocorrencia>
    {
        Task<IEnumerable<Ocorrencia>> BuscarPorMatriculaId(int matriculaId);
    }
}
