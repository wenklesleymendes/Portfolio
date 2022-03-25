using EscolaPro.Core.Model.Atendimentos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.Atendimentos
{
    public interface IAtendimentoOutboundRepository : IDomainRepository<AtendimentoOutbound>
    {
        Task<IEnumerable<AtendimentoOutbound>> RetornaHistoricoOutbound(int idAtendimento);
    }
}
