using EscolaPro.Core.Model.EstoqueProdutos;
using EscolaPro.Service.Dto.EstoqueVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface IEstoqueService
    {
        Task<DtoProduto> Inserir(DtoProduto produto);
        Task<DtoItemProduto> AdicionarItem(DtoItemProduto dtoItemProduto);
        Task<IEnumerable<DtoGridEstoque>> BuscarTodos();
        Task<IEnumerable<DtoHistoricoEstoque>> BuscarHistoricoPorEstoque(int idEstoque);
        Task<DtoProduto> BuscarPorId(int idProduto);
        Task<bool> Excluir(int idProduto);
        Task<bool> ExcluirItem(int idItemProduto);
        Task<bool> RetiradaApostila(int idProduto);
        Task<IEnumerable<DtoItemProduto>> BuscarItemProdutoPorEstoque(int idProduto);


    }
}
