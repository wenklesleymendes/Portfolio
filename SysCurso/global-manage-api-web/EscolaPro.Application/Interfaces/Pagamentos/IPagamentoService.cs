using EscolaPro.Service.Dto.PagamentosVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces.Pagamentos
{
    public interface IPagamentoService
    {
        Task EfetuarPagamento(DtoPagamento dtoPagamento);
        Task<DtoPagamento> GerarBoletoEnviarPorEmail(int pagamentoId);
        Task<DtoPagamento> BuscarPorId(int pagamentoId);
    }
}
