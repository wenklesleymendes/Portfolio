using EscolaPro.Core.Model.DadosFuncionario;
using EscolaPro.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository
{
    public class MateriaCursoProfessorRepository : DomainRepository<MateriaCursoProfessor>, IMateriaCursoProfessorRepository
    {
        public MateriaCursoProfessorRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<MateriaCursoProfessor>> BuscarPorCurso(int idCursoProfessor)
        {
            IQueryable<MateriaCursoProfessor> query = await Task.FromResult(GenerateQuery((x => x.CursoProfessorId == idCursoProfessor), null));

            return query.ToList();
        }
    }
}
