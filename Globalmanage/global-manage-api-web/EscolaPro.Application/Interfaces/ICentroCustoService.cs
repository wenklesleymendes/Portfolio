using EscolaPro.Service.Dto.UnidadeVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface ICentroCustoService
    {
        Task<DtoCentroCusto> Inserir(DtoCentroCusto dtoCentroCusto);
        Task<bool> Deletar(int idCentroCusto);
        Task<IEnumerable<DtoCentroCusto>> BuscarPorUnidade(int idUnidade);
    }
}
