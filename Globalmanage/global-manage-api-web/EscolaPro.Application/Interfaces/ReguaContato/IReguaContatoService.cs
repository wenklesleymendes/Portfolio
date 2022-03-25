using EscolaPro.Core.Model.ReguaContato;
using EscolaPro.Service.Dto.ReguaContato;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces.ReguaContato
{
    public interface IReguaContatoService
    {
        Task CarregaReguaContatoCobranca();
        Task<IEnumerable<ReguaContatoFila>> GetContatoFilas(TipoMensagem tipoMensagem);
        Task DispararContato(ReguaContatoFilaDto reguaContatoFila);
    }
}
