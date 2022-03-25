using EscolaPro.Core.Model;
using EscolaPro.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository
{
    public class ContratoLocacaoRepository : DomainRepository<ContratoLocacao>, IContratoLocacaoRepository
    {
        public ContratoLocacaoRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public async Task<ContratoLocacao> PorIdContratoLocacao(int idContratoLocacao)
        {
            IQueryable<ContratoLocacao> query = await Task.FromResult(GenerateQuery((x => x.Id == idContratoLocacao), null));

            return query.FirstOrDefault();
        }
    }
}
