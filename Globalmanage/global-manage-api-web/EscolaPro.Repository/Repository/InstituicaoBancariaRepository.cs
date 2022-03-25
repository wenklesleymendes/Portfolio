using EscolaPro.Core.Model;
using EscolaPro.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository
{
    public class InstituicaoBancariaRepository : DomainRepository<InstituicaoBancaria>, IInstituicaoBancariaRepository
    {
        public InstituicaoBancariaRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public async Task<InstituicaoBancaria> BuscarPorCodigoBanco(string codigoBanco)
        {
            try
            {
                IQueryable<InstituicaoBancaria> query = await Task.FromResult(GenerateQuery((x => x.CodigoBanco == codigoBanco), null));

                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
