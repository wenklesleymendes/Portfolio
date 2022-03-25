using EscolaPro.Core.Model.Provas;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Interfaces.AgendaProvas;
using EscolaPro.Repository.Scripts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository.AgendaProvas
{
    public class AgendaProvaRepository : DomainRepository<AgendaProva>, IAgendaProvaRepository
    {
        public AgendaProvaRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task AdicionarUnidadeParticipante(List<UnidadeParticipanteProva> unidadeParticipanteProvas, int idAgendaProva)
        {
            try
            {
                foreach (var item in unidadeParticipanteProvas)
                {
                    item.Id = 0;
                    item.AgendaProvaId = idAgendaProva;
                    dbContext.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }

                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AgendaProva> BuscarPorId(int agendaProvaId)
        {
            try
            {
                IQueryable<AgendaProva> query = await Task.FromResult(GenerateQuery((x => x.Id == agendaProvaId), null)
                                                           .Include(x => x.ColegioAutorizado)
                                                                .ThenInclude(x => x.Endereco)
                                                           .Include(x => x.AgendaCurso));

                return query.AsNoTracking().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //[Obsolete]
        //public async Task<IEnumerable<AgendaProva>> BuscarProvasDisponiveis(int unidadeId)
        //{
        //    try
        //    {
        //        string sqlQuery = AgendaProvaScript.Filtrar(unidadeId);

        //        var query = dbSet.FromSql(sqlQuery).Select(x => new AgendaProva
        //        {
        //            Id = x.Id,
        //        }).ToList();

        //        return query.ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public List<UnidadeParticipanteProva> BuscarUnidadeParticipante(int agendaProvaId)
        {
            try
            {
                var unidadeParticipante = dbContext.Set<UnidadeParticipanteProva>().Where(x => x.AgendaProvaId == agendaProvaId).AsNoTracking().ToList();

                return unidadeParticipante;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Deletar(int idAgendaProva)
        {
            try
            {
                var unidadeParticipanteProvas = dbContext.Set<UnidadeParticipanteProva>().Where(x => x.AgendaProvaId == idAgendaProva).ToList();

                foreach (var item in unidadeParticipanteProvas)
                {
                    dbContext.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                }

                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<AgendaProva> BuscarProvasDisponiveis(int colegioId, int unidadeId, int cursoId)
        {
            var today = DateTime.Now.Date;
            //return dbContext.Set<AgendaProva>()
            //    .Include(x => x.UnidadeParticipanteProva)
            //    .Include(x => x.AgendaCurso)
            //    .Where(x => x.IsDelete == false &&
            //        x.InicioInscricao <= today &&
            //        x.TerminoInscricao >= today &&
            //        x.UnidadeParticipanteProva.Any(y => y.UnidadeId == unidadeId) &&
            //        (cursoId == 0 || x.AgendaCurso.Any(y => y.CursoId == cursoId)))
            //    .AsNoTracking()
            //    .Distinct()
            //    .ToList();
            return dbContext.Set<AgendaProva>()
                .Include(x => x.AgendaCurso)
                .Include(x => x.UnidadeParticipanteProva)
                .Where(x => x.IsDelete == false &&
                    x.InicioInscricao <= today &&
                    x.TerminoInscricao >= today &&
                    x.ColegioAutorizadoId == colegioId &&
                    (cursoId == 0 || x.AgendaCurso.Any(y => y.CursoId == cursoId)) &&
                    x.UnidadeParticipanteProva.Any(y => y.UnidadeId == unidadeId))
                .AsNoTracking()
                .Distinct()
                .ToList();
        }

        public UnidadeParticipanteProva BuscarUnidadeParticipante(int agendaProvaId, int unidadeId)
        {
            return dbContext.Set<UnidadeParticipanteProva>()
                .Where(x => x.AgendaProvaId == agendaProvaId && x.UnidadeId == unidadeId)
                .FirstOrDefault();
        }
    }
}
