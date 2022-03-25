using EscolaPro.Core.Model;
using EscolaPro.Core.Model.Atendimentos;
using EscolaPro.Core.Model.ReguaContato;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.Atendimentos
{
    public interface IAtendimentoRepository : IDomainRepository<Atendimento>
    {
        Task<Atendimento> BuscarPorId(int atendimentoId);

        Task UpdateListaStatus(IEnumerable<Atendimento> lista);

        Task UpdateStatus(int id, StatusAtendimento statusFila);

        Task AtualizaStatusAtendimento(Atendimento atendimento);

        Task<Atendimento> BuscaPorCelular(string celularCliente);

        Task<IEnumerable<Atendimento>> FiltrarAtendimento(FiltroAtendimentos filtro);

        Task<int> ObtenhaQtdAtendimentoPorStatus(int idUnidade, int idStatus);

        Task<IEnumerable<Atendimento>> BuscaAtendimentosParaFila(int idUnidade);

        Task<IEnumerable<Atendimento>> BuscaAtendimentosExecucaoProximoDia(int idUnidade);
    }
}
