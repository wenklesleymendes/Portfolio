using EscolaPro.Core.Model.Provas;
using EscolaPro.Repository.Interfaces.AgendaProvas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository.AgendaProvas
{
    public class ColegioAutorizadoRepository : DomainRepository<ColegioAutorizado>, IColegioAutorizadoRepository
    {
        public ColegioAutorizadoRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<ColegioAutorizado> BuscarPorId(int colegioAutorizadoId)
        {
            try
            {
                IQueryable<ColegioAutorizado> query = await Task.FromResult(GenerateQuery((x => x.Id == colegioAutorizadoId), null)
                                                                .Include(x => x.Endereco));

                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
