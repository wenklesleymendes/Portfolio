using AutoMapper;
using EscolaPro.Core.Model;
using EscolaPro.Core.Model.Atendimentos;
using EscolaPro.Core.Model.Enums;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Interfaces.Atendimentos;
using EscolaPro.Service.Dto.AtendimentoVO;
using EscolaPro.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes.Atendimentos
{
    public class AtendimentoOutboundService : IAtendimentoOutboundService
    {
        private readonly IAtendimentoOutboundRepository _atendimentoOutboundRepository;
        private readonly IAtendimentoRepository _atendimentoRepository;
        private readonly IContatoRepository _contatoRepository;
        private readonly IAtendimentoAgendamentoRepository _atendimentoAgendamentoRepository;
        private readonly IMapper _mapper;

        private int _scoreInicial = 0;
        private int _scoreAplicado = 0;

        public IAtendimentoOutboundRepository AtendimentoOutboundRepository => _atendimentoOutboundRepository;

        public IAtendimentoRepository AtendimentoRepository => _atendimentoRepository;

        public IContatoRepository ContatoRepository => _contatoRepository;

        public IMapper Mapper => _mapper;

        public IAtendimentoAgendamentoRepository AtendimentoAgendamentoRepository => _atendimentoAgendamentoRepository;

        public AtendimentoOutboundService(
            IAtendimentoOutboundRepository atendimentoOutboundRepository,
            IAtendimentoRepository atendimentoRepository,
            IContatoRepository ContatoRepository,
            IAtendimentoAgendamentoRepository atendimentoAgendamentoRepository,
            IMapper mapper)
        {
            _atendimentoOutboundRepository = atendimentoOutboundRepository;
            _atendimentoRepository = atendimentoRepository;
            _contatoRepository = ContatoRepository;
            _atendimentoAgendamentoRepository = atendimentoAgendamentoRepository;
            _mapper = mapper;
        }

        public async Task<DtoAtendimentoOutbound> Inserir(DtoAtendimentoOutbound outbound)
        {
            try
            {
                CultureInfo cultures = new CultureInfo("pt-BR");

                DateTime? dataAgendamento = null;
                if (outbound.DiadoAgendamento != null)
                {
                    dataAgendamento = Convert.ToDateTime(outbound.DiadoAgendamento.Split("T")[0] + " " + outbound.HoradoAgendamento);
                }                

                AplicaScore(outbound);

                var _atendimentoOutbound = new AtendimentoOutbound()
                {
                    AtendimentoId = outbound.AtendimentoId,
                    DataHoraContato = DateTime.Now,
                    MatriculaAgendada = outbound.MatriculaAgendada,
                    UsuarioLogado = outbound.UsuarioLogado,
                    UsuarioCadastro = outbound.UsuarioCadastrado,
                    Observacao = outbound.Observacoes,
                    ScoreAplicado = _scoreAplicado.GetHashCode(),
                    ScoreInicial = _scoreInicial.GetHashCode(),
                    NumeroTentativa = outbound.NumeroTentativa,
                    MotivodoNaoAgendamento = outbound.MotivodoNaoAgendamento,
                    DataeHoradoAgendamento = dataAgendamento,
                    AgendamentodaMatricula = outbound.AgendamentodaMatricula,
                };

                await AtendimentoOutboundRepository.AddAsync(_atendimentoOutbound);

                if (outbound.MatriculaAgendada != 3)
                {
                    var agendamento = new AtendimentoAgendamento
                    {
                        IdAtendimento = outbound.AtendimentoId,
                        DataeHoradoUltimoContato = _atendimentoOutbound.DataHoraContato,
                        DataAgendamento = dataAgendamento.ToString().Split(" ")[0],
                        HoraAgendamento = outbound.HoradoAgendamento,
                        TipoAgendamento = (TipoAgendamentoEnum)outbound.AgendamentodaMatricula
                    };

                    await _atendimentoAgendamentoRepository.Inserir(agendamento);

                    var atendimento = await _atendimentoRepository.BuscarPorId(outbound.AtendimentoId);
                    atendimento.DataeHoradoAgendamento = dataAgendamento;
                    atendimento.ExisteAgendamento = true;
                    atendimento.Status = StatusRegistros.ExecucaoProximoDia.GetHashCode();

                    await _atendimentoRepository.UpdateAsync(atendimento);
                }

                return Mapper.Map<DtoAtendimentoOutbound>(_atendimentoOutbound);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AplicaScore(DtoAtendimentoOutbound outbound)
        {
            var ultimoAgendamento = HistoricoAgendamento(outbound.AtendimentoId)
                                    .Result.Where(a => a.DataAgendamento != null)
                                    .LastOrDefault();

            if (ultimoAgendamento != null)
            {
                DateTime? dataAgendamento = null;
                    dataAgendamento = Convert.ToDateTime(ultimoAgendamento.DataAgendamento + " " + ultimoAgendamento.HoraAgendamento);

                var totalDiasAposAgendamento =  DateTime.Now.Date - dataAgendamento.Value.Date;

                switch (totalDiasAposAgendamento.Days)
                {
                    case 1:
                        outbound.ScoreInicial = outbound.ScoreAplicado;
                        outbound.ScoreAplicado = ScoreEnum.LeadAgendamentoSemMatriculaPrimeiorDia.GetHashCode();
                        break;

                    case 2:
                        outbound.ScoreInicial = ScoreEnum.LeadAgendamentoSemMatriculaPrimeiorDia.GetHashCode();
                        outbound.ScoreAplicado = ScoreEnum.LeadAgendamentoSemMatriculaSegundoDia.GetHashCode();
                        break;

                    case 3:
                        outbound.ScoreInicial = ScoreEnum.LeadAgendamentoSemMatriculaSegundoDia.GetHashCode();
                        outbound.ScoreAplicado = ScoreEnum.LeadAgendamentoSemMatriculaTerceiroDia.GetHashCode();
                        break;

                    case 4:
                        outbound.ScoreInicial = ScoreEnum.LeadAgendamentoSemMatriculaTerceiroDia.GetHashCode();
                        outbound.ScoreAplicado = ScoreEnum.LeadAgendamentoSemMatriculaQuartoDia.GetHashCode();
                        break;

                    case 5:

                        outbound.ScoreInicial = ScoreEnum.LeadAgendamentoSemMatriculaQuartoDia.GetHashCode();
                        outbound.ScoreAplicado = ScoreEnum.LeadAgendamentoSemMatriculaQuintoDia.GetHashCode();
                        break;

                    default:
                        break;
                }
            }

            if (outbound.NumeroTentativa == 1 && outbound.DataeHoradoAgendamento == null)
            {
                _scoreAplicado = ScoreEnum.LeadPrimeiraTentativaContato.GetHashCode();
                _scoreInicial = ScoreEnum.LeadCadastroDiaAnteior.GetHashCode();
            }

            if (outbound.NumeroTentativa == 2 && outbound.DataeHoradoAgendamento == null)
            {
                _scoreAplicado = ScoreEnum.LeadSegundaTentativaContato.GetHashCode();
                _scoreInicial = ScoreEnum.LeadPrimeiraTentativaContato.GetHashCode();
            }

            if (outbound.NumeroTentativa >= 3 && outbound.DataeHoradoAgendamento == null)
            {
                var scoreCalculo = outbound.ScoreAplicado;
                _scoreInicial = scoreCalculo;
                _scoreAplicado = scoreCalculo - 10;
            }
        }

        public async Task<DtoAtendimento> AtualizaStatus(int id, int status)
        {
            Atendimento atendimentoRetorno;

            try
            {
                atendimentoRetorno = await AtendimentoRepository.GetByIdAsync(id);
                atendimentoRetorno.Status = status;
                atendimentoRetorno.StatusAlteracao = DateTime.Now;

                await AtendimentoRepository.UpdateAsync(atendimentoRetorno);
                return Mapper.Map<DtoAtendimento>(atendimentoRetorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AtualizaScoreAgendamento(AtendimentoOutbound outbound)
        {
            var dtoAtendimentos = BuscaAtendimentosPorId(outbound.AtendimentoId).Result;

            try
            {
                var dtoAtendimento = dtoAtendimentos.FirstOrDefault();
                dtoAtendimento.Score = outbound.ScoreAplicado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoAtendimento> Outbound(int idUnidade)
        {
            var servicoAtendimento = new AtendimentoService(AtendimentoRepository,
                                                            AtendimentoOutboundRepository,
                                                            ContatoRepository,
                                                            AtendimentoAgendamentoRepository,
                                                            Mapper);

            var atendimento = await servicoAtendimento.ProcessaOutbound(idUnidade);
            return atendimento;
        }

        public async Task<DtoAtendimentoOutbound> BuscarPorId(int idAtendimentoOutbound)
        {
            var _atendimentoOutbound = await AtendimentoOutboundRepository.GetByIdAsync(idAtendimentoOutbound);
            return Mapper.Map<DtoAtendimentoOutbound>(_atendimentoOutbound);
        }

        public async Task<IEnumerable<DtoAtendimentoOutbound>> HistoricoTentativas(int atendimentoId)
        {
            var _atendimentoOutbound = await AtendimentoOutboundRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<DtoAtendimentoOutbound>>(_atendimentoOutbound.Where(x => x.AtendimentoId == atendimentoId));
        }

        public async Task<int> ContarContatos(int idAtendimento)
        {
            var atendimentoOutbound = await AtendimentoOutboundRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<DtoAtendimentoOutbound>>(atendimentoOutbound.Where(x => x.AtendimentoId == idAtendimento)).Count();
        }

        public async Task<List<DtoAtendimento>> BuscaAtendimentosPorStatus(int status, int idUnidade)
        {
            var atendimentos = await AtendimentoRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<DtoAtendimento>>(atendimentos
                                                             .Where(x => !x.IsDelete &&
                                                                    x.Status == status &&
                                                                    x.UnidadeCadastro == idUnidade)
                                                             .OrderByDescending(x => x.Score)).ToList();
        }

        public async Task<List<DtoAtendimento>> BuscaAtendimentosPorId(int idAtendimento)
        {
            var atendimentos = await AtendimentoRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<DtoAtendimento>>(atendimentos
                                                             .Where(x => !x.IsDelete &&
                                                                    x.Id == idAtendimento)
                                                             .OrderByDescending(x => x.Score)).ToList();
        }

        public async Task<IEnumerable<DtoAtendimentoAgendamento>> HistoricoAgendamento(int atendimentoId)
        {
            var _agendamentos = await AtendimentoAgendamentoRepository.BuscarTodos();
            return Mapper.Map<IEnumerable<DtoAtendimentoAgendamento>>(_agendamentos.Where(x => x.IdAtendimento == atendimentoId));
        }

        private async Task<DtoAtendimento> AjustaStatusParaProximaTentativa(DtoAtendimentoOutbound retorno)
        {
            var status = StatusRegistros.ExecucaoProximoDia.GetHashCode();
            var atualizarAtendimento = await AtualizaStatus(retorno.AtendimentoId, status);

            return atualizarAtendimento;
        }

    }
}
