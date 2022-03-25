using EscolaPro.Core.Model;
using EscolaPro.Core.Model.Tickets;
using EscolaPro.Repository.Interfaces.Tickets;
using EscolaPro.Repository.Scripts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository.Tickets
{
    public class OcorrenciaRepository : DomainRepository<Ocorrencia>, IOcorrenciaRepository
    {
        public OcorrenciaRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<Ocorrencia>> BuscarPorMatriculaId(int matriculaId)
        {
            try
            {
                IQueryable<Ocorrencia> query = await Task.FromResult(GenerateQuery((x => x.MatriculaId == matriculaId), null)
                                            .Include(x => x.Matricula).ThenInclude(x => x.Aluno)
                                            .Include(x => x.Matricula).ThenInclude(x => x.Unidade)
                                            );
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
