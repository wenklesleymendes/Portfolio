using EscolaPro.Service.Dto.FornecedorVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface IFornecedorService
    {
        Task<DtoFornecedor> Inserir(DtoFornecedor fornecedor);
        Task<IEnumerable<DtoFornecedor>> BuscarTodos();
        Task<DtoFornecedor> BuscarPorId(int idFornecedor);
        Task<bool> Excluir(int idFornecedor);
        Task<bool> DesativarOuAtivar(int idFornecedor);
    }
}
