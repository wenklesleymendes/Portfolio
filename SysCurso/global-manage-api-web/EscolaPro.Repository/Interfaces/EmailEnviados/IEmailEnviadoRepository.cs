using EscolaPro.Core.Model.Pagamentos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.EmailEnviados
{
    public interface IEmailEnviadoRepository : IDomainRepository<EmailEnviado>
    {
        Task<IEnumerable<EmailEnviado>> BuscarPorPagamento(int pagamentoId);
    }
}
