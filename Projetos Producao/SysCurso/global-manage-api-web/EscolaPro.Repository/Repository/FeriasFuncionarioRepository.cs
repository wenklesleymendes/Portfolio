using EscolaPro.Core.Model.DadosFuncionario;
using EscolaPro.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository
{
    public class FeriasFuncionarioRepository : DomainRepository<FeriasFuncionario>, IFeriasFuncionarioRepository
    {
        public FeriasFuncionarioRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<FeriasFuncionario>> BuscarPorIdFuncionario(int idFuncionario)
        {
            var query = await Task.FromResult(GenerateQuery((x => x.FuncionarioId == idFuncionario && !x.IsDelete), null));

            return query.ToList();
        }
    }
}
