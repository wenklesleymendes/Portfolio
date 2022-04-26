using EscolaPro.Service.Dto;
using EscolaPro.Service.Dto.FolhaPagamentoVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface IFolhaPagamentoService
    {
        Task<DtoFolhaPagamento> Inserir(DtoFolhaPagamento folhaPagamento);
        Task<IEnumerable<DtoFolhaPagamentoGrid>> BuscarTodos(DtoFiltrarBusca filtrarBusca);
        Task<DtoFolhaPagamento> BuscarPorId(int idFolhaPagamento);
        Task<bool> Excluir(int idFolhaPagamento);
        Task<DtoHoleriteFolhaPagamento> ImprimirReciboPagamento(int idFolhaPagamento);
        Task<DtoAnexo> DownloadComprovanteBancario(int idFolhaPagamento);
    }
}
