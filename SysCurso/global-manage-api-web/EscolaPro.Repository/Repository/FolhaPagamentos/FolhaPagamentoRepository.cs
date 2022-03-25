using EscolaPro.Core.Model;
using EscolaPro.Core.Model.FolhaPagamentos;
using EscolaPro.Core.Model.MetasComissoes;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Interfaces.FolhaPagamentos;
using EscolaPro.Repository.Scripts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository.FolhaPagamentos
{
    public class FolhaPagamentoRepository : DomainRepository<FolhaPagamento>, IFolhaPagamentoRepository
    {
        public FolhaPagamentoRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task AdicionarHoraExtra(List<HoraExtra> horaExtras, int idFolhaPagamento)
        {
            foreach (var item in horaExtras.ToList())
            {
                item.Id = 0;
                item.FolhaPagamentoId = idFolhaPagamento;
                dbContext.Entry<HoraExtra>(item).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<FolhaPagamento>> BuscarPagamentosPendente(int funcionarioId)
        {
            try
            {
                IQueryable<FolhaPagamento> query = await Task.FromResult(GenerateQuery((x => x.FuncionarioId == funcionarioId && x.StatusPagamento == StatusPagamentoEnum.AReceber && !x.IsDelete), null));

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Obsolete]
        public async Task<IEnumerable<FolhaPagamento>> BuscarPorFiltro(string cpf, string nome, DateTime? inicioPeriodo, DateTime? fimPeriodo, int? unidadeId)
        {
            try
            {
                var sqlQuery = FolhaPagamentoScript.Filtrar(cpf, nome, inicioPeriodo, fimPeriodo, unidadeId);

                var folhaPagamnentoLista = dbSet.FromSql(sqlQuery).Select(x => new FolhaPagamento
                {
                    Id = x.Id
                }).ToList();

                return folhaPagamnentoLista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<FolhaPagamento> BuscarPorId(int idFolhaPagamento)
        {
            IQueryable<FolhaPagamento> query = await Task.FromResult(GenerateQuery((x => x.Id == idFolhaPagamento), null)
                .Include(x => x.HoraExtra)
                .Include(x => x.Funcionario).ThenInclude(y => y.DadosBancario));

            return query.FirstOrDefault();
        }

        public async Task DeletarHoraExtra(int idFolhaPagamento)
        {
            var horaExtras = dbContext.Set<HoraExtra>().Where(x => x.FolhaPagamentoId == idFolhaPagamento);

            foreach (var item in horaExtras)
            {
                dbContext.Entry<HoraExtra>(item).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> VerificarSeExistePagamentoPendente(int funcionarioId)
        {
            IQueryable<FolhaPagamento> query = await Task.FromResult(GenerateQuery((x => x.FuncionarioId == funcionarioId && x.StatusPagamento == StatusPagamentoEnum.AReceber && !x.IsDelete), null));

            return query.Count() > 0 ? true : false; 
        }
    }
}
