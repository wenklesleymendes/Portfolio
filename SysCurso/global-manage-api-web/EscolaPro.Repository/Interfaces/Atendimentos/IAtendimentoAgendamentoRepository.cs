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
        Task<AtendimentoAgendamento> BuscarPorIdAtendimento(int idAtendimentoAgendamento);
        Task<bool> Excluir(int idAtendimentoAgendamento);
    }
}
