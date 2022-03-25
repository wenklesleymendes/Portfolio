using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces.Pagamentos.TransacaoCartao
{
    interface IAcquirer
    {
        public Task<string> Capture(object paymentData);
    }
}
