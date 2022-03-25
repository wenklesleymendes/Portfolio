using EscolaPro.Core.Model.Atendimentos;
using EscolaPro.Repository.Interfaces.Atendimentos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository.Atendimentos
{
    public class AtendimentoAgendamentoRepository : DomainRepository<AtendimentoAgendamento>, IAtendimentoAgendamentoRepository
    {

        public AtendimentoAgendamentoRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<AtendimentoAgendamento>> BuscarTodos()
        {
            var agendamento = await GetAllAsync();
            return agendamento;
        }

        public async Task<AtendimentoAgendamento> Inserir(AtendimentoAgendamento atendimentoAgendamento)
        {
            try
            {
                if (atendimentoAgendamento.Id == 0)
                {
                    var atendimentoAgendamentoInserido = await AddAsync(atendimentoAgendamento);

                    return atendimentoAgendamento;
                }
                else
                {
                    await UpdateAsync(atendimentoAgendamento);
                    return atendimentoAgendamento;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AtendimentoAgendamento> BuscarPorIdAgendamento(int idAtendimentoAgendamento)
        {
            try
            {
                IQueryable<AtendimentoAgendamento> query = await Task.FromResult(GenerateQuery((x => x.Id == idAtendimentoAgendamento), null)
                    .AsNoTracking());

                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AtendimentoAgendamento> BuscaPorIdAtendimento(int idAtendimento)
        {
            try
            {
                IQueryable<AtendimentoAgendamento> query = await Task.FromResult(GenerateQuery((x => x.IdAtendimento == idAtendimento), null)
                    .AsNoTracking());

                var ultimoRegistro = query.AsEnumerable();

                return ultimoRegistro.LastOrDefault();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<AtendimentoAgendamento>> HistoricoTentativas(int idAtendimento)
        {
            try
            {
                IQueryable<AtendimentoAgendamento> query = await Task.FromResult(GenerateQuery((x => x.IdAtendimento == idAtendimento), null)
                    .AsNoTracking());

                var todosRegistros = query.AsEnumerable();

                return todosRegistros;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Excluir(int idAtendimentoAgendamento)
        {
            try
            {
                var agendamento = await GetByIdAsync(idAtendimentoAgendamento);
                agendamento.IsDelete = true;
                await UpdateAsync(agendamento);

                return agendamento.IsDelete;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
