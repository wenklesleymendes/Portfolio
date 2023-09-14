using EscolaPro.Core.Model.Atendimentos;
using EscolaPro.Repository.Interfaces.Atendimentos;

namespace EscolaPro.Repository.Repository
{
    public class AtendimentoOutboundRepository : DomainRepository<AtendimentoOutbound>, 
                                                 IAtendimentoOutboundRepository
    {
        public AtendimentoOutboundRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }
    }
}
