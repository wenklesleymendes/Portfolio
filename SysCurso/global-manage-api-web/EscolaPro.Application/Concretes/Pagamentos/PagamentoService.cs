using EscolaPro.Service.Dto.PagamentosVO;
using EscolaPro.Service.Interfaces.Pagamentos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes.Pagamentos
{
    public class PagamentoService : IPagamentoService
    {
        public async Task<DtoPagamento> BuscarPorId(int pagamentoId)
        {
            throw new NotImplementedException();
        }

        public Task EfetuarPagamento(DtoPagamento dtoPagamento)
        {
            throw new NotImplementedException();
        }

        public async Task<DtoPagamento> GerarBoletoEnviarPorEmail(int pagamentoId)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
