using EscolaPro.Core.Model.ReguaContato;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.ReguaContato
{
    public interface IReguaContatoFilaRepository : IDomainRepository<ReguaContatoFila>
    {
        Task<IEnumerable<ReguaContatoFila>> BuscarPorRegraId(int reguaContatoRegrasId);
        Task<List<ReguaContatoFila>> BuscarPorTipoMensage(TipoMensagem tipoMensagem);
        Task<ReguaContatoFila> BuscarPorId(int id);
        Task UpdateStatus(List<ReguaContatoFila> lista, StatusFila statusFila);
        Task UpdateStatus(int id, StatusFila statusFila);
    }
}
