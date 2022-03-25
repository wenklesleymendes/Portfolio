using EscolaPro.Core.Model.ContasPagar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.ContasAPagar
{
    public interface IDespesaParcelaRepository : IDomainRepository<DespesaParcela>
    {
        Task<IEnumerable<DespesaParcela>> BuscarPorIdDespesa(int idDespesa);
        Task<IEnumerable<DespesaParcela>> Inserir(List<DespesaParcela> despesaParcelas, int idDespesa);
    }
}
