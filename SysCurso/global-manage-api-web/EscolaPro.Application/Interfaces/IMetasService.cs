using EscolaPro.Service.Dto.MetasComissoesVO;
using EscolaPro.Service.Dto.MetasComissoesVO.Dashboard;
using EscolaPro.Service.Dto.TicketVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface IMetasService
    {
        Task<DtoMetas> Inserir(DtoMetas dtoMetas);
        Task<IEnumerable<DtoMetas>> BuscarTodos();
        Task<List<string>> ListaNomeMetas();
        Task<DtoMetas> BuscarPorId(int idMeta);
        Task<IEnumerable<DtoMetas>> Filtrar(DtoFiltrarMeta filtrar);
        Task<DtoMetas> BuscarPorUnidade(int idUnidade);
        Task<bool> Excluir(int idMeta);
        Task<DtoDashboardMetasComissoes> ConsultarDashboard(DtoFiltrarMeta filtrar);
    }
}
