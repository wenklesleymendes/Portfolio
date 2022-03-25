using EscolaPro.Core.Model.Atendimentos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.Atendimentos
{
    public interface IAtendimentoAgendamentoRepository : IDomainRepository<AtendimentoAgendamento>
    {
        Task<AtendimentoAgendamento> Inserir(AtendimentoAgendamento atendimentoAgendamento);
        Task<IEnumerable<AtendimentoAgendamento>> BuscarTodos();
        Task<AtendimentoAgendamento> BuscarPorIdAgendamento(int idAtendimentoAgendamento);
        Task<AtendimentoAgendamento> BuscaPorIdAtendimento(int idAtendimento);
        Task<IEnumerable<AtendimentoAgendamento>> HistoricoTentativas(int idAtendimento);
        Task<bool> Excluir(int idAtendimentoAgendamento);
    }
}
