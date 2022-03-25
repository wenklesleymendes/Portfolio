using EscolaPro.Core.Model.Provas;
using EscolaPro.Repository.Interfaces.AgendaProvas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository.AgendaProvas
{
    public class UnidadeTransporteProvaRepository : DomainRepository<UnidadeTransporteProva>, IUnidadeTransporteProvaRepository
    {
        public UnidadeTransporteProvaRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<UnidadeTransporteProva> BuscarPorUnidadeId(int agendaProvaId, int unidadeId)
        {
            return dbContext.Set<UnidadeTransporteProva>()
                .Include(x => x.UnidadeParticipanteProva)
                .Include(x => x.ProvaAlunos)
                .Where(x => x.AgendaProvaId == agendaProvaId && x.UnidadeParticipanteProva.UnidadeId == unidadeId)
                .OrderBy(x => x.NumeroOnibus)
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<UnidadeTransporteProva> BuscarPorUnidadeEAgendaProvaEUnidadeTransporteProva(int idAgendaProva, int idUnidade, int idUnidadeTransporteProva)
        {
            return dbContext.Set<UnidadeTransporteProva>()
                .Include(x => x.UnidadeParticipanteProva)
                .Where(x => x.Id == idUnidadeTransporteProva &&
                            x.AgendaProvaId == idAgendaProva && 
                            x.UnidadeParticipanteProva.UnidadeId == idUnidade)
                .OrderBy(x => x.NumeroOnibus)
                .AsNoTracking()
                .ToList();
        }

    }
}
