using EscolaPro.Core.Model.AulasOnline;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.AulasOnline
{
    public interface IRespostaRepository : IDomainRepository<Resposta>
    {
        Task<IEnumerable<Resposta>> BuscarPorPergunta(int perguntaId);
    }
}
