using EscolaPro.Service.Dto.AtendimentoVO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces.Atendimentos
{
    public interface IAtendimentoService
    {
        Task<DtoAtendimento> Inserir(DtoAtendimento atendimento);
        Task<IEnumerable<DtoAtendimento>> BuscarTodos();
        Task<IEnumerable<DtoAtendimento>> BuscarAgendamentos(int idUnidade);
        Task<DtoAtendimento> BuscarPorId(int idAtendimento);
        Task<bool> Deletar(int idAtendimento);
    }
}
