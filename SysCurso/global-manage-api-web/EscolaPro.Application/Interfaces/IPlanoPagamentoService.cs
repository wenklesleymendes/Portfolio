using EscolaPro.Service.Dto.PlanoPagamentoVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface IPlanoPagamentoService
    {
        Task<DtoPlanoPagamento> Inserir(DtoPlanoPagamento planoPagamento);
        Task<DtoPlanoPagamento> BuscarPorId(int idPlanoPagamento);
        Task<DtoPlanoPagamento> BuscarPorIdAlterar(int idPlanoPagamento);
        Task<IEnumerable<DtoPlanoPagamento>> BuscarPorIdCurso(int idCurso);
        Task<IEnumerable<DtoPlanoPagamento>> BuscarTodos();
        public Task<DtoPlanoPagamento> AtivarOuDesativar(int idPlanoPagamento);
        Task<bool> Deletar(int id);
        Task<IEnumerable<DtoPlanoPagamento>> BuscarPlanoPagamento(int formaPagamento, int? quantidadeParcela, int cursoId, int unidadeId);
        Task<IEnumerable<DtoPlanoPagamento>> BuscarPorCursoUnidade(int cursoId, int unidadeId);
    }
}
