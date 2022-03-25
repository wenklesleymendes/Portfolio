using EscolaPro.Core.Model;
using EscolaPro.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository
{
    public class ParametroRepository : DomainRepository<Parametro>, IParametroRepository
    {
        public ParametroRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<string> BuscarParametroPorChave(string chave)
        {
            IQueryable<Parametro> query = await Task.FromResult(GenerateQuery((x => x.Chave == chave), null));

            return query.FirstOrDefault().Valor;
        }

        public async Task<string> BuscarParametroPorId(int id)
        {
            IQueryable<Parametro> query = await Task.FromResult(GenerateQuery((x => x.Id == id), null));

            return query.FirstOrDefault().Valor;
        }
    }
}
