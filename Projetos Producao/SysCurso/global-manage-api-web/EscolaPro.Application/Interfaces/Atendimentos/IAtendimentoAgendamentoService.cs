using EscolaPro.Service.Dto.AtendimentoVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces.Atendimentos
{
    public interface IAtendimentoAgendamentoService
    {
        Task<DtoAtendimentoAgendamento> Inserir(DtoAtendimentoAgendamento dtoAtendimentoAgendamento);
        Task<IEnumerable<DtoAtendimentoAgendamento>> BuscarTodos();
        Task<DtoAtendimentoAgendamento> BuscarPorId(int idAtendimentoAgendamento);
        Task<bool> Deletar(int idAgendamento);
        Task<IEnumerable<DtoAtendimentoAgendamento>> BuscaPorUnidade(int idUnidade);

    }
}
