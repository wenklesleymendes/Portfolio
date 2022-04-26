using EscolaPro.Core.Model.PainelMatricula;
using EscolaPro.Repository.Interfaces.MatriculaAlunos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository.MatriculaAlunos
{
    public class ProvaMateriaAlunoRepository : DomainRepository<ProvaMateriaAluno>, IProvaMateriaAlunoRepository
    {
        public ProvaMateriaAlunoRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<ProvaMateriaAluno>> BuscarPorProvaId(int provaAlunoId)
        {
            IQueryable<ProvaMateriaAluno> query = await Task.FromResult(GenerateQuery((x => x.ProvaAlunoId == provaAlunoId), null));

            return query.ToList();
        }
     
    }
}
