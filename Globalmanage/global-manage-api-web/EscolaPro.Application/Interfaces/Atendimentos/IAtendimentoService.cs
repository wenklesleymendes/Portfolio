using EscolaPro.Core.Model.Atendimentos;
using EscolaPro.Service.Dto.AtendimentoVO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces.Atendimentos
{
    public interface IAtendimentoService
    {
        Task<DtoAtendimento> Inserir(DtoAtendimento atendimento);

        Task<int> AtualizaStatus(int idAtendimento, int idStatus);

        Task UpdateAtendimento(DtoAtendimento atendimento);

        Task<IEnumerable<DtoAtendimento>> BuscarTodos();

        Task<IEnumerable<DtoAtendimento>> BuscarAgendamentos(int idUnidade);

        Task<DtoAtendimento> BuscarPorId(int idAtendimento);

        Task<DtoAtendimento> BuscaIdPorNumerodeCelular(string celularCliente);

        Task<bool> Deletar(int idAtendimento);

        Task<int> ContaAtendimentosExecutar(int idUnidade);

        Task DesativaAtendimentosVencidos(IEnumerable<Atendimento> registrosAtivos);

        Task<IEnumerable<Atendimento>> FiltraAtendimentos(FiltroAtendimentos filtro);

        Task EditaAtendimento(DtoAtendimento dtoAtendimento);
    }
}
