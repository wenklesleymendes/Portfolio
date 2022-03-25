using EscolaPro.Core.Model.Pagamentos;
using EscolaPro.Service.Dto.TicketVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface IOcorrenciaService
    {
        Task<DtoOcorencia> Inserir(DtoOcorencia ocorrencia);
        Task<IEnumerable<DtoOcorencia>> BuscarPorMatriculaId(int matriculaId);
        Task<DtoOcorencia> BuscarPorId(int idOcorrencia);
        Task<DtoTicketTimeline> BuscarTimeline(int idOcorrencia);
    }
}
