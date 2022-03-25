using EscolaPro.Core.Model.PainelMatricula;
using EscolaPro.Repository.Interfaces.Solicitacoes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository.Solicitacoes
{
    public class SolicitacaoAlunoRepository : DomainRepository<SolicitacaoAluno>, ISolicitacaoAlunoRepository
    {
        public SolicitacaoAlunoRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<SolicitacaoAluno>> BuscarHistorico(int matriculaId)
        {
            try
            {
                IQueryable<SolicitacaoAluno> query = await Task.FromResult(GenerateQuery((x => x.MatriculaId == matriculaId), null)
                    .Include(x => x.Solicitacao).AsNoTracking());

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<SolicitacaoAluno> BuscarPorId(int solicitacaoId)
        {
            try
            {
                IQueryable<SolicitacaoAluno> query = await Task.FromResult(GenerateQuery((x => x.Id == solicitacaoId), null)
                    .Include(x => x.Solicitacao)
                    .ThenInclude(x=> x.EmailDestinatario)
                    .Include(x=> x.Solicitacao)
                    .ThenInclude(x=> x.SolicitacaoFuncionarioTicket));

                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
