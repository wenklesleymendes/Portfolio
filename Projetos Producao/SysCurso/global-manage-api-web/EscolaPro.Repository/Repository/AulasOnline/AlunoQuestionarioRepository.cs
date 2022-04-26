using EscolaPro.Core.Model;
using EscolaPro.Core.Model.AlunoQuestionarioProva;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Interfaces.AulasOnline;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository.AulasOnline
{
    public class AlunoQuestionarioRepository : DomainRepository<AlunoQuestionario>, IAlunoQuestionarioRepository
    {
        public AlunoQuestionarioRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<AlunoQuestionario> BuscarPorId(int alunoQuestionarioId)
        {
            try
            {
                IQueryable<AlunoQuestionario> query = await Task.FromResult(GenerateQuery((x => x.Id == alunoQuestionarioId && !x.IsDelete), null)
                                                                .Include(x => x.AlunoQuestionarioReposta));

                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AlunoQuestionario> BuscarPorPerguntaId(int perguntaId)
        {
            try
            {
                IQueryable<AlunoQuestionario> query = await Task.FromResult(GenerateQuery((x => x.PerguntaId == perguntaId && !x.IsDelete), null)
                                                                .Include(x => x.AlunoQuestionarioReposta));

                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Excluir(int alunoQuestionarioId)
        {
            try
            {
                var questionario = await GetByIdAsync(alunoQuestionarioId);
                questionario.IsDelete = true;
                await UpdateAsync(questionario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
