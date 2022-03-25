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
    public class ReguaContatoParametroRepository : DomainRepository<ReguaContatoParametro>, IReguaContatoParametroRepository
    {
        public ReguaContatoParametroRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<ReguaContatoParametro>> Buscar(int reguaContatoRegraId)
        {
            try
            {
                IQueryable<ReguaContatoParametro> query = await Task.FromResult(GenerateQuery(x => x.ReguaContatoRegraId == reguaContatoRegraId).AsNoTracking());

                //return (await this.GetAllAsync()).ToList();
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
