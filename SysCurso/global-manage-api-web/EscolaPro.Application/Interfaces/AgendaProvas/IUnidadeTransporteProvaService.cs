using EscolaPro.Service.Dto.AgendaProvaVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces.AgendaProvas
{
    public interface IUnidadeTransporteProvaService
    {
        DtoUnidadeTransporteProva BuscarProximoOnibus(int agendaProvaId, int unidadeId);
        Task<DtoUnidadeTransporteProva> BuscarOnibus(int UnidadeTransporteProvaId);
    }
}
