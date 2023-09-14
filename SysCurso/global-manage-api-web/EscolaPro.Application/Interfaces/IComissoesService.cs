using EscolaPro.Service.Dto.MetasComissoesVO;
using EscolaPro.Service.Dto.MetasComissoesVO.Dashboard;
using EscolaPro.Service.Dto.TicketVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface IComissoesService
    {
        Task<DtoComissao> Inserir(DtoComissao dtoComissao);
        Task<IEnumerable<DtoComissao>> BuscarTodos();
        Task<DtoComissao> BuscarPorId(int idComissoes);
        Task<DtoComissao> BuscarPorUnidade(int idUnidade);
        Task<IEnumerable<DtoComissao>> Filtrar(DtoFiltrar filtrar);
        Task<IEnumerable<DtoMinhasComissoes>> DashboardMinhasComissoes(DtoFiltrar filtrar);
        Task<bool> Excluir(int idComissoes);
    }
}
