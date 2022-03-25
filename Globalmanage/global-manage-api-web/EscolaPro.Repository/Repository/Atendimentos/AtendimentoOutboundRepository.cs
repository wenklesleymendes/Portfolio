using EscolaPro.Core.Model.Atendimentos;
using EscolaPro.Repository.Interfaces.Atendimentos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository
{
    public class AtendimentoOutboundRepository : DomainRepository<AtendimentoOutbound>, 
                                                 IAtendimentoOutboundRepository
    {
        public AtendimentoOutboundRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<AtendimentoOutbound>> RetornaHistoricoOutbound(int idAtendimento)
        {
            IQueryable<AtendimentoOutbound> outbounds = await Task.FromResult(GenerateQuery((u => u.AtendimentoId == idAtendimento), null));

            return outbounds;
        }
    }
}
