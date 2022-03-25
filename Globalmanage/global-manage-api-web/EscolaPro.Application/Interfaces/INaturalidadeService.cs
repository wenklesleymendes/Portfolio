using EscolaPro.Service.Dto.AlunosVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface INaturalidadeService
    {
        Task<DtoNaturalidade> Inserir(DtoNaturalidade dtoNaturalidade);
        Task<bool> Excluir(int naturalidadeId);
        Task<IEnumerable<DtoNaturalidade>> BuscarTodos();
    }
}
