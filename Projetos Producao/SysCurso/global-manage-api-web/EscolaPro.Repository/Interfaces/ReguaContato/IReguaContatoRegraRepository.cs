using EscolaPro.Core.Model.ReguaContato;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.ReguaContato
{
    public interface IReguaContatoRegraRepository : IDomainRepository<ReguaContatoRegra>
    {
        Task<List<ReguaContatoRegra>> BuscarRegra(TipoRegra tipoRegra);
    }
}
