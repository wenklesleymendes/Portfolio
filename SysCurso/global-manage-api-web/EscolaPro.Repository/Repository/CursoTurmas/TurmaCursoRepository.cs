using EscolaPro.Core.Model;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Scripts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository
{
    public class TurmaCursoRepository : DomainRepository<TurmaCurso>, ITurmaCursoRepository
    {
        public TurmaCursoRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        [Obsolete]
        public async Task<IEnumerable<Turma>> BuscarPorCursoId(int cursoId, int unidadeId, int? usuarioLogadoId)
        {
            try
            {
                string sqlQuery = TurmaScript.Filtrar(cursoId, unidadeId, usuarioLogadoId);

                var query = dbSet.FromSql(sqlQuery).Select(x => new Turma
                {
                    Id = x.Id,
                }).ToList();


                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<TurmaCurso>> BuscarPorIdTurma(int id)
        {
            IQueryable<TurmaCurso> query = await Task.FromResult(GenerateQuery((x => x.TurmaId == id), null));

            return query;
        }

        public async Task Deletar(TurmaCurso turmaCurso)
        {
            dbContext.Entry(turmaCurso).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            await dbContext.SaveChangesAsync();
        }
    }
}
