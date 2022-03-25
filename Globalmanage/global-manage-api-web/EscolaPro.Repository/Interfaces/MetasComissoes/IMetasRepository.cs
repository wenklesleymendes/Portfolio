using EscolaPro.Core.Model.MetasComissoes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.MetasComissoes
{
    public interface IMetasRepository : IDomainRepository<Meta>
    {
        Task<Meta> BuscarPorId(int idMeta);
        Task<IEnumerable<Meta>> Filtrar(int? UnidadeId, string nomeMeta);
        Task<bool> MetaUnidadesDeletar(int idMeta);
        Task<bool> MetaPeriodosDeletar(int idMeta);
        Task AdicionarDetalhamentoMeta(int idMeta, List<DetalhamentoMeta> metaPeriodos);
    }
}
