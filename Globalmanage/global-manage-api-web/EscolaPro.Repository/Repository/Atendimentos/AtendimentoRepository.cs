using EscolaPro.Core.Model;
using EscolaPro.Core.Model.Atendimentos;
using EscolaPro.Core.Model.ReguaContato;
using EscolaPro.Repository.Interfaces.Atendimentos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository
{
    public class AtendimentoRepository : DomainRepository<Atendimento>, IAtendimentoRepository
    {
        public AtendimentoRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public async Task<Atendimento> BuscarPorId(int atendimentoId)
        {
            try
            {
                IQueryable<Atendimento> query = await Task.FromResult(GenerateQuery((x => x.Id == atendimentoId), null)
                    .AsNoTracking());

                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateListaStatus(IEnumerable<Atendimento> atendimentos)
        {
            await this.UpdateRangeAsync(atendimentos);
        }

        public async Task UpdateStatus(int id, StatusAtendimento statusFila)
        {
            IQueryable<Atendimento> query = await Task.FromResult(GenerateQuery(x => x.Id == id));

            var atendimento = query.FirstOrDefault();
            atendimento.StatusAlteracao = DateTime.Now;
            atendimento.Status = statusFila.GetHashCode();

            await this.UpdateAsync(atendimento);
        }

        public async Task AtualizaStatusAtendimento(Atendimento atendimento) 
            => await this.UpdateAsync(atendimento);

        public async Task<Atendimento> BuscaPorCelular(string celularCliente)
        {
            IQueryable<Atendimento> atendimento = await Task.FromResult(GenerateQuery((x => x.Celular == celularCliente), null));

            return atendimento.FirstOrDefault();
        }

        public async Task<IEnumerable<Atendimento>> FiltrarAtendimento(FiltroAtendimentos filtro)
        {
            DateTime? dataInicio = null;
            DateTime? dataFim = null;
            DateTime? dataAtendimento = null;

            if (!string.IsNullOrEmpty(filtro.DatadoAtendimento))
            {
                dataAtendimento = DateTime.Parse(filtro.DatadoAtendimento);
            }

            if (!string.IsNullOrEmpty(filtro.PesquisarDataInicial))
            {
                dataInicio = DateTime.Parse(filtro.PesquisarDataInicial);
            }

            if (!string.IsNullOrEmpty(filtro.PesquisarDataFinal))
            {
                dataFim = DateTime.Parse(filtro.PesquisarDataFinal);
            }

            var query = dbSet.Where(a => (string.IsNullOrEmpty(filtro.Celular) || a.Celular == filtro.Celular) &&
                                         (!filtro.CanaldeAtendimento.HasValue || a.CanaldeAtendimento == filtro.CanaldeAtendimento) &&
                                         (string.IsNullOrEmpty(filtro.NomedoCliente) || a.NomedoCliente.Contains(filtro.NomedoCliente)) &&
                                         (string.IsNullOrEmpty(filtro.Celular) || a.Celular == filtro.Celular) &&
                                         (!filtro.IdAtendente.HasValue || a.UsuarioCadastro == filtro.IdAtendente) &&
                                         (!filtro.StatusdoAtendimento.HasValue || a.Status != 0) &&
                                         (string.IsNullOrEmpty(filtro.TelefoneFixo) || a.TelefoneFixo == filtro.TelefoneFixo) &&
                                         (!dataAtendimento.HasValue || a.DataeHoradoAtendimento.Date == dataAtendimento.Value.Date) &&
                                         (!dataInicio.HasValue || a.DataeHoradoAtendimento.Date >= dataInicio.Value.Date) &&
                                         (!dataFim.HasValue || a.DataeHoradoAtendimento.Date <= dataFim.Value.Date) &&
                                         (!filtro.IdUnidade.HasValue || a.UnidadeCadastro == filtro.IdUnidade));

            return query;
        }

        public async Task<int> ObtenhaQtdAtendimentoPorStatus(int idUnidade, int idStatus)
        {
            var query =  dbSet.Where(a => a.UnidadeCadastro == idUnidade && a.Status == idStatus).Count();
            return query;
        }

        public async Task<IEnumerable<Atendimento>> BuscaAtendimentosParaFila(int idUnidade)
        {
            IQueryable<Atendimento> atendimentos = await Task.FromResult(GenerateQuery((u => u.UnidadeCadastro == idUnidade &&
                                            u.Status != StatusAtendimento.Desativado.GetHashCode() &&
                                            u.Status != StatusAtendimento.Matriculado.GetHashCode()), null));

            return atendimentos;
        }

        public async Task<IEnumerable<Atendimento>> BuscaAtendimentosExecucaoProximoDia(int idUnidade)
        {
            IQueryable<Atendimento> atendimentos = await Task.FromResult(GenerateQuery((u => u.UnidadeCadastro == idUnidade &&
                                            u.Status == StatusAtendimento.ExecucaoProximoDia.GetHashCode()), null));

            return atendimentos;
        }
    }
}
