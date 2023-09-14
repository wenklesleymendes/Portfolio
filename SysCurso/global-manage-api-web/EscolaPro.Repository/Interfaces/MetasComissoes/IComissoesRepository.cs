using EscolaPro.Core.Model.Enums;
using EscolaPro.Core.Model.MetasComissoes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.MetasComissoes
{
    public interface IComissoesRepository : IDomainRepository<Comissoes>
    {
        Task<Comissoes> BuscarPorId(int idComissoes);
        Task AtualizarParcelas(int idComissao, List<ComissaoParcela> comissaoParcelas);
        Task<IEnumerable<Comissoes>> Filtrar(int? UnidadeId, DateTime? dataInicio, DateTime? dataFim, TipoPagamentoEnum? tipoPagamentoEnum);
    }
}
