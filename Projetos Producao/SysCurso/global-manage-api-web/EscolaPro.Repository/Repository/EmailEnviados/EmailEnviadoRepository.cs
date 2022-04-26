using EscolaPro.Core.Model.Pagamentos;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Interfaces.EmailEnviados;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository.EmailEnviados
{
    public class EmailEnviadoRepository : DomainRepository<EmailEnviado>, IEmailEnviadoRepository
    {
        public EmailEnviadoRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<EmailEnviado>> BuscarPorPagamento(int pagamentoId)
        {
            try
            {
                IQueryable<EmailEnviado> query = await Task.FromResult(GenerateQuery((x => x.PagamentoId == pagamentoId), null));

                return query.AsNoTracking().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
