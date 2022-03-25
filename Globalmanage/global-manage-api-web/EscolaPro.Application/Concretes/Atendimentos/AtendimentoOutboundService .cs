using AutoMapper;
using EscolaPro.Core.Model;
using EscolaPro.Core.Model.Atendimentos;
using EscolaPro.Core.Model.Enums;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Interfaces.Atendimentos;
using EscolaPro.Service.Dto.AtendimentoVO;
using EscolaPro.Service.Interfaces;
using EscolaPro.Service.Interfaces.Atendimentos;
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
        private readonly IAtendimentoRepository _repositoryAtendimento;
        private readonly IContatosService _contatosService;
        private readonly IContatoRepository _contatoRepository;
        private readonly IAtendimentoAgendamentoRepository _atendimentoAgendamentoRepository;
        private readonly IMapper _mapper;
        private readonly ILeadsService _leadsService;
        private readonly IAtendimentoService _atendimentoService;

        private int _scoreInicial = 0;
        private int _scoreAplicado = 0;
        private const int TelefoneIncorreOuInexitente = 8;
        private const int Matriculado = 9;
        private const int MatriculadoOutraInstuicao = 10;
        private const int NaoTemMaisInteresse = 11;

        public IAtendimentoOutboundRepository AtendimentoOutboundRepository => _atendimentoOutboundRepository;

        public IAtendimentoRepository AtendimentoRepository => _repositoryAtendimento;

        public IContatoRepository ContatoRepository => _contatoRepository;

        public IContatosService ContatosService => _contatosService;

        public IMapper Mapper => _mapper;

        public IAtendimentoAgendamentoRepository AtendimentoAgendamentoRepository => _atendimentoAgendamentoRepository;

        public ILeadsService LeadsService => _leadsService;

        public AtendimentoOutboundService(
            IAtendimentoOutboundRepository atendimentoOutboundRepository,
            IAtendimentoRepository repositoryAtendimento,
            IContatoRepository ContatoRepository,
            IAtendimentoAgendamentoRepository atendimentoAgendamentoRepository,
            IMapper mapper,
            ILeadsService leadsService,
            IAtendimentoService atendimentoService)
        {
            _atendimentoOutboundRepository = atendimentoOutboundRepository;
            _repositoryAtendimento = repositoryAtendimento;
            _contatoRepository = ContatoRepository;
            _atendimentoAgendamentoRepository = atendimentoAgendamentoRepository;
            _mapper = mapper;
            _leadsService = leadsService;
            _atendimentoService = atendimentoService;
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
                    UsuarioCadastro = outbound.UsuarioCadastro,
                    Observacao = outbound.Observacao,
                    ScoreAplicado = _scoreAplicado.GetHashCode(),
                    ScoreInicial = _scoreInicial.GetHashCode(),
                    NumeroOutbound = outbound.NumeroOutbound,
                    MotivodoNaoAgendamento = outbound.MotivodoNaoAgendamento,
                    DataeHoradoAgendamento = dataAgendamento,
                    AgendamentodaMatricula = outbound.AgendamentodaMatricula,
                };

                await AtendimentoOutboundRepository.AddAsync(_atendimentoOutbound);

                var atendimento = await _repositoryAtendimento.BuscarPorId(outbound.AtendimentoId);

                //var dtoAtendimento = Mapper.Map<DtoAtendimento>(atendimento);

                if (outbound.AgendamentodaMatricula != 3)
                {
                    var agendamento = new AtendimentoAgendamento
                    {
                        IdAtendimento = outbound.AtendimentoId,
                        DataeHoradoUltimoContato = _atendimentoOutbound.DataHoraContato,
                        DataAgendamento = dataAgendamento.ToString().Split(" ")[0],
                        HoraAgendamento = outbound.HoradoAgendamento,
                        TipoAgendamento = (TipoAgendamentoEnum)outbound.AgendamentodaMatricula,
                        Celular =  atendimento.Celular.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", ""),
                        UsuarioCadastro = outbound.UsuarioCadastro,
                        Observacoes = outbound.Observacao,
                    };

                    await _atendimentoAgendamentoRepository.Inserir(agendamento);

                    atendimento.DataeHoradoAgendamento = dataAgendamento;
                    atendimento.ExisteAgendamento = true;
                }                

                if (outbound.MotivodoNaoAgendamento == 14)
                {

                    atendimento.DataeHoradoAgendamento = dataAgendamento;
                    atendimento.ExisteAgendamento = true;
                    atendimento.Status = StatusAtendimento.ExecucaoProximoDia.GetHashCode();
                    atendimento.Score = _scoreAplicado.GetHashCode();
                }

                atendimento.Status = StatusAtendimento.ExecucaoProximoDia.GetHashCode();
                atendimento.Score = _scoreAplicado.GetHashCode();

                if (EhMotivoParaDesativarAtendimento(outbound.MotivodoNaoAgendamento))
                    atendimento = AplicaStatusDesativado(atendimento);

                await _repositoryAtendimento.UpdateAsync(atendimento);

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
                                    .Result.Where(a => a.DataAgendamento != null &&
                                                  a.DataAgendamento.Length > 0)
                                    .LastOrDefault();

            if (ultimoAgendamento != null)
            {
                DateTime? dataAgendamento = null;
                dataAgendamento = Convert.ToDateTime(
                    ultimoAgendamento.DataAgendamento + " " + ultimoAgendamento.HoraAgendamento);

                var totalDiasAposAgendamento = DateTime.Now.Date.Subtract(dataAgendamento.Value.Date);

                switch (totalDiasAposAgendamento.Days)
                {
                    case 1:
                        _scoreInicial = outbound.ScoreAplicado;
                        _scoreAplicado = ScoreEnum.LeadAgendamentoSemMatriculaPrimeiorDia.GetHashCode();
                        return;

                    case 2:
                        _scoreInicial = ScoreEnum.LeadAgendamentoSemMatriculaPrimeiorDia.GetHashCode();
                        _scoreAplicado = ScoreEnum.LeadAgendamentoSemMatriculaSegundoDia.GetHashCode();
                        return;

                    case 3:
                        _scoreInicial = ScoreEnum.LeadAgendamentoSemMatriculaSegundoDia.GetHashCode();
                        _scoreAplicado = ScoreEnum.LeadAgendamentoSemMatriculaTerceiroDia.GetHashCode();
                        return;

                    case 4:
                        _scoreInicial = ScoreEnum.LeadAgendamentoSemMatriculaTerceiroDia.GetHashCode();
                        _scoreAplicado = ScoreEnum.LeadAgendamentoSemMatriculaQuartoDia.GetHashCode();
                        return;

                    case 5:
                        _scoreInicial = ScoreEnum.LeadAgendamentoSemMatriculaQuartoDia.GetHashCode();
                        _scoreAplicado = ScoreEnum.LeadAgendamentoSemMatriculaQuintoDia.GetHashCode();
                        return;

                    default:
                        break;
                }
            }

            if(outbound.MotivodoNaoAgendamento == 14)
            {
                _scoreAplicado = ScoreEnum.LeadRetornoAgendado.GetHashCode();
                _scoreInicial = outbound.ScoreAplicado == 2080 ? 930 : outbound.ScoreAplicado;
                return;
            }

            if (outbound.NumeroOutbound == 1 && outbound.DataeHoradoAgendamento == null)
            {
                _scoreAplicado = ScoreEnum.LeadPrimeiraTentativaContato.GetHashCode();
                _scoreInicial = ScoreEnum.LeadCadastroDiaAnteior.GetHashCode();
                return;
            }

            if (outbound.NumeroOutbound == 2 && outbound.DataeHoradoAgendamento == null)
            {
                _scoreAplicado = ScoreEnum.LeadSegundaTentativaContato.GetHashCode();
                _scoreInicial = ScoreEnum.LeadPrimeiraTentativaContato.GetHashCode();
                return;
            }

            if (outbound.NumeroOutbound >= 3 && outbound.DataeHoradoAgendamento == null)
            {
                if (outbound.ScoreAplicado > 2000)
                {
                    var outboundsAnteriores = AtendimentoOutboundRepository.RetornaHistoricoOutbound(outbound.AtendimentoId).Result;
                    var ultimoScore = outboundsAnteriores.LastOrDefault(o => o.ScoreInicial != 0 && o.ScoreAplicado < 2000).ScoreAplicado;
                    outbound.ScoreInicial = ultimoScore;
                }

                _scoreInicial = outbound.ScoreAplicado;
                _scoreAplicado = outbound.ScoreInicial - 10;
                return;
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

                await _repositoryAtendimento.UpdateAsync(atendimentoRetorno);

                return Mapper.Map<DtoAtendimento>(atendimentoRetorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoAtendimento> ObtenhaOutbound(int idUnidade)
        {
            var servicoAtendimento = new AtendimentoService(AtendimentoRepository,
                                                            AtendimentoOutboundRepository,                                                            
                                                            ContatoRepository,
                                                            ContatosService,
                                                            AtendimentoAgendamentoRepository,
                                                            Mapper,
                                                            LeadsService);

            return await servicoAtendimento.ProcessaOutbound(idUnidade);
        }

        public async Task<DtoAtendimentoOutbound> BuscarPorId(int idAtendimentoOutbound)
        {
            var _atendimentoOutbound = await AtendimentoOutboundRepository.GetByIdAsync(idAtendimentoOutbound);
            return Mapper.Map<DtoAtendimentoOutbound>(_atendimentoOutbound);
        }

        public async Task<IEnumerable<AtendimentoOutbound>> HistoricoTentativas(int atendimentoId)
        {
            var _atendimentoOutbound = await AtendimentoOutboundRepository.RetornaHistoricoOutbound(atendimentoId);

            return _atendimentoOutbound;
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
            var status = StatusAtendimento.ExecucaoProximoDia.GetHashCode();
            var atualizarAtendimento = await AtualizaStatus(retorno.AtendimentoId, status);

            return atualizarAtendimento;
        }

        public Atendimento AplicaStatusDesativado(Atendimento atendimento)
        {

            var destivado = StatusAtendimento.Desativado.GetHashCode();
            var matriculado = StatusAtendimento.Matriculado.GetHashCode();
            var motivoNaoAgendamento = atendimento.MotivodoNaoAgendamento.GetHashCode();

            atendimento.Status = motivoNaoAgendamento == Matriculado ? atendimento.Status = matriculado : atendimento.Status = destivado;

            return atendimento;
        }

        private bool EhMotivoParaDesativarAtendimento(int? motivoNaoAgendamento) =>
        motivoNaoAgendamento switch
        {
            TelefoneIncorreOuInexitente => true,
            Matriculado => true,
            MatriculadoOutraInstuicao => true,
            NaoTemMaisInteresse => true,
            _ => false
        };
    }
}