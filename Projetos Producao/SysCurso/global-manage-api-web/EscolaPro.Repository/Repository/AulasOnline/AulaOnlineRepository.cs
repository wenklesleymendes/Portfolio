using EscolaPro.Core.Model;
using EscolaPro.Core.Model.AulasOnline;
using EscolaPro.Repository.Interfaces.AulasOnline;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository.AulasOnline
{
    public class AulaOnlineRepository : DomainRepository<AulaOnline>, IAulaOnlineRepository
    {
        public AulaOnlineRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task Atualizar(AulaOnline aulaOnline)
        {
            try
            {
                foreach (var curso in aulaOnline.Curso.Where(x => x.Id > 0))
                {

                    dbContext.Entry<CursoOnline>(curso).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                    //dbContext.Entry(curso).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                    await dbContext.SaveChangesAsync();
                }

                foreach (var curso in aulaOnline.Curso)
                {
                    curso.Id = 0; 
                    curso.AulaOnlineId = aulaOnline.Id;
                    dbContext.Entry<CursoOnline>(curso).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    //dbContext.Entry(curso).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    await dbContext.SaveChangesAsync();
                }

                aulaOnline.Curso = new List<CursoOnline>();
                dbContext.Entry(aulaOnline).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AulaOnline> BuscarPorCurso(int cursoId)
        {
            try
            {
                IQueryable<AulaOnline> query = null;

                var curso = dbContext.Set<CursoOnline>().Where(x => x.CursoId == cursoId).ToList();

                if (curso.Count > 0)
                {
                    query = await Task.FromResult(GenerateQuery(x => x.Id == curso.FirstOrDefault().AulaOnlineId, null));
                }
                else
                {
                    return new AulaOnline();
                }

                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AulaOnline> BuscarPorId(int aulaOnlineId)
        {
            try
            {
                IQueryable<AulaOnline> query = await Task.FromResult(GenerateQuery((x => x.Id == aulaOnlineId), null)
                                                         .Include(x => x.Curso));

                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
