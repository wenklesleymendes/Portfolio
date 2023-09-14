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
    public class AtendimentoService : IAtendimentoService
    {
        private readonly IAtendimentoOutboundService _servicoOutbound;
        private readonly IAtendimentoRepository _atendimentoRepository;
        private readonly IAtendimentoOutboundRepository _outboundRepository;
        private readonly IAtendimentoAgendamentoRepository _atendimentoAgendamentoRepository;
        private readonly IContatoRepository _contatoRepository;
        private readonly IMapper _mapper;

        private const int _diasParaExperiar = 31;

        public AtendimentoService(
            IAtendimentoRepository atendimentoRepository,
            IAtendimentoOutboundRepository atendimentoOutboundRepository,
            IContatoRepository contatoRepository,
            IAtendimentoAgendamentoRepository atendimentoAgendamentoRepository,
            IMapper mapper)
        {
            _contatoRepository = contatoRepository;
            _atendimentoRepository = atendimentoRepository;
            _outboundRepository = atendimentoOutboundRepository;
            _atendimentoAgendamentoRepository = atendimentoAgendamentoRepository;
            _mapper = mapper;
        }

        public async Task<DtoAtendimento> BuscarPorId(int idCategoria)
        {
            var categoria = await _atendimentoRepository.GetByIdAsync(idCategoria);
            return _mapper.Map<DtoAtendimento>(categoria);
        }

        public async Task<DtoAtendimento> ProcessaOutbound(int idUnidade)
        {
            await GerenciaStatus(idUnidade);

            var desativado = StatusRegistros.Desativado.GetHashCode();
            var outbounds = _atendimentoRepository.GetAllAsync().Result
                                       .Where(u => u.UnidadeCadastro == idUnidade &&
                                              u.Status != desativado).ToList();

            var listaOrdenada = outbounds.OrderByDescending(p => p.Score).ToList();
            var outbound = listaOrdenada.Where(l => l.Status == StatusRegistros.Executar.GetHashCode()).FirstOrDefault();
            
            if (outbound != null)
            {
                var status = StatusRegistros.EmExecucao.GetHashCode();
                await AtualizaStatus(outbound.Id, status);
            }

            return _mapper.Map<DtoAtendimento>(outbound);
        }

        private async Task<int> AplicaScoreAgendamento(Atendimento registro)
        {
            var totalDiasAposAgendamento = DateTime.Now.Date - registro.DataeHoradoAgendamento.Value.Date;

            switch (totalDiasAposAgendamento.Days)
            {
                case 1:
                    registro.Score = ScoreEnum.LeadAgendamentoSemMatriculaPrimeiorDia.GetHashCode();
                    break;

                case 2:
                    registro.Score = ScoreEnum.LeadAgendamentoSemMatriculaSegundoDia.GetHashCode();
                    break;

                case 3:
                    registro.Score = ScoreEnum.LeadAgendamentoSemMatriculaTerceiroDia.GetHashCode();
                    break;

                case 4:
                    registro.Score = ScoreEnum.LeadAgendamentoSemMatriculaQuartoDia.GetHashCode();
                    break;

                case 5:
                    registro.Score = ScoreEnum.LeadAgendamentoSemMatriculaQuintoDia.GetHashCode();
                    break;

                default:
                    break;
            }

            await UpdateAtendimento(registro);

            return registro.Score;
        }

        private List<Atendimento> UpdateAgendamentos(Dictionary<int, DateTime?> agendadoOutbound, List<Atendimento> atendimentosOutbound)
        {
            if (atendimentosOutbound.Any() && agendadoOutbound.Count > 0)
            {
                var agendamentos = AtualizaAgendamentosAtendimento(agendadoOutbound, atendimentosOutbound).Result;
                atendimentosOutbound = ExecutaFiltrosStatusMatriculaAgendameto(agendamentos).Result;
            }

            return atendimentosOutbound;
        }

        public async Task AtualizaAgendamento(Dictionary<int, DateTime?> outboundAgendado, int idUnidade)
        {
            IEnumerable<Atendimento> todosAtendimento = await _atendimentoRepository.GetAllAsync();

            var atendimentoUnidade = todosAtendimento.Where(u => u.UnidadeCadastro == idUnidade).ToList();
            await AtualizaAgendamentosAtendimento(outboundAgendado, atendimentoUnidade);
        }

        private async Task<List<Atendimento>> AtualizaAgendamentosAtendimento(Dictionary<int, DateTime?> outboundAgendado, List<Atendimento> atendimentoUnidade)
        {
            var atendimentos = await _atendimentoRepository.GetAllAsync();
            foreach (var item in atendimentos)
            {
                foreach (var outbound in outboundAgendado)
                {
                    if (outbound.Key == item.Id)
                    {
                        item.ExisteAgendamento = true;
                        item.DataeHoradoAgendamento = outbound.Value;
                        await UpdateAtendimento(item);
                    }
                }
            }

            return atendimentos.ToList();
        }

        private async Task<List<Atendimento>> AplicaLeadAgendamentos(List<Atendimento> resultado)
        {
            var listaAgendamentoSemMatricula = resultado.Where(a =>
            {
                return a.DataeHoradoAgendamento > DateTime.Now &&
                       a.ExisteMatricula == false;
            });

            foreach (var atendimento in listaAgendamentoSemMatricula)
            {
                var qtdDias = DateTime.Now.Date - atendimento.DataeHoradoAgendamento.Value.Date;
                atendimento.Score = await ObtenhaScoreAgendamento(qtdDias.Days);
            }

            return listaAgendamentoSemMatricula.ToList();
        }

        private Task<int> ObtenhaScoreAgendamento(int dias)
        {

            switch (dias)
            {
                case 1:
                    return Task.FromResult(ScoreEnum.LeadAgendamentoSemMatriculaPrimeiorDia.GetHashCode());

                case 2:
                    return Task.FromResult(ScoreEnum.LeadAgendamentoSemMatriculaPrimeiorDia.GetHashCode());

                case 3:
                    return Task.FromResult(ScoreEnum.LeadAgendamentoSemMatriculaPrimeiorDia.GetHashCode());

                case 4:
                    return Task.FromResult(ScoreEnum.LeadAgendamentoSemMatriculaPrimeiorDia.GetHashCode());

                case 5:
                    return Task.FromResult(ScoreEnum.LeadAgendamentoSemMatriculaPrimeiorDia.GetHashCode());

                default:
                    break;
            }

            return Task.FromResult(0);
        }

        private async Task<Atendimento> AplicaScoreAtendimento(List<Atendimento> listaOrdenada, int score)
        {
            var atendimento = listaOrdenada.FirstOrDefault();
            atendimento.Score = ScoreEnum.LeadSegundaTentativaContato.GetHashCode();

            await UpdateAtendimento(atendimento);
            return atendimento;
        }

        private Task<List<Atendimento>> ExecutaFiltrosStatusMatriculaAgendameto(List<Atendimento> lista)
        {
            return Task.FromResult(lista.Where(m =>
            {
                return m.Status == StatusRegistros.Executar.GetHashCode() &&
                       m.ExisteMatricula == false;

            }).ToList());
        }

        public async Task<IEnumerable<DtoAtendimento>> BuscarTodos()
        {
            var atendimentos = await _atendimentoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<DtoAtendimento>>(atendimentos.Where(x => !x.IsDelete));
        }

        public Task<bool> Deletar(int idAtendimento)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Excluir(int idCategoria)
        {
            var categoria = await _atendimentoRepository.GetByIdAsync(idCategoria);
            categoria.IsDelete = true;
            await _atendimentoRepository.UpdateAsync(categoria);
            return categoria.IsDelete;
        }

        public async Task<DtoAtendimento> Inserir(DtoAtendimento dtoAtendimento)
        {
            var scoreBase = 1000;

            try
            {
                if (dtoAtendimento.Id == 0)
                {
                    CultureInfo cultures = new CultureInfo("pt-BR");
                    DateTime dataAtendimento = Convert.ToDateTime(dtoAtendimento.DataeHoradoAtendimento.Replace(",", ""), cultures);

                    DateTime? dataAgendamento = null;
                    if (dtoAtendimento.DiadoAgendamento != null)
                    {
                        dataAgendamento = Convert.ToDateTime(dtoAtendimento.DiadoAgendamento.Split("T")[0] + " " + dtoAtendimento.HoradoAgendamento);
                    }


                    var _atendimento = new Atendimento()
                    {
                        CanaldeAtendimento = dtoAtendimento.CanaldeAtendimento,
                        Celular = dtoAtendimento.Celular,
                        ComonosConheceu = dtoAtendimento.ComonosConheceu,
                        CursodeInteresse = dtoAtendimento.CursodeInteresse,
                        DataeHoradoAgendamento = dataAgendamento,
                        DataeHoradoAtendimento = dataAtendimento,
                        Email = dtoAtendimento.Email,
                        Status = StatusRegistros.ExecucaoProximoDia.GetHashCode(),
                        ExisteAgendamento = dataAgendamento != null,
                        ExisteMatricula = false,
                        MotivodeInteressenoCurso = dtoAtendimento.MotivodeInteressenoCurso,
                        NomedoCliente = dtoAtendimento.NomedoCliente,
                        UsuarioCadastro = dtoAtendimento.UsuarioCadastro,
                        UsuarioLogado = dtoAtendimento.UsuarioLogado,
                        Periodo = dtoAtendimento.Periodo,
                        TelefoneFixo = dtoAtendimento.TelefoneFixo,
                        Score = scoreBase,
                        UnidadeCadastro = dtoAtendimento.UnidadeCadastro,
                        AgendamentodaMatricula = dtoAtendimento.AgendamentodaMatricula,
                        MotivodoNaoAgendamento = dtoAtendimento.MotivodoNaoAgendamento
                    };

                    var atendimento = await _atendimentoRepository.AddAsync(_atendimento);

                    if (atendimento.ExisteAgendamento)
                    {
                        var agendamento = new AtendimentoAgendamento
                        {
                            IdAtendimento = atendimento.Id,
                            HoraAgendamento = dtoAtendimento.HoradoAgendamento,
                            DataAgendamento = dataAgendamento.ToString().Split(" ")[0],
                            DataeHoradoUltimoContato = atendimento.DataeHoradoAtendimento,
                            TipoAgendamento = (TipoAgendamentoEnum)atendimento.AgendamentodaMatricula,
                            Celular = atendimento.Celular
                        };

                        await _atendimentoAgendamentoRepository.Inserir(agendamento);
                    }

                    return _mapper.Map<DtoAtendimento>(_atendimento);
                }
                else
                {
                    await _atendimentoRepository.UpdateAsync(_mapper.Map<Atendimento>(dtoAtendimento));
                    return await BuscarPorId(dtoAtendimento.Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task UpdateEmLisataStatus(List<Atendimento> lista, int status = 1)
        {
            try
            {
                foreach (var atendimento in lista)
                {
                    atendimento.Status = status;
                    atendimento.StatusAlteracao = DateTime.Now;

                    await _atendimentoRepository.UpdateAsync(atendimento);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task UpdateAtendimento(Atendimento atendimento)
        {
            try
            {
                await _atendimentoRepository.UpdateAsync(atendimento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<DtoAtendimento>> BuscarAgendamentos(int idUnidade)
        {
            var atendimentos = await _atendimentoRepository.GetAllAsync();
            var lista = FiltraAgendamento(idUnidade, atendimentos).ToList();

            var outbounds = _outboundRepository.GetAllAsync().Result;
            var agendamentoOutbound = outbounds.Where(x => x.MotivodoNaoAgendamento == null).ToList();
            var agendamentos = _mapper.Map<IEnumerable<AtendimentoOutbound>>(agendamentoOutbound);
            var agendamentosOutboundDia = agendamentos.Where(p => p.DataeHoradoAgendamento.Value.Date == DateTime.Now.Date);

            if (agendamentosOutboundDia.Any())
            {
                foreach (var outbound in agendamentosOutboundDia)
                {
                    var registro2 = atendimentos.Where(p => p.Id == outbound.AtendimentoId).FirstOrDefault();

                    if (registro2.DataeHoradoAgendamento != outbound.DataeHoradoAgendamento)
                    {
                        registro2.DataeHoradoAgendamento = outbound.DataeHoradoAgendamento;
                        registro2.AgendamentodaMatricula = outbound.AgendamentodaMatricula;
                        await UpdateAtendimento(registro2);

                        lista.Add(registro2);
                    }
                }

                lista = FiltraAgendamento(idUnidade, lista).ToList();
            }

            return _mapper.Map<IEnumerable<DtoAtendimento>>(lista);
        }

        private static IOrderedEnumerable<Atendimento> FiltraAgendamento(int idUnidade, IEnumerable<Atendimento> atendimentos)
        {
            return atendimentos.Where(d =>
            {
                return d.DataeHoradoAgendamento?.Date == DateTime.Now.Date &&
                       d.UnidadeCadastro == idUnidade;

            }).ToList().OrderBy(x => x.DataeHoradoAgendamento);
        }

        public async Task GerenciaStatus(int idUnidade)
        {
            var outbounds = _atendimentoRepository.GetAllAsync().Result.Where(
               u => u.UnidadeCadastro == idUnidade &&
               u.Status != StatusRegistros.Desativado.GetHashCode()).ToList();

            #region Ajusta status atendimento com mais de 1 hora em execucao

            var listaEmExecucao = outbounds.Where(e => e.Status == StatusRegistros.EmExecucao.GetHashCode()).ToList();

            if (listaEmExecucao.Any())
            {
                var emExecucao = StatusRegistros.Executar.GetHashCode();
                await AplicaNovoStatus(listaEmExecucao, emExecucao);
            }            

            #endregion

            #region Regra atendimento com matriculas ou dias expirado

            var registrosAtivos = outbounds.Where(e =>
             e.Status == StatusRegistros.Executar.GetHashCode() ||
             e.Status == StatusRegistros.ExecucaoProximoDia.GetHashCode()).ToList();

            if (registrosAtivos.Any())
            {
                var desativado = StatusRegistros.Desativado.GetHashCode();
                await AplicaNovoStatus(registrosAtivos, desativado);
            }            

            #endregion

            #region Ajusta status para execução proroximo dia

            var registrosProximoDia = outbounds.Where(e => e.Status == StatusRegistros.ExecucaoProximoDia.GetHashCode()).ToList();

            if (registrosProximoDia.Any())
            {
                var executar = StatusRegistros.Executar.GetHashCode();
                await AplicaNovoStatus(registrosProximoDia, executar);
            }            

            #endregion

        }

        private async Task AplicaNovoStatus(List<Atendimento> lista, int novoStatus)
        {
            var dataHoraAtual = DateTime.Now;
            const double limeteDeTempo = 1;

            foreach (var registro in lista)
            {
                var existeExecucao = registro.Status == StatusRegistros.EmExecucao.GetHashCode();

                if (existeExecucao)
                {
                    var dataHoraAlteracao = registro.StatusAlteracao;
                    TimeSpan intervaloDeExecucao = dataHoraAtual.Subtract(dataHoraAlteracao);

                    if (intervaloDeExecucao.TotalHours > limeteDeTempo)
                    {
                        await AtualizaStatus(registro.Id, novoStatus);
                        continue;
                    }
                }                

                var servicoContatos = new ContatosService(_contatoRepository, _mapper);
                var existeContatoCelular = servicoContatos.ExisteContatoCelular(registro.Celular).Result;
                var existeContatoEmal = servicoContatos.ExisteContatoEmail(registro.Email).Result;
                var existeMatricula = existeContatoCelular || existeContatoEmal;

                if (existeMatricula)
                {
                    registro.ExisteMatricula = existeMatricula;
                    registro.Status = StatusRegistros.Desativado.GetHashCode();

                    await _atendimentoRepository.UpdateAsync(registro);
                    continue;
                }

                var DataHoraInicial = registro.DataeHoradoAtendimento;
                var DataHoraFinal = registro.DataeHoradoAtendimento.AddDays(_diasParaExperiar);
                var diferenca = DataHoraFinal - dataHoraAtual;
                int dias = diferenca.Days;

                if (dias < 0)
                {
                    await AtualizaStatus(registro.Id, novoStatus);
                    continue;
                }

                var agendamento = _atendimentoAgendamentoRepository.BuscarTodos()
                                                 .Result.Where(r => r.IdAtendimento == registro.Id)
                                                 .LastOrDefault();
                if(agendamento != null && !existeExecucao)
                {
                    var dataAgendamento = Convert.ToDateTime(agendamento.DataAgendamento);
                    var ehAgendamentoVencido = dataHoraAtual.Date > dataAgendamento.Date;
                    if (ehAgendamentoVencido)
                    {
                        novoStatus = StatusRegistros.Executar.GetHashCode();
                        await AtualizaStatus(registro.Id, novoStatus);
                        continue;
                    }
                }            

                TimeSpan intervaloDecadastro = dataHoraAtual.Date.Subtract(registro.DataeHoradoAtendimento.Date);
                var ehcadastroDiaAnterior = intervaloDecadastro.TotalDays >=  limeteDeTempo;
                var podeContinuar = ehcadastroDiaAnterior && agendamento is null && !existeExecucao;

                if (podeContinuar)
                {
                    novoStatus = StatusRegistros.Executar.GetHashCode();
                    await AtualizaStatus(registro.Id, novoStatus);
                    continue;
                }
            }
        }

        public async Task<DtoAtendimento> AtualizaStatus(int id, int status)
        {
            Atendimento atendimentoRetorno;

            try
            {
                atendimentoRetorno = await _atendimentoRepository.GetByIdAsync(id);
                atendimentoRetorno.Status = status;
                atendimentoRetorno.StatusAlteracao = DateTime.Now;

                await _atendimentoRepository.UpdateAsync(atendimentoRetorno);
                return _mapper.Map<DtoAtendimento>(atendimentoRetorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
