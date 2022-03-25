using EscolaPro.Core.Model.ReguaContato;
using EscolaPro.Repository.Interfaces.ReguaContato;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository.ReguaContato
{
    public class ReguaContatoHistoricoRepository : DomainRepository<ReguaContatoHistorico>, IReguaContatoHistoricoRepository
    {
        public ReguaContatoHistoricoRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }
    }
}
