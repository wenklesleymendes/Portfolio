using EscolaPro.Core.Model.ReguaContato;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.ReguaContato
{
    public interface IReguaContatoParametroRepository : IDomainRepository<ReguaContatoParametro>
    {
        Task<List<ReguaContatoParametro>> Buscar(int reguaContatoRegraId);
    }
}
