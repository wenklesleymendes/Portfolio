using EscolaPro.API.Dto;
using EscolaPro.Core.Model;
using EscolaPro.Core.Model.Provas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface IHistoricoProvasService
    {
        Task<List<HistoricoProvas>> ListaColegioAutorizadoExcel(FiltroHistoricoProvas filtro);
        Task<List<HistoricoProvas>> ListaGeralDeInscritosParaProvaExcel(FiltroHistoricoProvas filtro);
        Task<ChamadaOnibus> ListaDeChamadaOnibusExcel(FiltroHistoricoProvas filtro);
        Task<List<UnidadeTransporteProva>> NumeroOnibus(int idUnidade, string dataInicioMatricula, string dataFimMatricula);
    }
}
