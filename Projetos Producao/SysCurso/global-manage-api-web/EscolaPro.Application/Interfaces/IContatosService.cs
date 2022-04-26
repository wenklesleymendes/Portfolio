using EscolaPro.Service.Dto.UnidadeVO;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface IContatosService
    {
        Task<bool> ExisteContatoCelular(string celualar);
        Task<bool> ExisteContatoEmail(string email);
        Task<DtoContato> BuscarPorEmail(string email);
        Task<DtoContato> BuscarPorCelular(string celular);
    }
}
