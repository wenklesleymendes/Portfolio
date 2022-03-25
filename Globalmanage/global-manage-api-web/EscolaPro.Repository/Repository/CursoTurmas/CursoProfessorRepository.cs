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
    public class CursoProfessorRepository : DomainRepository<CursoProfessor>, ICursoProfessorRepository
    {
        public CursoProfessorRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<CursoProfessor>> BuscarPorFuncionarioId(int idFuncionario)
        {
            IQueryable<CursoProfessor> query = await Task.FromResult(GenerateQuery((x => x.FuncionarioId == idFuncionario), null));

            return query.ToList();
        }
    }
}
