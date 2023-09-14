using EscolaPro.Core.Model;
using EscolaPro.Core.Model.CursoTurma;
using EscolaPro.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository
{
    public class CursoRepository : DomainRepository<Curso>, ICursoRepository
    {
        public CursoRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Curso>> BuscarCursosComMateria()
        {
            var cursos = await GetAllAsync();

            List<Curso> cursoRetorno = new List<Curso>();

            foreach (var item in cursos.Where(x => !x.IsDelete))
            {
                item.Materia = dbContext.Set<Materia>().Where(x => x.CursoId == item.Id && !x.IsDelete).ToList();
                cursoRetorno.Add(item);
            }

            return cursoRetorno;
        }

        public async Task<Curso> BuscarPorId(int idCurso)
        {
            try
            {
                var curso = await GetByIdAsync(idCurso);

                curso.Materia = dbContext.Set<Materia>().Where(x => x.CursoId == idCurso && !x.IsDelete).ToList();

                return curso;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Materia>> InserirMateria(Materia materia)
        {
            if (materia.Id == 0)
            {
                dbContext.Entry<Materia>(materia).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            }
            else
            {
                dbContext.Entry<Materia>(materia).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }

            await dbContext.SaveChangesAsync();

            return dbContext.Set<Materia>().Where(x => x.CursoId == materia.CursoId).ToList();
        }

        public async Task<bool> RemoverMateria(int idMateria)
        {
            var materia = dbContext.Set<Materia>().Where(x => x.Id == idMateria).FirstOrDefault();
            materia.IsDelete = true;
            dbContext.Entry<Materia>(materia).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await dbContext.SaveChangesAsync();
            return materia.IsDelete;
        }
    }
}
