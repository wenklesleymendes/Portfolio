using EscolaPro.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces
{
    public interface IPlanoPagamentoCursoRepository : IDomainRepository<PlanoPagamentoCurso>
    {
        Task<IEnumerable<PlanoPagamentoCurso>> BuscarPorPlanoPagamento(int idPlanoPagamento);
        Task<IEnumerable<PlanoPagamentoCurso>> BuscarPorCursoId(int idCurso);
        Task Deletar(PlanoPagamentoCurso planoPagamentoCurso);
    }
}
