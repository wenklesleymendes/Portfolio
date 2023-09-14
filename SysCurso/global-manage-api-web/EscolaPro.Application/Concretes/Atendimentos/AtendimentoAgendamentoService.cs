using AutoMapper;
using EscolaPro.Core.Model.Atendimentos;
using EscolaPro.Repository.Interfaces.Atendimentos;
using EscolaPro.Service.Dto.AtendimentoVO;
using EscolaPro.Service.Interfaces.Atendimentos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes.Atendimentos
{
    public class AtendimentoAgendamentoService : IAtendimentoAgendamentoService
    {
        private readonly IAtendimentoAgendamentoRepository _atendimentoAgendamentoRepository;
        private readonly IMapper _mapper;
        private readonly IAtendimentoRepository _atendimentoRepository;

        public AtendimentoAgendamentoService(IAtendimentoAgendamentoRepository atendimentoAgendamentoRepository,
            IAtendimentoRepository atendimentoRepository, IMapper mapper)
        {
            _atendimentoAgendamentoRepository = atendimentoAgendamentoRepository;
            _atendimentoRepository = atendimentoRepository;
            _mapper = mapper;
        }

        public async Task<DtoAtendimentoAgendamento> Inserir(DtoAtendimentoAgendamento dtoAtendimentoAgendamento)
        {
            var agendamento = _mapper.Map<AtendimentoAgendamento>(dtoAtendimentoAgendamento);
            var retorno = await _atendimentoAgendamentoRepository.Inserir(agendamento);

            return _mapper.Map<DtoAtendimentoAgendamento>(retorno);
        }

        public async Task<IEnumerable<DtoAtendimentoAgendamento>> BuscarTodos()
        {
            var agendamentos = await _atendimentoAgendamentoRepository.BuscarTodos();
            var atendimentos = await _atendimentoRepository.GetAllAsync();

            var atendimentosAgendamentos = from agendamento in agendamentos
                                           join atendimento in atendimentos
                                           on agendamento.IdAtendimento equals atendimento.Id
                                           select new DtoAtendimentoAgendamento
                                           {
                                               Id = agendamento.Id,
                                               IdAtendimento = atendimento.Id,
                                               NomedoCliente = atendimento.NomedoCliente,
                                               HoraAgendamento = agendamento.HoraAgendamento,
                                               DataAgendamento = agendamento.DataAgendamento,
                                               DataeHoradoUltimoContato = agendamento.DataeHoradoUltimoContato.ToString(),
                                               TipoAgendamento = agendamento.TipoAgendamento
                                           };
            
            return atendimentosAgendamentos;
        }

        public async Task<IEnumerable<DtoAtendimentoAgendamento>> BuscaPorUnidade(int idUnidade)
        {
            var atendimentos = await _atendimentoRepository.GetAllAsync();
            var agendamentos = await _atendimentoAgendamentoRepository.BuscarTodos();

            var agendamentosPorUnidade = from agendamento in agendamentos
                                         join atendimento in atendimentos
                                         on agendamento.IdAtendimento equals atendimento.Id
                                         where atendimento.UnidadeCadastro == idUnidade
                                         select new DtoAtendimentoAgendamento
                                         {
                                             Id = agendamento.Id,
                                             IdAtendimento = atendimento.Id,
                                             NomedoCliente = atendimento.NomedoCliente,
                                             HoraAgendamento = agendamento.HoraAgendamento,
                                             DataAgendamento = agendamento.DataAgendamento,
                                             DataeHoradoUltimoContato = agendamento.DataeHoradoUltimoContato.ToString().Replace(" ", " "),
                                             TipoAgendamento = agendamento.TipoAgendamento,
                                             Celular = atendimento.Celular,
                                           };

            var agendamentoFiltrado = agendamentosPorUnidade.Where(
                a => a.DataAgendamento == DateTime.Now.Date.ToString().Split(" ")[0])
                .OrderBy(a => a.HoraAgendamento);

            return agendamentoFiltrado;
        }

        public async Task<DtoAtendimentoAgendamento> BuscarPorId(int idAtendimentoAgendamento)
        {
            var agendamento = await _atendimentoAgendamentoRepository.BuscarPorIdAtendimento(idAtendimentoAgendamento);
            var retorno = _mapper.Map<DtoAtendimentoAgendamento>(agendamento);

            return retorno;
        }

        public async Task<bool> Deletar(int idAgendamento)
        {
            try
            {
                return await _atendimentoAgendamentoRepository.Excluir(idAgendamento);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
