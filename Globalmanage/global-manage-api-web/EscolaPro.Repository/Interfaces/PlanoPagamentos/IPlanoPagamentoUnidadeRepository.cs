using EscolaPro.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces
{
    public interface IPlanoPagamentoUnidadeRepository : IDomainRepository<PlanoPagamentoUnidade>
    {
        Task<IEnumerable<PlanoPagamentoUnidade>> BuscarPorPlanoPagamento(int idPlanoPagamento);
        Task Deletar(PlanoPagamentoUnidade planoPagamentoCurso);
    }
}
