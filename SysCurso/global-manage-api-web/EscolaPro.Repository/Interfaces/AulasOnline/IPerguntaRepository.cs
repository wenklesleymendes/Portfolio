using EscolaPro.Core.Model.AulasOnline;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.AulasOnline
{
    public interface IPerguntaRepository : IDomainRepository<Pergunta>
    {
        Task<Pergunta> BuscarPorId(int perguntaId);
        Task<IEnumerable<Pergunta>> BuscarPorVideoAula(int videoAulaId);
        Task<Pergunta> Atualizar(Pergunta pergunta, List<Resposta> respostas);
    }
}
