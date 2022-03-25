using EscolaPro.Service.Dto.AulaOnlineVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface IRespostaService
    {
        Task<DtoResposta> Inserir(DtoResposta dtoResposta);
        Task<IEnumerable<DtoResposta>> BuscarPorPergunta(int perguntaId);
        Task<DtoResposta> BuscarPorId(int respostaId);
        Task<bool> Excluir(int respostaId);
    }
}
