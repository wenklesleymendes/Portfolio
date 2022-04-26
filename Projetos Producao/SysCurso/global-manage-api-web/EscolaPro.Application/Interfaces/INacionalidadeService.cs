using EscolaPro.Service.Dto.AlunosVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface INacionalidadeService
    {
        Task<DtoNacionalidade> Inserir(DtoNacionalidade dtoNacionalidade);
        Task<bool> Excluir(int naturalidadeId);
        Task<IEnumerable<DtoNacionalidade>> BuscarTodos();
    }
}
