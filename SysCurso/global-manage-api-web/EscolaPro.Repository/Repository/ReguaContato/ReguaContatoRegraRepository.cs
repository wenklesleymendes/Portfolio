using EscolaPro.Core.Model.ReguaContato;
using EscolaPro.Repository.Interfaces.ReguaContato;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository.ReguaContato
{
    public class ReguaContatoRegraRepository : DomainRepository<ReguaContatoRegra>, IReguaContatoRegraRepository
    {
        public ReguaContatoRegraRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<ReguaContatoRegra>> BuscarRegra(TipoRegra tipoRegra)
        {
            try
            {
                IQueryable<ReguaContatoRegra> query = await Task.FromResult(GenerateQuery(x => x.TipoRegra == tipoRegra).Include(x=> x.ReguaContatoParametro).AsNoTracking());

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
