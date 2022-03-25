using EscolaPro.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces
{
    public interface IPlanoPagamentoRepository : IDomainRepository<PlanoPagamento>
    {
        Task<IEnumerable<PlanoPagamento>> BuscarPlanoPagamento(int formaPagamento, int? quantidadeParcela, int cursoId, int unidadeId);
        Task<IEnumerable<PlanoPagamento>> PorCursoUnidade(int cursoId, int unidadeId);
        Task<PlanoPagamento> BuscarPorId(int idPlanoPagamento);
    }
}
