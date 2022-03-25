using EscolaPro.Service.Dto.EstoqueVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface IEstoqueHistoricoService
    {
        Task<DtoHistoricoEstoque> Inserir(DtoHistoricoEstoque historicoEstoque);
        Task<IEnumerable<DtoHistoricoEstoque>> BuscarPorIdEstoque(int idEstoque);

    }
}
