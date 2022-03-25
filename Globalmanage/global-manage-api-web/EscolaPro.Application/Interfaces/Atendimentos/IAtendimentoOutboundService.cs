using EscolaPro.Core.Model.Atendimentos;
using EscolaPro.Service.Dto.AtendimentoVO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface IAtendimentoOutboundService
    {
        Task<DtoAtendimentoOutbound> Inserir(DtoAtendimentoOutbound atendimentoOutbound);
        Task<DtoAtendimento> AtualizaStatus(int id, int status);
        Task<DtoAtendimento> ObtenhaOutbound(int idUnidade);
        Task<List<DtoAtendimento>> BuscaAtendimentosPorStatus(int status, int idUnidade);
        Task<DtoAtendimentoOutbound> BuscarPorId(int idAtendimentoOutbound);
        Task<IEnumerable<AtendimentoOutbound>> HistoricoTentativas(int idAtendimento);
        Task<IEnumerable<DtoAtendimentoAgendamento>> HistoricoAgendamento(int idAtendimento);
        Task<int> ContarContatos(int idAtendimento);
    }
}
