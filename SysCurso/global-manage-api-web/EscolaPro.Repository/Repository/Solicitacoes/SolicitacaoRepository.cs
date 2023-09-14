using EscolaPro.Core.Model.Solicitacoes;
using EscolaPro.Repository.Interfaces.Solicitacoes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository.Solicitacoes
{
    public class SolicitacaoRepository : DomainRepository<Solicitacao>, ISolicitacaoRepository
    {
        public SolicitacaoRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task ApagarEmailsAntigo(int solicitacaoId)
        {
            try
            {
                var solicitacaoOLDs = dbContext.Set<EmailDestinatario>().Where(x => x.SolicitacaoId == solicitacaoId).ToList();

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

        public async Task<IEnumerable<Solicitacao>> BuscarPorCursoId(int cursoId)
        {
            try
            {
                var solicitacaoCursoLista = dbContext.Set<SolicitacaoCurso>().Where(x => x.CursoId == cursoId).ToList();

                List<Solicitacao> solicitacaoLista = new List<Solicitacao>();

                foreach (var solicitacaoCurso in solicitacaoCursoLista)
                {
                    var solicitacao = dbContext.Set<Solicitacao>().Where(x => x.Id == solicitacaoCurso.SolicitacaoId && !x.IsDelete)
                                                                  .Include(x=> x.StatusProvaEnum)
                                                                  .Include(x=> x.StatusCertificado)
                                                                  .FirstOrDefault();

                    if (solicitacao != null)
                    {
                        solicitacaoLista.Add(solicitacao);
                    }
                }

                return solicitacaoLista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Solicitacao> BuscarPorId(int solicitacaoId)
        {
            try
            {
                IQueryable<Solicitacao> query = await Task.FromResult(GenerateQuery((x => x.Id == solicitacaoId), null)
                    .Include(x => x.SolicitacaoCurso)
                    .Include(x => x.SolicitacaoFuncionarioTicket)
                    .Include(x => x.EmailDestinatario)
                    .Include(x => x.StatusCertificado)
                    .Include(x => x.StatusProvaEnum).AsNoTracking());

                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Solicitacao>> BuscarTodos()
        {
            try
            {
                IQueryable<Solicitacao> query = await Task.FromResult(GenerateQuery((x => !x.IsDelete), null)
               .Include(x => x.SolicitacaoCurso)
                    .Include(x => x.SolicitacaoFuncionarioTicket)
                    .Include(x => x.EmailDestinatario)
                    .Include(x => x.StatusCertificado)
                    .Include(x => x.StatusProvaEnum));

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task InserirCertificados(List<StatusCertificado> statusCertificados, int solicitacaoId)
        {
            try
            {
                var solicitacaoOLDs = dbContext.Set<StatusCertificado>().Where(x => x.SolicitacaoId == solicitacaoId).ToList();

                foreach (var item in solicitacaoOLDs)
                {
                    dbContext.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                }

                await dbContext.SaveChangesAsync();

                foreach (var item in statusCertificados)
                {
                    dbContext.Entry(item).State = EntityState.Added;
                }

                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task InserirCursoSolicitacao(List<SolicitacaoCurso> solicitacaoCurso, int solicitacaoId)
        {
            try
            {
                var solicitacaoOLDs = dbContext.Set<SolicitacaoCurso>().Where(x => x.SolicitacaoId == solicitacaoId).ToList();

                foreach (var item in solicitacaoOLDs)
                {
                    dbContext.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                }

                await dbContext.SaveChangesAsync();

                foreach (var item in solicitacaoCurso)
                {
                    dbContext.Entry(item).State = EntityState.Added;
                }

                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task InserirEmails(List<EmailDestinatario> emailDestinatarios, int solicitacaoId)
        {
            try
            {
                await ApagarEmailsAntigo(solicitacaoId);

                foreach (var item in emailDestinatarios)
                {
                    dbContext.Entry(item).State = EntityState.Added;
                }

                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task InserirProvasSolicitacao(List<StatusProva> statusProvas, int solicitacaoId)
        {
            try
            {
                var solicitacaoOLDs = dbContext.Set<StatusProva>().Where(x => x.SolicitacaoId == solicitacaoId).ToList();

                foreach (var item in solicitacaoOLDs)
                {
                    dbContext.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                }

                await dbContext.SaveChangesAsync();

                foreach (var item in statusProvas)
                {
                    dbContext.Entry(item).State = EntityState.Added;
                }

                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task InserirSolicitacaoFuncionarioTicket(List<SolicitacaoFuncionarioTicket> solicitacaoFuncionarioTickets, int solicitacaoId)
        {
            try
            {
                var solicitacaoOLDs = dbContext.Set<SolicitacaoFuncionarioTicket>().Where(x => x.SolicitacaoId == solicitacaoId).ToList();

                foreach (var item in solicitacaoOLDs)
                {
                    dbContext.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                }

                await dbContext.SaveChangesAsync();

                foreach (var item in solicitacaoFuncionarioTickets)
                {
                    dbContext.Entry(item).State = EntityState.Added;
                }

                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Solicitacao> SelecionarFoto(int solicitacaoId)
        {
            try
            {
                IQueryable<Solicitacao> query = await Task.FromResult(GenerateQuery((x => x.Id == solicitacaoId), null));

                return query.AsNoTracking().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<byte[]> UploadFoto(byte[] file, int solicitacaoId, string extensao)
        {
            var solicitacao = await BuscarPorId(solicitacaoId);
            solicitacao.Imagem = file;
            solicitacao.Extensao = extensao;

            await UpdateAsync(solicitacao);
            return solicitacao.Imagem;
        }

    }
}
