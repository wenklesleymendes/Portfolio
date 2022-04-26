using EscolaPro.Core.Model;
using EscolaPro.Core.Model.Atendimentos;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.Atendimentos
{
    public interface IAtendimentoRepository : IDomainRepository<Atendimento>
    {
        Task<Atendimento> BuscarPorId(int atendimentoId);
    }
}
