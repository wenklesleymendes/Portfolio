using EscolaPro.Core.Model;
using EscolaPro.Core.Model.Atendimentos;
using EscolaPro.Repository.Interfaces.Atendimentos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository
{
    public class AtendimentoRepository : DomainRepository<Atendimento>,
                                         IAtendimentoRepository
    {
        public AtendimentoRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public async Task<Atendimento> BuscarPorId(int atendimentoId)
        {
            try
            {
                IQueryable<Atendimento> query = await Task.FromResult(GenerateQuery((x => x.Id == atendimentoId), null)
                    .AsNoTracking());

                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
