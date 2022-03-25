using EscolaPro.Core.Model.ReguaContato;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces.ReguaContato
{
    public interface IReguaContatoHistoricoService
    {
        Task<ReguaContatoHistorico> Inserir(ReguaContatoHistorico reguaContatoHistorico);
    }
}