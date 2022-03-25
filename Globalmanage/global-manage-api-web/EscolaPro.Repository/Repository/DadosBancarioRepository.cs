using EscolaPro.Core.Model;
using EscolaPro.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository
{
    public class DadosBancarioRepository : DomainRepository<DadosBancario>, IDadosBancarioRepository
    {
        public DadosBancarioRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public async Task<DadosBancario> PorIdDadosBancario(int idDadosBancario)
        {
            IQueryable<DadosBancario> query = await Task.FromResult(GenerateQuery((x => x.Id == idDadosBancario), null));

            return query.FirstOrDefault();
        }
    }
}
