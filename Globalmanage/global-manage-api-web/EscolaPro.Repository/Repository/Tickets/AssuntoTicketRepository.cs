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
    public class AssuntoTicketRepository : DomainRepository<AssuntoTicket>, IAssuntoTicketRepository
    {
        public AssuntoTicketRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<AssuntoTicket> BuscarPorId(int idTicket)
        {
            IQueryable<AssuntoTicket> query = await Task.FromResult(GenerateQuery((x => x.Id == idTicket), null)
                                        .Include(x => x.CentroCusto)
                                        .Include(x => x.Unidade)
                                        .Include(x => x.FuncionarioAssuntoTicket)
                                        .ThenInclude(x=> x.Usuario)
                                        .ThenInclude(x=> x.Funcionario));

            return query.FirstOrDefault();
        }


        public async Task<List<AssuntoTicket>> BuscarPorUnidadeDepartamento(int? idUnidade, int? idDepartamento)
        {
            IQueryable<AssuntoTicket> query = await Task.FromResult(GenerateQuery((x => (idUnidade == null || x.UnidadeId == idUnidade)
                                         && (idDepartamento == null || x.CentroCustoId == idDepartamento)
                                         && !x.IsDelete), null)
                                        .Include(x => x.CentroCusto)
                                        .Include(x => x.Unidade)
                                        .Include(x => x.FuncionarioAssuntoTicket));

            return query.ToList();
        }


        public async Task<List<AssuntoTicket>> BuscarTodos()
        {
            IQueryable<AssuntoTicket> query = await Task.FromResult(GenerateQuery((x => !x.IsDelete), null)
                                        .Include(x => x.CentroCusto)
                                        .Include(x => x.Unidade)
                                        .Include(x => x.FuncionarioAssuntoTicket));

            return query.ToList();
        }
        public async Task<AssuntoTicket> BuscarAssuntoSolicitacao()
        {
            try
            {
                IQueryable<AssuntoTicket> query = await Task.FromResult(GenerateQuery(null));

                return query.AsNoTracking().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AssuntoTicket BuscarBaixaBoleto()
        {
            return dbContext.Set<AssuntoTicket>().AsNoTracking()
                .Include(x => x.CentroCusto)
                .Include(x => x.Unidade)
                .Include(x => x.FuncionarioAssuntoTicket)
                .ThenInclude(x => x.Usuario)
                .ThenInclude(x => x.Funcionario)
                .FirstOrDefault(x => !x.IsDelete && x.Descricao == "Baixa de boleto por pagamento via cartão");
        }

        public AssuntoTicket BuscarAnaliseDocumentacaoProva()
        {
            return dbContext.Set<AssuntoTicket>().AsNoTracking()
                .Include(x => x.CentroCusto)
                .Include(x => x.Unidade)
                .Include(x => x.FuncionarioAssuntoTicket)
                .ThenInclude(x => x.Usuario)
                .ThenInclude(x => x.Funcionario)
                .FirstOrDefault(x => !x.IsDelete && x.Descricao == "Análise documentação prova");
        }

        public AssuntoTicket BuscarAuditoriaCancelamento()
        {
            return dbContext.Set<AssuntoTicket>().AsNoTracking()
                .Include(x => x.CentroCusto)
                .Include(x => x.Unidade)
                .Include(x => x.FuncionarioAssuntoTicket)
                .ThenInclude(x => x.Usuario)
                .ThenInclude(x => x.Funcionario)
                .FirstOrDefault(x => !x.IsDelete && x.Descricao == "Auditoria de Cancelamento");
        }
    }
}
