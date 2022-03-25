using EscolaPro.Core.Model.Fornecedores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces.Fornecedores
{
    public interface IFornecedorRepository : IDomainRepository<Fornecedor>
    {
        Task<Fornecedor> BuscarPorId(int idFuncionario);
        Task<Fornecedor> Inserir(Fornecedor fornecedor);
    }
}
