using EscolaPro.Core.Model.Provas;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.AgendaProvas
{
    public interface IColegioAutorizadoRepository : IDomainRepository<ColegioAutorizado>
    {
        Task<ColegioAutorizado> BuscarPorId(int colegioAutorizadoId);
    }
}
