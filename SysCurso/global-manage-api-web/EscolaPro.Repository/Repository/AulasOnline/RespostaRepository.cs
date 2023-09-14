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
    public class RespostaRepository : DomainRepository<Resposta>, IRespostaRepository
    {
        public RespostaRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Resposta>> BuscarPorPergunta(int perguntaId)
        {
            try
            {
                IQueryable<Resposta> query = await Task.FromResult(GenerateQuery((x => x.PerguntaId == perguntaId), null));

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
