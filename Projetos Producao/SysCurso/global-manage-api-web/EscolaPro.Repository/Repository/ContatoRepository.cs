using EscolaPro.Core.Model;
using EscolaPro.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository
{
    public class ContatoRepository : DomainRepository<Contato>, IContatoRepository
    {
        public ContatoRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<Contato> PorIdContato(int idContato)
        {
            IQueryable<Contato> query = await Task.FromResult(GenerateQuery((x => x.Id == idContato), null));
            return query.FirstOrDefault();
        }

        public async Task<Contato> BuscarPorCelular(string caluar)
        {
            IQueryable<Contato> query = await Task.FromResult(GenerateQuery((x => x.Celular == caluar), null));
            return query.FirstOrDefault();
        }

        public async Task<Contato> BuscarPorEmail(string email) 
        { 
            IQueryable<Contato> query = await Task.FromResult(GenerateQuery((x => x.Email == email), null));
            return query.FirstOrDefault();
        }
    }
}
