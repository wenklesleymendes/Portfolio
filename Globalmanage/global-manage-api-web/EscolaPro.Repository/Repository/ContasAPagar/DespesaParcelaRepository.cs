using EscolaPro.Core.Model.ContasPagar;
using EscolaPro.Core.Model.MetasComissoes;
using EscolaPro.Repository.Interfaces.ContasAPagar;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository.ContasAPagar
{
    public class DespesaParcelaRepository : DomainRepository<DespesaParcela>, IDespesaParcelaRepository
    {
        public DespesaParcelaRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<DespesaParcela>> BuscarPorIdDespesa(int idDespesa)
        {
            try
            {
                IQueryable<DespesaParcela> query = await Task.FromResult(GenerateQuery((x => x.DespesaId == idDespesa), null));

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<DespesaParcela>> Inserir(List<DespesaParcela> despesaParcelas, int idDespesa)
        {
            try
            {
                var despesasParcelasOld = await BuscarPorIdDespesa(idDespesa);

                foreach (var item in despesasParcelasOld.Where(x => x.StatusPagamento == StatusPagamentoEnum.AReceber))
                {
                    item.DespesaId = idDespesa;

                    await RemoveAsync(item);
                }

                foreach (var item in despesaParcelas.Where(x => x.StatusPagamento == StatusPagamentoEnum.AReceber))
                {
                    item.Id = 0;
                    item.DespesaId = idDespesa;
                    item.StatusPagamento = StatusPagamentoEnum.AReceber;
                    await AddAsync(item);
                }

                return await BuscarPorIdDespesa(idDespesa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
