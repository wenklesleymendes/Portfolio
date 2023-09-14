using EscolaPro.Core.Model.Provas;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.AgendaProvas
{
    public interface IAgendaProvaRepository : IDomainRepository<AgendaProva>
    {
        Task Deletar(int idAgendaProva);
        Task AdicionarUnidadeParticipante(List<UnidadeParticipanteProva> unidadeParticipanteProvas, int idAgendaProva);
        Task<AgendaProva> BuscarPorId(int AgendaProvaId);
        List<UnidadeParticipanteProva> BuscarUnidadeParticipante(int agendaProvaId);
        //Task<IEnumerable<AgendaProva>> BuscarProvasDisponiveis(int unidadeId);
        IEnumerable<AgendaProva> BuscarProvasDisponiveis(int colegioId, int unidadeId, int cursoId = 0);
        UnidadeParticipanteProva BuscarUnidadeParticipante(int agendaProvaId, int unidadeId);
    }
}
