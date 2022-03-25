using EscolaPro.Core.Model.Atendimentos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.Atendimentos
{
    public interface ILeadsRepository : IDomainRepository<Leads>
    {
        Task<IEnumerable<Leads>> BuscaLeadsPorStatus(int status);
    }
}
