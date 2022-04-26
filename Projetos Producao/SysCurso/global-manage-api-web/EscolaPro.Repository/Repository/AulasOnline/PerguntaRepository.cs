using EscolaPro.Core.Model;
using EscolaPro.Core.Model.AulasOnline;
using EscolaPro.Repository.Interfaces.AulasOnline;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository.AulasOnline
{
    public class PerguntaRepository : DomainRepository<Pergunta>, IPerguntaRepository
    {
        public PerguntaRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<Pergunta> Atualizar(Pergunta pergunta, List<Resposta> respostasParam)
        {
            try
            {
                // Atualizar / Adicionar novas respostas
                foreach (var item in respostasParam)
                {
                    item.PerguntaId = pergunta.Id;

                    if (item.Id == 0)
                    {
                        dbContext.Entry<Resposta>(item).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                    else
                    {
                        dbContext.Entry<Resposta>(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                }

                // Atualizar Pergunta
                dbContext.Entry(pergunta).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await dbContext.SaveChangesAsync();

                // Excluir respostas antigas
                var respostas = dbContext.Set<Resposta>().Where(x => x.PerguntaId == pergunta.Id).ToList();

                List<Resposta> respostasDelete = new List<Resposta>();

                foreach (var item in respostas)
                {
                    var itemAdd = respostasParam.Where(x => x.Id == item.Id).FirstOrDefault();

                    if(itemAdd == null)
                    {
                        var anexos = dbContext.Set<Anexo>().Where(x => x.RespostaId == item.Id).ToList();

                        foreach (var anexo in anexos)
                        {
                            dbContext.Entry<Anexo>(anexo).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                        }

                        dbContext.Entry<Resposta>(item).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                    }
                }

                await dbContext.SaveChangesAsync();

                return await BuscarPorId(pergunta.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Pergunta> BuscarPorId(int perguntaId)
        {
            try
            {
                IQueryable<Pergunta> query = await Task.FromResult(GenerateQuery((x => x.Id == perguntaId), null)
                                                        .Include(x => x.Resposta));

                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Pergunta>> BuscarPorVideoAula(int videoAulaId)
        {
            try
            {
                IQueryable<Pergunta> query = await Task.FromResult(GenerateQuery((x => x.VideoAulaId == videoAulaId && !x.IsDelete), null)
                                    .Include(x => x.Resposta));

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
