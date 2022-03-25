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
    public class CancelamentoMatriculaRepository : DomainRepository<CancelamentoMatricula>, ICancelamentoMatriculaRepository
    {
        public CancelamentoMatriculaRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public async Task<CancelamentoMatricula> BuscarPorMatricula(int matriculaId)
        {
            IQueryable<CancelamentoMatricula> query = await Task.FromResult(GenerateQuery((x => x.MatriculaAlunoId == matriculaId)
                                                                          , null).Include(x=> x.MatriculaAluno)
                                                                                 .Include(x => x.CancelamentoIsencaos)
                                                                                 .ThenInclude(x=> x.CancelamentoIsencaoPagamentos));

            return query.FirstOrDefault();
        }
    }
}
