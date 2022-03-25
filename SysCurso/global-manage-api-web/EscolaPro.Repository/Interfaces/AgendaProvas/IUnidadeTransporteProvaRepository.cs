using EscolaPro.Core.Model.Provas;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.AgendaProvas
{
    public interface IUnidadeTransporteProvaRepository : IDomainRepository<UnidadeTransporteProva>
    {
        IEnumerable<UnidadeTransporteProva> BuscarPorUnidadeId(int agendaProvaId, int unidadeId);

        IEnumerable<UnidadeTransporteProva> BuscarPorUnidadeEAgendaProvaEUnidadeTransporteProva(int idAgendaProva, int idUnidade, int idUnidadeTransporteProva);
    }
}
