using EscolaPro.Service.Dto.FornecedorVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface ICategoriaService
    {
        Task<DtoCategoria> Inserir(DtoCategoria categoria);
        Task<IEnumerable<DtoCategoria>> BuscarTodos();
        Task<DtoCategoria> BuscarPorId(int idCategoria);
        Task<bool> Excluir(int idCategoria);
    }
}
