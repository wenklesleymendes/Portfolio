using EscolaPro.Service.Dto.FuncionarioVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface IFeriasFuncionarioService
    {
        Task<DtoFeriasFuncionario> ConcederFerias(DtoFeriasFuncionario dtoFeriasFuncionario);
        Task<bool> DeletarFerias(int idFerias);
        Task<IEnumerable<DtoFeriasFuncionario>> BuscarTodosPorFuncionario(int idFuncionario);
        Task<DtoFeriasDetalhamento> BuscarDetalhamentoFerias(int idFuncionario);
    }
}
