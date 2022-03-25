using EscolaPro.Core.Model.ContasPagar;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Interfaces.ContasAPagar;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EscolaPro.Repository.Scripts;
using EscolaPro.Core.Model.Enums;
using EscolaPro.Core.Model.MetasComissoes;
using EscolaPro.Core.Model.Fornecedores;
using EscolaPro.Core.Model;

namespace EscolaPro.Repository.Repository.ContasAPagar
{
    public class ContasPagarRepository : DomainRepository<Despesa>, IContasPagarRepository
    {
        public ContasPagarRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task AdicionarParcelaDespesa(List<DespesaParcela> despesaParcelas, int idDespesa)
        {
            foreach (var item in despesaParcelas.ToList())
            {
                item.DespesaId = idDespesa;
                dbContext.Entry<DespesaParcela>(item).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task<Despesa> BuscarPorId(int idDespesa)
        {
            try
            {
                IQueryable<Despesa> query = await Task.FromResult(GenerateQuery((x => x.Id == idDespesa), null)
                    .Include(x => x.Unidade)
                    .Include(x => x.Categoria)
                    .Include(x => x.CentroCusto)
                    .Include(x => x.DespesaParcela)
                    .Include(x => x.Fornecedor)
                    .Include(x => x.DespesaDARF)
                    .Include(x => x.DespesaGPS));

                return query.AsNoTracking().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeletarParcelaDespesa(int idDespesa)
        {
            var horaExtras = dbContext.Set<DespesaParcela>().Where(x => x.DespesaId == idDespesa);

            foreach (var item in horaExtras)
            {
                dbContext.Entry<DespesaParcela>(item).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            }

            await dbContext.SaveChangesAsync();
        }

        [Obsolete]
        public async Task<IEnumerable<Despesa>> Filtrar(string cpf, string categoria, DateTime? inicioPeriodo, DateTime? fimPeriodo, int? unidadeId, TipoPagamentoEnum? tipoPagamento, TipoPessoaEnum? tipoPessoa, StatusPagamentoEnum? statusPagamento)
        {
            try
            {
                var sqlQuery = ContasAPagarScript.Filtrar(cpf, categoria, inicioPeriodo, fimPeriodo, unidadeId, tipoPagamento, tipoPessoa, statusPagamento);

                var contasAPagarLista = dbSet.FromSql(sqlQuery).Select(x => new Despesa
                {
                    Id = x.Id,
                    DespesaParcela = x.DespesaParcela
                }).ToList();

                return contasAPagarLista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
