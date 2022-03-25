using EscolaPro.Core.Model.Tickets;
using EscolaPro.Repository.Interfaces.Tickets;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository.Tickets
{
    public class FuncionarioAssuntoTicketRepository: DomainRepository<FuncionarioAssuntoTicket>, IFuncionarioAssuntoTicketRepository
    {
        public FuncionarioAssuntoTicketRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public  async Task<List<FuncionarioAssuntoTicket>> BuscarPorAssuntoId(int idAssumto)
        {
            IQueryable<FuncionarioAssuntoTicket> query = await Task.FromResult(GenerateQuery((x => x.AssuntoTicketId == idAssumto && !x.IsDelete), null));
                            

            return query.ToList();

        }

        public async Task ApagarFuncionariosAntigo(int assuntoId)
        {
            try
            {
                var solicitacaoOLDs = dbContext.Set<FuncionarioAssuntoTicket>().Where(x => x.AssuntoTicketId == assuntoId).ToList();

                foreach (var item in solicitacaoOLDs)
                {
                    dbContext.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                }

                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
