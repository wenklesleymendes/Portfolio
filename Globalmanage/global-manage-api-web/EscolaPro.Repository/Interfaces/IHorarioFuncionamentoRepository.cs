using EscolaPro.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces
{
    public interface IHorarioFuncionamentoRepository : IDomainRepository<HorarioFuncionamento>
    {
        Task<List<HorarioFuncionamento>> PorIdUnidade(int idUnidade);
    }
}
