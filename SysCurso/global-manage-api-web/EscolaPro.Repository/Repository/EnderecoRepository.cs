using EscolaPro.Core.Model;
using EscolaPro.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository
{
    public class EnderecoRepository : DomainRepository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public async Task<Endereco> PorIdEndereco(int idEndereco)
        {
            IQueryable<Endereco> query = await Task.FromResult(GenerateQuery((x => x.Id == idEndereco), null));

            return query.FirstOrDefault();
        }
    }
}
