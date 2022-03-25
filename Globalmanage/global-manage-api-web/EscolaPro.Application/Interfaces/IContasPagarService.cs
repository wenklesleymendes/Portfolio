using EscolaPro.Core.Model.ContasPagar;
using EscolaPro.Service.Dto.ContasAPagarVO;
using EscolaPro.Service.Dto.FolhaPagamentoVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface IContasPagarService
    {
        Task<DtoDespesa> Inserir(DtoDespesa dtoDespesa);
        Task<DtoGridDespesaResponse> BuscarTodos(DtoFiltrarBusca dtoFiltrarBusca);
        Task<DtoDespesa> BuscarPorId(int idDespesa);
        Task<bool> Excluir(int idDespesa);
        Task<bool> LiquidarPagamento(DtoLiquidarDespesa dtoLiquidarDespesa);
        Task<DtoDetalheDespesa> BuscarDetalheDespesa(int idDespesa);
        Task<bool> CancelarPagamento(DtoDespesaCancelar despesaCancelar);
    }
}
