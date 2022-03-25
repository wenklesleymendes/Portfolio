using EscolaPro.Core.Model.Atendimentos;
using EscolaPro.Repository.Interfaces.Atendimentos;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Repository.Repository.Atendimentos
{
    public class LeadsRepository : DomainRepository<Leads>, ILeadsRepository
    {
        public LeadsRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }
    }
}
