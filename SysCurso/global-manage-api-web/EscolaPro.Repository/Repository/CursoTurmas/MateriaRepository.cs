using EscolaPro.Core.Model.CursoTurma;
using EscolaPro.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository
{
    public class MateriaRepository : DomainRepository<Materia>, IMateriaRepository
    {
        public MateriaRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Materia>> BuscarPorIdCurso(int idCurso)
        {
            IQueryable<Materia> query = await Task.FromResult(GenerateQuery((x => x.CursoId == idCurso && !x.IsDelete), null));

            return query.OrderBy(x=>x.Ordenacao).ToList();
        }
    }
}
