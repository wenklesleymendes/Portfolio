using EscolaPro.Core.Model.ContasPagar;
using EscolaPro.Core.Model.Enums;
using EscolaPro.Core.Model.Fornecedores;
using EscolaPro.Core.Model.MetasComissoes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.ContasAPagar
{
    public interface IContasPagarRepository : IDomainRepository<Despesa>
    {
        Task<Despesa> BuscarPorId(int idDespesa);
        Task DeletarParcelaDespesa(int idDespesa);
        Task AdicionarParcelaDespesa(List<DespesaParcela> despesaParcelas, int idDespesa);
        Task<IEnumerable<Despesa>> Filtrar(string cpf, string categoria, DateTime? inicioPeriodo, DateTime? fimPeriodo, int? unidadeId, TipoPagamentoEnum? tipoPagamento, TipoPessoaEnum? tipoPessoa, StatusPagamentoEnum? statusPagamento);
    }
}
