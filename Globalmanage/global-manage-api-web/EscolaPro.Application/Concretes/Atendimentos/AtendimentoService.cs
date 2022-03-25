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
        private readonly IContatosService _contatosService;
        private readonly IMapper _mapper;
        private readonly ILeadsService _leadsService;
        private const int _diasParaExperiar = 100;
        private static List<(int idUnidade, DateTime ultimoAjuste)> historicoAjustaFila = new List<(int, DateTime)>();
        private static DateTime _horarioDesativacao;
        private static DateTime _diaParaAjustarStatusAtendimento;
        private const int TelefoneIncorreOuInexitente = 8;
        private const int Matriculado = 9;
        private const int MatriculadoOutraInstuicao = 10;
        private const int NaoTemMaisInteresse = 11;
        private const int Facebook = 3;
        private const int WhatsApp = 4;

        public AtendimentoService(
            IAtendimentoRepository atendimentoRepository,
            IAtendimentoOutboundRepository atendimentoOutboundRepository,
            IContatoRepository contatoRepository,
            IContatosService contatosService,
            IAtendimentoAgendamentoRepository atendimentoAgendamentoRepository,
            IMapper mapper,
            ILeadsService leadsService)
        {
            _contatoRepository = contatoRepository;
            _contatosService = contatosService;
            _atendimentoRepository = atendimentoRepository;
            _outboundRepository = atendimentoOutboundRepository;
            _atendimentoAgendamentoRepository = atendimentoAgendamentoRepository;
            _mapper = mapper;
            _leadsService = leadsService;
        }

        public async Task<DtoAtendimento> BuscarPorId(int idCategoria)
        {
            var categoria = await _atendimentoRepository.GetByIdAsync(idCategoria);

            var atendimento = _mapper.Map<DtoAtendimento>(categoria);

            if (!string.IsNullOrEmpty(atendimento.DataeHoradoAgendamento))
            {
                var data = atendimento.DataeHoradoAgendamento.Split(" ")[0].Replace(",", "");
                var hora = atendimento.DataeHoradoAgendamento.Split(" ")[1];
                atendimento.DiadoAgendamento = data;
                atendimento.HoradoAgendamento = hora;
            }

            return atendimento;
        }

        public async Task<DtoAtendimento> BuscaIdPorNumerodeCelular(string celularCliente)
        {
            var retorno = await _atendimentoRepository.BuscaPorCelular(celularCliente);

            var atendimento = _mapper.Map<DtoAtendimento>(retorno);

            return atendimento;
        }

        public async Task<DtoAtendimento> ProcessaOutbound(int idUnidade)
        {
            await TransformaLeadsEmAtendimento();

            await AjusteStatusFilaAtendimento(idUnidade);

            var codigoEmExecucao = StatusAtendimento.EmExecucao.GetHashCode();
            var codigoExecutar = StatusAtendimento.Executar.GetHashCode();

            var dataAtual = DateTime.Now.Date;

            var outbounds = _atendimentoRepository
                             .BuscaAtendimentosParaFila(idUnidade)
                             .Result
                             .OrderBy(o => o.Score)
                             .ToList();

            await ProcessaExecucaoAposUmaHora(outbounds.Where(s => s.Status == codigoEmExecucao));

            var outboundsPrioritarios = outbounds.Where(o => o.Score > 2000 && o.Status == codigoExecutar);

            if (outboundsPrioritarios.Any())
            {
                var atendimentos = outboundsPrioritarios.Where(s => s.Status != codigoEmExecucao)
                                                        .OrderByDescending(o => o.Score);

                var maiorScoreDaLista = atendimentos.FirstOrDefault().Score;
                var atendimentosAgrupadoScore = atendimentos.Where(g => g.Score == maiorScoreDaLista)
                                                            .OrderByDescending(o => o.StatusAlteracao);

                var atendimento = atendimentosAgrupadoScore.FirstOrDefault();
                atendimento.Status = StatusAtendimento.EmExecucao.GetHashCode();
                atendimento.StatusAlteracao = DateTime.Now;

                await _atendimentoRepository.AtualizaStatusAtendimento(atendimento);

                return _mapper.Map<DtoAtendimento>(atendimento);
            }

            var outboundsParaExecutar = outbounds.Where(l => l.Status == codigoExecutar && l.Score < 2000)
                                                 .OrderByDescending(p => p.Score)
                                                 .FirstOrDefault();

            outboundsParaExecutar.Status = codigoEmExecucao;
            outboundsParaExecutar.StatusAlteracao = DateTime.Now;

            await _atendimentoRepository.AtualizaStatusAtendimento(outboundsParaExecutar);

            return _mapper.Map<DtoAtendimento>(outboundsParaExecutar);
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

        public async Task<DtoAtendimento> Inserir(DtoAtendimento dtoAtendimento)
        {
            var scoreBase = Score(dtoAtendimento.CanaldeAtendimento);

            var executar = StatusAtendimento.Executar.GetHashCode();
            var execucaoProximoDia = StatusAtendimento.ExecucaoProximoDia.GetHashCode();
            dtoAtendimento.Status = scoreBase > 2000 && dtoAtendimento.AgendamentodaMatricula == 3 ? executar : execucaoProximoDia;

            try
            {
                if (dtoAtendimento.Id == 0)
                {
                    CultureInfo cultures = new CultureInfo("pt-BR");
                    DateTime dataAtendimento = Convert.ToDateTime(dtoAtendimento.DataeHoradoAtendimento.Replace(",", ""), cultures);

                    DateTime? dataAgendamento = ConverteDataAgendamento(dtoAtendimento);

                    var _atendimento = new Atendimento()
                    {
                        CanaldeAtendimento = dtoAtendimento.CanaldeAtendimento,
                        Celular = dtoAtendimento.Celular,
                        ComonosConheceu = dtoAtendimento.ComonosConheceu,
                        CursodeInteresse = dtoAtendimento.CursodeInteresse,
                        DataeHoradoAgendamento = dataAgendamento,
                        DataeHoradoAtendimento = dataAtendimento,
                        Email = dtoAtendimento.Email,
                        Status = dtoAtendimento.Status,
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
                        MotivodoNaoAgendamento = dtoAtendimento.MotivodoNaoAgendamento,
                        Observacoes = dtoAtendimento?.Observacoes
                    };

                    var atendimento = await _atendimentoRepository.AddAsync(_atendimento);

                    if (atendimento.ExisteAgendamento || atendimento.AgendamentodaMatricula != 3)
                    {
                        var agendamento = new AtendimentoAgendamento
                        {
                            IdAtendimento = atendimento.Id,
                            HoraAgendamento = dtoAtendimento.HoradoAgendamento,
                            DataAgendamento = dataAgendamento.ToString().Split(" ")[0],
                            DataeHoradoUltimoContato = atendimento.DataeHoradoAtendimento,
                            TipoAgendamento = (TipoAgendamentoEnum)atendimento.AgendamentodaMatricula,
                            Celular = atendimento.Celular,
                            Observacoes = atendimento.Observacoes,
                            UsuarioCadastro = atendimento.UsuarioCadastro
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

        public async Task UpdateAtendimento(DtoAtendimento atendimento)
        {
            try
            {

                if (EhMotivoParaDesativarAtendimento(atendimento.MotivodoNaoAgendamento))
                    atendimento = AplicaStatusDesativado(atendimento);

                DateTime? dataAgendamento = null;

                if (atendimento.DiadoAgendamento != null)
                {
                    dataAgendamento = Convert.ToDateTime(atendimento.DiadoAgendamento.Split("T")[0] + " " + atendimento.HoradoAgendamento);
                }

                atendimento.StatusAlteracao = DateTime.Now;

                dataAgendamento = Convert.ToDateTime(atendimento.DataeHoradoAgendamento);

                var atendimentoParaAtualizar = _mapper.Map<Atendimento>(atendimento);

                //Solução para persistir a data devido diferença de tipo entre o model e o Dto
                atendimentoParaAtualizar.DataeHoradoAgendamento = dataAgendamento;

                await _atendimentoRepository.UpdateAsync(atendimentoParaAtualizar);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task EditaAtendimento(DtoAtendimento dtoAtendimento)
        {
            var executarProximoDia = StatusAtendimento.ExecucaoProximoDia.GetHashCode();

            dtoAtendimento.StatusAlteracao = DateTime.Now;

            if (!string.IsNullOrEmpty(dtoAtendimento.DiadoAgendamento))
            {
                dtoAtendimento.DiadoAgendamento = DateTime.Parse(dtoAtendimento.DiadoAgendamento)
                                                          .Date.ToString()
                                                          .Substring(0, 10);
            }                

            if (dtoAtendimento.EhNovoAgendamentoEditado)
            {
                var agendamento = new AtendimentoAgendamento
                {
                    IdAtendimento = dtoAtendimento.Id,
                    DataeHoradoUltimoContato = dtoAtendimento.StatusAlteracao,
                    DataAgendamento = dtoAtendimento.DiadoAgendamento,
                    HoraAgendamento = dtoAtendimento.HoradoAgendamento,
                    TipoAgendamento = (TipoAgendamentoEnum)dtoAtendimento.AgendamentodaMatricula,
                    Celular = dtoAtendimento.Celular.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", ""),
                    UsuarioCadastro = dtoAtendimento.UsuarioCadastro
                };

                await _atendimentoAgendamentoRepository.Inserir(agendamento);

                dtoAtendimento.ExisteAgendamento = true;
                dtoAtendimento.Status = executarProximoDia;
                dtoAtendimento.DataeHoradoAgendamento = ConverteDataAgendamento(dtoAtendimento).ToString();
            }

            var atendimento = _mapper.Map<Atendimento>(dtoAtendimento);

            await _atendimentoRepository.UpdateAsync(atendimento);
        }

        private DateTime? ConverteDataAgendamento(DtoAtendimento dtoAtendimento)
        {
            DateTime? dataAgendamento = null;
            if (!string.IsNullOrEmpty(dtoAtendimento.DiadoAgendamento))
            {
                dataAgendamento = Convert.ToDateTime(dtoAtendimento.DiadoAgendamento.Split("T")[0] + " " + dtoAtendimento.HoradoAgendamento);
            }

            return dataAgendamento;
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

                        await UpdateAtendimento(_mapper.Map<DtoAtendimento>(registro2));

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

        public async Task AjusteStatusFilaAtendimento(int idUnidade)
        {
            Dictionary<int, Atendimento> dicionarioOutbounds = new Dictionary<int, Atendimento>();

            var outbounds = _atendimentoRepository
                             .BuscaAtendimentosExecucaoProximoDia(idUnidade)
                             .Result
                             .OrderBy(o => o.Score)
                             .ToDictionary(o => o.Id, o => o);

            var dataHoraAtual = DateTime.Now;
            DateTime? dateNull = null;
            DateTime dataValorDefault = dateNull.GetValueOrDefault();

            const double limeteDeTempo = 1;

            var executarProximoDia = StatusAtendimento.ExecucaoProximoDia.GetHashCode();
            var emExecucao = StatusAtendimento.EmExecucao;
            var executar = StatusAtendimento.Executar;

            (int idUnidade, DateTime dataAjuste) historicoAtual = (idUnidade, dataHoraAtual.Date);

            var ehParaAjustarFila = historicoAjustaFila
                                    .FirstOrDefault(h => h.idUnidade == historicoAtual.idUnidade && h.ultimoAjuste.Date == historicoAtual.dataAjuste)
                                    == (0, dataValorDefault.Date) 
                                    ? true 
                                    : false;

            if (ehParaAjustarFila)
            {

                #region Ajusta status atendimento do dia                

                var registrosProximoDia = outbounds.Where(e => e.Value.Score <= 1000 && e.Value.Status == executarProximoDia).ToDictionary(r => r.Key, r => r.Value);

                if (registrosProximoDia.Any())
                {

                    var listAgendamento = _atendimentoAgendamentoRepository.BuscarTodos().Result.ToDictionary(a => a.Id, a => a);

                    foreach (var registro in registrosProximoDia)
                    {

                        var ehAtendimentoStatusFinanceiro = registro.Value.MotivodoNaoAgendamento == 2 || registro.Value.MotivodoNaoAgendamento == 3;

                        var dataParaAtendimento = registro.Value.StatusAlteracao.Date != dataValorDefault ? registro.Value.StatusAlteracao.Date : registro.Value.DataeHoradoAtendimento.Date;

                        var intervaloFinanceiro = dataHoraAtual.Date.Subtract(dataParaAtendimento);

                        if (ehAtendimentoStatusFinanceiro && intervaloFinanceiro.TotalDays < 30)
                        {
                            continue;
                        }

                        var agendamento = listAgendamento.Where(r => r.Value.IdAtendimento == registro.Key).LastOrDefault();

                        var existeAgendamento = agendamento.Value?.DataAgendamento.Length > 0 &&
                                                registro.Value.Status != emExecucao.GetHashCode();

                        if (existeAgendamento)
                        {
                            var dataAgendamento = Convert.ToDateTime(agendamento.Value.DataAgendamento);
                            var ehAgendamentoVencido = dataHoraAtual.Date > dataAgendamento.Date;
                            var ehPrazoParaUmaNovaTentativa = dataHoraAtual.Date > registro.Value.StatusAlteracao.Date;

                            if (ehAgendamentoVencido && ehPrazoParaUmaNovaTentativa)
                            {
                                await _atendimentoRepository.UpdateStatus(registro.Key, executar);
                                continue;
                            }
                        }

                        TimeSpan intervaloDecadastro = dataHoraAtual.Date.Subtract(registro.Value.DataeHoradoAtendimento.Date);
                        var ehcadastroDiaAnterior = intervaloDecadastro.TotalDays >= limeteDeTempo;
                        var podeContinuar = ehcadastroDiaAnterior &&
                                        !existeAgendamento &&
                                        registro.Value.Status == executarProximoDia.GetHashCode();

                        if (podeContinuar)
                        {
                            await _atendimentoRepository.UpdateStatus(registro.Key, executar);
                            continue;
                        }
                    }

                    var historicoAnterior = historicoAjustaFila.FirstOrDefault(h => h.Item1 == idUnidade);

                    if (historicoAnterior != (0, dataValorDefault.Date))
                        historicoAjustaFila.Remove(historicoAnterior);

                    historicoAjustaFila.Add(historicoAtual);
                }

                #endregion
            }

            #region Ajusta score acima de 2000 de volta pra fila

            var registrosRetorno = outbounds.Where(e => e.Value.Score > 2000 && e.Value.Status == executarProximoDia).ToDictionary(r => r.Key, r => r.Value);

            if (registrosRetorno.Any())
            {
                foreach (var registro in registrosRetorno)
                {
                    var dataRetorno = Convert.ToDateTime(registro.Value.DataeHoradoAgendamento);

                    TimeSpan intervaloretorno = dataRetorno.Subtract(registro.Value.StatusAlteracao);

                    var ehRetornoVencido = intervaloretorno.TotalHours > 0.98 ? dataHoraAtual >= dataRetorno && registro.Value.Score == 2090 : dataHoraAtual >= dataRetorno.AddHours(1) && registro.Value.Score == 2090;

                    if (ehRetornoVencido)
                    {
                        await _atendimentoRepository.UpdateStatus(registro.Key, executar);
                        continue;
                    }

                    var ehAgendamentoVencido = dataHoraAtual.Date > dataRetorno.Date;

                    if (ehAgendamentoVencido)
                    {
                        await _atendimentoRepository.UpdateStatus(registro.Key, executar);
                        continue;
                    }
                }
            }
            #endregion
        }

        private async Task ProcessaExecucaoAposUmaHora(IEnumerable<Atendimento> atendimentos)
        {

            if (atendimentos.Any())
            {
                const double limeteDeTempo = 1;
                var atendimentosParaFila = new List<Atendimento>();

                foreach (var registro in atendimentos)
                {
                    var dataHoraAtual = DateTime.Now;
                    var dataHoraAlteracao = registro.StatusAlteracao;
                    TimeSpan intervaloDeExecucao = dataHoraAtual.Subtract(dataHoraAlteracao);

                    if (intervaloDeExecucao.TotalHours > limeteDeTempo)
                    {
                        registro.Status = StatusAtendimento.Executar.GetHashCode();
                        registro.StatusAlteracao = dataHoraAtual;
                        atendimentosParaFila.Add(registro);
                    }
                }

                if (atendimentosParaFila.Any())
                {
                    var atendimentosUpdate = (IEnumerable<Atendimento>)atendimentosParaFila;
                    await _atendimentoRepository.UpdateListaStatus(atendimentosUpdate);
                }
            }
        }

        public async Task<int> AtualizaStatus(int id, int status)
        {
            Atendimento atendimentoRetorno;

            try
            {
                atendimentoRetorno = await _atendimentoRepository.GetByIdAsync(id);
                atendimentoRetorno.Status = status;
                atendimentoRetorno.StatusAlteracao = DateTime.Now;

                await _atendimentoRepository.UpdateAsync(atendimentoRetorno);

                return atendimentoRetorno.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task TransformaLeadsEmAtendimento()
        {
            var atendimentosLeads = await _leadsService.ProcessaDadosLeads();

            if (!atendimentosLeads.Any())
            {
                return;
            }

            const int naoAgendado = 3;
            const int motivoNaoAgendamento = 8;

            foreach (var registro in atendimentosLeads)
            {
                const int scoreFuraFila = 2080;

                registro.DataeHoradoAtendimento = DateTime.Now.ToString();
                registro.Score = scoreFuraFila;
                registro.Status = StatusAtendimento.Executar.GetHashCode();
                registro.AgendamentodaMatricula = naoAgendado;
                registro.MotivodoNaoAgendamento = motivoNaoAgendamento;

                await _atendimentoRepository.AddAsync(_mapper.Map<Atendimento>(registro));
            }
        }

        public async Task<int> ContaAtendimentosExecutar(int idUnidade)
        {
            var executar = StatusAtendimento.Executar.GetHashCode();

            var dataHoraAtual = DateTime.Now;

            var atendimentos = _atendimentoRepository.BuscaAtendimentosParaFila(idUnidade).Result.Where(
               u => u.UnidadeCadastro == idUnidade);

            await DesativaAtendimentosVencidos(atendimentos);

            await AplicaStatusMarticulado(atendimentos);

            var teste = await _atendimentoRepository.ObtenhaQtdAtendimentoPorStatus(idUnidade, StatusAtendimento.Executar.GetHashCode());
            return teste;
        }

        public async Task AplicaStatusMarticulado(IEnumerable<Atendimento> atendimentos)
        {
            var codigoEmExecucao = StatusAtendimento.EmExecucao.GetHashCode();
            var matriculado = StatusAtendimento.Matriculado;

            if (atendimentos.Any())
            {

                var dicionarioAtendimento = atendimentos.ToDictionary(o => o.Id, o => o);

                var contatos = _contatoRepository.GetAllAsync().Result.ToDictionary(c => c.Id, c => c);

                var resultado = from itemAtendimento in dicionarioAtendimento
                                let variavel = contatos.Any(t => t.Value.Celular == itemAtendimento.Value.Celular)
                                select new
                                {
                                    id = itemAtendimento.Value.Id,
                                    resultado = variavel
                                };

                foreach (var registro in resultado)
                {
                    if (registro.resultado)
                    {
                        await _atendimentoRepository.UpdateStatus(registro.id, matriculado);
                    }
                }
            }
        }

        public async Task DesativaAtendimentosVencidos(IEnumerable<Atendimento> registrosAtivos)
        {

            var dataHoraAtual = DateTime.Now;

            #region Regra atendimento matriculado ou com dias expirado
            var destivado = StatusAtendimento.Desativado;

            if (registrosAtivos.Any())
            {
                foreach (var registro in registrosAtivos)
                {

                    var DataHoraInicial = registro.DataeHoradoAtendimento;
                    var DataHoraVencimento = registro.DataeHoradoAtendimento.AddDays(_diasParaExperiar);
                    var diferenca = DataHoraVencimento.Subtract(dataHoraAtual);
                    int dias = diferenca.Days;

                    if (dias == 0)
                    {
                        await _atendimentoRepository.UpdateStatus(registro.Id, destivado);
                    }
                }
            }

            #endregion
        }

        public DtoAtendimento AplicaStatusDesativado(DtoAtendimento atendimento)
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

        public async Task<IEnumerable<Atendimento>> FiltraAtendimentos(FiltroAtendimentos filtro)
        {
            var retorno = await _atendimentoRepository.FiltrarAtendimento(filtro);

            foreach (var item in retorno)
            {
                bool verificaCelular = item.Celular.Length > 11;

                if (verificaCelular)
                {
                    item.Celular = item.Celular.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");
                }
            }

            if (filtro.StatusdoAtendimento == 5)
            {
                var retornofiltrado = retorno.Where(r => r.Status == 5);
                return retornofiltrado.OrderBy(r => r.DataeHoradoAtendimento);
            }

            if (filtro.StatusdoAtendimento != 5 && filtro.StatusdoAtendimento != 0 && filtro.StatusdoAtendimento != null)
            {
                var retornofiltrado = retorno.Where(r => r.Status != 5);

                return retornofiltrado.OrderBy(r => r.DataeHoradoAtendimento);
            }

            return retorno.OrderBy(r => r.DataeHoradoAtendimento);
        }

        private int Score(int canalAtendimento) => canalAtendimento switch
        {
            Facebook => 2040,
            WhatsApp => 2030,
            _ => 1000
        };

    }
}
