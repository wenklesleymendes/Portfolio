using AutoMapper;
using EscolaPro.Core.Helpers;
using EscolaPro.Core.Model;
using EscolaPro.Core.Model.Pagamentos;
using EscolaPro.Core.Model.Tickets;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Interfaces.Pagamentos;
using EscolaPro.Repository.Interfaces.Tickets;
using EscolaPro.Service.Dto.AlunosVO;
using EscolaPro.Service.Dto.ControleUsuarioVO;
using EscolaPro.Service.Dto.TicketVO;
using EscolaPro.Service.Interfaces;
using EscolaPro.Service.Interfaces.MatriculaAlunos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes
{
    public class TicketService : ITicketService
    {
        private readonly IAssuntoTicketRepository _assuntoTicketRepository;
        private readonly IMensagemTicketRepository _mensagemTicketRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly IUnidadeService _unidadeService;
        private readonly ICentroCustoRepository _centroCustoRepository;
        private readonly IAnexoService _anexoService;
        private readonly IUsuarioDestinarioTicketRepository _usuarioDestinarioTicketRepository;
        private readonly IMatriculaAlunoService _matriculaAlunoService;
        private readonly IAlunoFinanceiroContratoRepository _alunoFinanceiroContratoRepository;
        private readonly IOcorrenciaRepository _ocorrenciaRepository;
        private readonly IMapper _mapper;

        public TicketService(
            IAssuntoTicketRepository assuntoTicketRepository,
            IMensagemTicketRepository mensagemTicketRepository,
            ITicketRepository ticketRepository,
            IUsuarioService usuarioService,
            IAnexoService anexoService,
            IUnidadeService unidadeService,
            ICentroCustoRepository centroCustoRepository,
            IUsuarioDestinarioTicketRepository usuarioDestinarioTicketRepository,
            IMatriculaAlunoService matriculaAlunoService,
            IAlunoFinanceiroContratoRepository alunoFinanceiroContratoRepository,
            IOcorrenciaRepository ocorrenciaRepository,
            IMapper mapper)
        {
            _assuntoTicketRepository = assuntoTicketRepository;
            _mensagemTicketRepository = mensagemTicketRepository;
            _ticketRepository = ticketRepository;
            _ocorrenciaRepository = ocorrenciaRepository;
            _usuarioService = usuarioService;
            _anexoService = anexoService;
            _unidadeService = unidadeService;
            _centroCustoRepository = centroCustoRepository;
            _usuarioDestinarioTicketRepository = usuarioDestinarioTicketRepository;
            _matriculaAlunoService = matriculaAlunoService;
            _alunoFinanceiroContratoRepository = alunoFinanceiroContratoRepository;
            _mapper = mapper;
        }

        public async Task<DtoTicketRetorno> BuscarPorId(int idTicket)
        {
            var ticket = await _ticketRepository.BuscarPorId(idTicket);

            return _mapper.Map<DtoTicketRetorno>(ticket);
        }


        public async Task<IEnumerable<DtoTicket>> BuscarPorMatriculaId(int matriculaId, int assuntoTicketId =0)
        {
            var tickets = await _ticketRepository.BuscarPorMatriculaId(matriculaId, assuntoTicketId);

            return _mapper.Map<IEnumerable<DtoTicket>>(tickets);
        }
        public async Task<IEnumerable<DtoTicket>> BuscarTodos()
        {
            List<DtoTicket> ticketLista = new List<DtoTicket>();

            var tickets = await _ticketRepository.GetAllAsync();

            foreach (var item in tickets.Where(x => !x.IsDelete))
            {
                var ticket = await _ticketRepository.BuscarPorId(item.Id);

                ticketLista.Add(_mapper.Map<DtoTicket>(ticket));
            }

            return _mapper.Map<IEnumerable<DtoTicket>>(ticketLista);
        }

        public async Task<DtoDashboardTicket> ConsultarDashBoard(int usuarioLogadoId)
        {
            DtoDashboardTicket dtoDashboard = new DtoDashboardTicket();
            var usuarioLogado = await _usuarioService.BuscarPorId(usuarioLogadoId);


            var ticketIds = await _ticketRepository.TickestsUsuario(_mapper.Map<Usuario>(usuarioLogado));

            var tic = await _ticketRepository.GetAllAsync();
            List<DtoTicketRetorno> tickets = new List<DtoTicketRetorno>();

            foreach (var item in ticketIds)
            {
                var ticket = await BuscarPorId(item.Id);

                tickets.Add(ticket);
            }

            dtoDashboard.Abertos = tickets.Where(x => x.Status != StatusTicketEnum.Finalizado).Count();

            dtoDashboard.Resolvidos = tickets.Where(x => x.Status == StatusTicketEnum.Finalizado).Count();

            dtoDashboard.TotalOcorrencias = tickets.Count();

            foreach (var item in tickets)
            {


                if (item.Status != Core.Model.Tickets.StatusTicketEnum.Finalizado)
                {
                    var assunto = await _assuntoTicketRepository.GetByIdAsync(item.AssuntoTicketId);

                    var totalDiasAtraso = VerificaTotalDiasAtrasado(item.DataAbertura.Value, assunto.TempoEmDias);

                    if (totalDiasAtraso > 0)
                    {
                        dtoDashboard.Atrasados++;
                    }
                }
            }

            return dtoDashboard;
        }

        public async Task<bool> Deletar(int idTicket)
        {
            var ticket = await _ticketRepository.GetByIdAsync(idTicket);
            ticket.IsDelete = true;
            var id = await _ticketRepository.UpdateAsync(ticket);
            return id > 0 ? true : false;
        }

        public async Task<DtoTicketRetorno> Inserir(DtoTicket dtoTicket)
        {
            Ticket ticket = new Ticket
            {
                AssuntoTicketId = dtoTicket.AssuntoTicketId,
                DataAbertura = DateTime.Now,
                UsuarioLogadoId = dtoTicket.UsuarioLogadoId,
                Status = StatusTicketEnum.Aberto,
                NumeroProtocolo = new Random(DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + DateTime.Now.Minute).Next().ToString(),
                MatriculaId = dtoTicket.MatriculaId
            };

            ticket.DestinatarioTicket = new List<DestinatarioTicket>();

            var destinatarioTicket = _mapper.Map<DestinatarioTicket>(dtoTicket.DestinatarioTicket);

            destinatarioTicket.StatusTicket = StatusTicketEnum.Aberto;

            ticket.DestinatarioTicket.Add(destinatarioTicket);

            int ticketId = 0;

            if (ticket.Id == 0)
            {
                ticket.DataAbertura = DateTime.Now;
                ticket.Status = StatusTicketEnum.Aberto;

                var retorno = await _ticketRepository.AddAsync(_mapper.Map<Ticket>(ticket));

                ticketId = retorno.Id;
            }

            return await BuscarPorId(ticketId);
        }

        public async Task<DtoMensagemTicket> ResponderTicket(DtoMensagemTicket mensagemTicket)
        {
            try
            {
                var destinarioTicket = _mapper.Map<DtoDestinatarioTicket>(mensagemTicket);

                await AtualizarTicket(mensagemTicket.TicketId, mensagemTicket.StatusTicket);

                var mensagem = await _mensagemTicketRepository.AddAsync(_mapper.Map<DestinatarioTicket>(mensagemTicket));

                return _mapper.Map<DtoMensagemTicket>(mensagem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Ticket> FinalizarTicket(int idTicket)
        {
            try
            {
                var ticket = await _ticketRepository.BuscarPorId(idTicket);
                ticket.Status = StatusTicketEnum.Finalizado;
                await _ticketRepository.UpdateAsync(ticket);
                return ticket;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<DtoGridTicket>> Filtrar(DtoFiltrarTicket dtoFiltrarTicket)
        {
            try
            {
                var usuarioLogado = await _usuarioService.BuscarPorId(dtoFiltrarTicket.UsuarioLogadoId);

                switch (usuarioLogado.PerfilUsuario.PerfilSistemaEnum)
                {
                    case Core.Model.Enums.PerfilSistemaEnum.FinanceiroSenior:
                    case Core.Model.Enums.PerfilSistemaEnum.FinanceiroPleno:
                    case Core.Model.Enums.PerfilSistemaEnum.FinanceiroJunior:
                        dtoFiltrarTicket.UsuarioFinanceiro = true;
                        break;
                    default:
                        dtoFiltrarTicket.UsuarioFinanceiro = false;
                        break;
                }

                dtoFiltrarTicket.VerTudo = usuarioLogado.PerfilUsuario.VerTodasUnidades;

                IEnumerable<Ticket> meusTicketsAbertos = new List<Ticket>();
                IEnumerable<Ocorrencia> ocorrencias = new List<Ocorrencia>();

                if (dtoFiltrarTicket.MatriculaId.HasValue)
                {
                    meusTicketsAbertos = await _ticketRepository.BuscarPorMatriculaId(dtoFiltrarTicket.MatriculaId.Value);
                    ocorrencias = await _ocorrenciaRepository.BuscarPorMatriculaId(dtoFiltrarTicket.MatriculaId.Value);
                }
                else
                {
                    if (dtoFiltrarTicket.FiltroAvancado)
                    {
                        if (!string.IsNullOrEmpty(dtoFiltrarTicket.NomeResponsavel))
                        {
                            DtoUsuario usuario = await _usuarioService.FiltrarUsuario(dtoFiltrarTicket.NomeResponsavel);
                            if (usuario != null && usuario.Id > 0)
                            {
                                dtoFiltrarTicket.UsuarioLogadoId = usuario.Id;
                                dtoFiltrarTicket.FiltroUsuario = usuario;
                            }
                        }
                        else
                            dtoFiltrarTicket.UsuarioLogadoId = 0;
                        meusTicketsAbertos = await _ticketRepository.Filtrar(_mapper.Map<FiltrarTicket>(dtoFiltrarTicket));
                    }
                    else
                    {
                        DtoUsuario usuario = await _usuarioService.BuscarPorId(dtoFiltrarTicket.UsuarioLogadoId);
                        meusTicketsAbertos = await _ticketRepository.TickestsUsuario(_mapper.Map<Usuario>(usuario), dtoFiltrarTicket.StatusTickets ?? StatusTicketEnum.Todos);
                    }
                }

                var cds = meusTicketsAbertos.Where(x => x.MatriculaId > 0).ToList();

                List<DtoGridTicket> gridTickets = new List<DtoGridTicket>();

                foreach (var ticket in meusTicketsAbertos.ToList())
                {
                    DtoAlunoSimples aluno = new DtoAlunoSimples();

                    if (ticket.MatriculaId > 0 && ticket.Matricula != null)
                    {

                        aluno.Nome = ticket.Matricula.Aluno.Nome;
                        aluno.Matricula = ticket.Matricula.NumeroMatricula;
                        aluno.UnidadeAluno = ticket.Matricula.Unidade.Nome;
                    }

                    gridTickets.Add(new DtoGridTicket
                    {
                        TicketId = ticket.Id,
                        NumeroProtocolo = ticket.NumeroProtocolo,
                        Assunto = ticket.AssuntoTicket.Descricao,
                        DataAbertura = ticket.DataAbertura.Value,
                        DataAtendimento = ticket.DestinatarioTicket.Last().DataAtendimento,
                        SLA = VerificarSLA(ticket.DataAbertura.Value, ticket.AssuntoTicket.TempoEmDias, ticket.Status),
                        Atendente = await BuscarAtendente(ticket.DestinatarioTicket.Last()),
                        Status = ticket.Status,
                        UsuarioResponsavel = ticket.UsuarioLogadoId != 0 ? await BuscarUsuarioResponsavel(ticket.UsuarioLogadoId) : null,
                        Aluno = aluno
                    });
                }

                if (ocorrencias != null)
                {
                    foreach (var item in ocorrencias)
                    {
                        gridTickets.Add(new DtoGridTicket
                        {
                            TicketId = item.Id,
                            Assunto = item.Assunto,
                            UsuarioResponsavel = item.UsuarioLogadoId != 0 ? await BuscarUsuarioResponsavel(item.UsuarioLogadoId) : null,
                            DataAbertura = item.DataAbertura,
                            SLA = "Não Aplicável",
                            NumeroProtocolo = item.NumeroProtocolo,
                            IsOcorrencia = true,
                            Status = StatusTicketEnum.Ocorrencia,
                            DataAtendimento = item.DataAtendimento ?? item.DataAbertura.Value,
                            Aluno = new DtoAlunoSimples
                            {
                                Matricula = item.Matricula.NumeroMatricula,
                                Nome = item.Matricula.Aluno.Nome,
                                UnidadeAluno = item.Matricula.Unidade.Nome
                            }
                        });

                    }
                }
                return gridTickets.OrderBy(x => x.DataAbertura);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AtualizarTicket(int ticketId, StatusTicketEnum statusTicket)
        {
            try
            {
                var ticket = await _ticketRepository.BuscarPorId(ticketId);

                ticket.Status = statusTicket;

                await _ticketRepository.UpdateAsync(ticket);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<string> BuscarAtendente(DestinatarioTicket destinatarioTicket)
        {
            try
            {
                string atendente = string.Empty;

                if (destinatarioTicket.UsuarioDestinarioTicket.Count() > 0)
                {
                    foreach (var atendenteResponsavel in destinatarioTicket.UsuarioDestinarioTicket)
                    {
                        var usuario = await _usuarioService.BuscarPorId(atendenteResponsavel.FuncionarioId.Value);

                        if (destinatarioTicket.UsuarioDestinarioTicket.Count > 0)
                        {
                            if (destinatarioTicket.UsuarioDestinarioTicket.Count == 1)
                            {
                                atendente = $"{usuario.Funcionario.Nome}";
                            }
                            else
                            {
                                if (atendenteResponsavel == destinatarioTicket.UsuarioDestinarioTicket.Last())
                                {
                                    atendente = atendente + $"{usuario.Funcionario.Nome}";
                                }
                                else
                                {
                                    atendente = atendente + $"{usuario.Funcionario.Nome}, ";
                                }
                            }
                        }
                        else
                        {
                            atendente = usuario.Funcionario.Nome;
                        }

                    }
                }
                else
                {
                    var unidade = await _unidadeService.BuscarPorId(destinatarioTicket.UnidadeId.Value);

                    if (destinatarioTicket.DepartamentoId > 0)
                    {
                        var departamento = await _centroCustoRepository.GetByIdAsync(destinatarioTicket.DepartamentoId);

                        atendente = $"{unidade.Nome}-{departamento.Nome}";
                    }
                    else
                    {
                        return unidade.Nome;
                    }
                }

                return atendente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string VerificarSLA(DateTime dataAbertura, int tempoEmDias, StatusTicketEnum statusTicketEnum)
        {
            try
            {

                string descricaoSLA = string.Empty;

                switch (statusTicketEnum)
                {
                    case StatusTicketEnum.Aberto:
                    case StatusTicketEnum.Devolvido:
                    case StatusTicketEnum.EmAtendimento:
                        var quantidadeDiasAtrasado = VerificaTotalDiasAtrasado(dataAbertura, tempoEmDias);

                        if (quantidadeDiasAtrasado > 0)
                            descricaoSLA = descricaoSLA = $"Atrasado {quantidadeDiasAtrasado} dias";
                        else
                            descricaoSLA = "Em dia";
                        break;
                    case StatusTicketEnum.Finalizado:
                        descricaoSLA = "Finalizado";
                        break;
                    default:
                        break;
                }

                return descricaoSLA;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<string> BuscarUsuarioResponsavel(int? idFuncionarioResponsavel)
        {
            try
            {
                if (idFuncionarioResponsavel.HasValue)
                {
                    var usuario = await _usuarioService.BuscarPorId(idFuncionarioResponsavel.Value);

                    if (usuario.IsAluno)
                        return "Via Portal do Aluno";
                    else
                        return usuario?.Funcionario?.Nome ?? "";
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoTicketTimeline> BuscarMensagens(int ticketId)
        {
            var ticket = await _ticketRepository.BuscarPorId(ticketId);

            var mensagensTickets = await _mensagemTicketRepository.BuscarPorTicket(ticketId);

            var usuarioResponsavel = await _usuarioService.BuscarPorId(ticket.UsuarioLogadoId);

            DtoDestinatarioTicket destinatarioResponsavel = new DtoDestinatarioTicket();

            if (usuarioResponsavel.UserName != "admin" && !usuarioResponsavel.IsAluno)
            {
                destinatarioResponsavel = new DtoDestinatarioTicket
                {
                    DepartamentoId = usuarioResponsavel.Departamento.Id,
                    UnidadeId = usuarioResponsavel.Unidade.Id,
                    TicketId = ticket.Id,
                };
            }

            destinatarioResponsavel.UsuarioDestinarioTicket = new List<DtoUsuarioDestinarioTicket>();

            destinatarioResponsavel.UsuarioDestinarioTicket.Add(new DtoUsuarioDestinarioTicket { FuncionarioId = usuarioResponsavel.Id });

            DtoTicketTimeline ticketTimelines = new DtoTicketTimeline
            {
                TicketId = ticket.Id,
                StatusTicket = ticket.Status,
                Mensagens = new List<DtoMensagensTimeLine>(),
                UsuarioResponsavel = destinatarioResponsavel
            };

            foreach (var item in mensagensTickets)
            {
                var usuario = await _usuarioService.BuscarPorId(item.UsuarioLogadoId);

                string atendente = string.Empty;

                var anexo = await _anexoService.BuscarPorFiltro(new Core.Model.Anexos.AnexoFiltrar { DestinatarioTicketId = item.Id });

                var nomeUsuario = usuario.UserName == "admin" ? "admin" : (usuario.IsAluno ? "Via Portal do Aluno" : usuario.Funcionario.Nome);

                switch (item.StatusTicket)
                {
                    case StatusTicketEnum.Aberto:
                        atendente = $"Aberto por {nomeUsuario}";
                        break;
                    case StatusTicketEnum.Devolvido:
                    case StatusTicketEnum.EmAtendimento:
                        atendente = $"Respondido por {nomeUsuario}";
                        break;
                    case StatusTicketEnum.Finalizado:
                        atendente = $"Finalizado por {nomeUsuario}";
                        break;
                    default:
                        break;
                }

                ticketTimelines.Mensagens.Add(new DtoMensagensTimeLine
                {
                    Data = item.DataAtendimento,
                    Mensagem = item.Mensagem,
                    StatusTicket = item.StatusTicket,
                    Atendente = atendente,
                    Unidade = usuario.UserName == "admin" ? "ADMIN" : usuario.Unidade.Nome,
                    AnexoId = anexo.Count() > 0 ? anexo.FirstOrDefault().Id.Value : 0,
                    ArquivoString = anexo.Count() > 0 ? anexo.FirstOrDefault().ArquivoString : "",
                    Extensao = anexo.Count() > 0 ? anexo.FirstOrDefault().Extensao : "",
                });
            }

            return ticketTimelines;
        }

        private int VerificaTotalDiasAtrasado(DateTime dataAbertura, int tempoEmDiaSLA)
        {
            try
            {
                var dataAtendimento = dataAbertura.AddDays(tempoEmDiaSLA);

                TimeSpan totalDiasAtrasados;

                if (dataAtendimento < DateTime.Now)
                {
                    totalDiasAtrasados = DateTime.Now.Date - dataAtendimento.Date;


                    return totalDiasAtrasados.Days;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<List<DtoGridTicket>> MontarGrid(List<Ticket> ticketLista, int usuarioLagadoId)
        {
            try
            {
                List<DtoGridTicket> gridTickets = new List<DtoGridTicket>();

                foreach (var item in ticketLista)
                {
                    var ticket = await _ticketRepository.BuscarPorId(item.Id);

                    var ultimoAtendimento = ticket.DestinatarioTicket.LastOrDefault();

                    var usuarioDestinatarioLista = await _usuarioDestinarioTicketRepository.BuscarPorDestinatarioTicket(ultimoAtendimento.Id);

                    ultimoAtendimento.UsuarioDestinarioTicket = new List<UsuarioDestinarioTicket>();

                    ultimoAtendimento.UsuarioDestinarioTicket = usuarioDestinatarioLista.ToList();

                    var existeUsuarioEncaminhado = ultimoAtendimento.UsuarioDestinarioTicket.Where(x => x.FuncionarioId == usuarioLagadoId).ToList();

                    if (existeUsuarioEncaminhado.Count > 0)
                    {

                        var assuntoTicket = await _assuntoTicketRepository.GetByIdAsync(ticket.AssuntoTicketId);

                        gridTickets.Add(new DtoGridTicket
                        {
                            TicketId = ticket.Id,
                            NumeroProtocolo = ticket.NumeroProtocolo,
                            Assunto = assuntoTicket.Descricao,
                            DataAbertura = ticket.DataAbertura.Value,
                            DataAtendimento = ultimoAtendimento.DataAtendimento,
                            SLA = VerificarSLA(ticket.DataAbertura.Value, assuntoTicket.Id, ticket.Status),
                            Atendente = await BuscarAtendente(ultimoAtendimento),
                            Status = ticket.Status,
                            UsuarioResponsavel = await BuscarUsuarioResponsavel(ticket.UsuarioLogadoId)
                        });
                    }

                }

                return gridTickets;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Ticket>> MontarTicket(Ticket item)
        {
            List<Ticket> tickets = new List<Ticket>();

            var ticket = await _ticketRepository.BuscarPorId(item.Id);

            List<DestinatarioTicket> destinatarioTickets = new List<DestinatarioTicket>();

            foreach (var destinatarioTicket in ticket.DestinatarioTicket)
            {
                var usuarioDestinatarioLista = await _usuarioDestinarioTicketRepository.BuscarPorDestinatarioTicket(destinatarioTicket.Id);

                destinatarioTicket.UsuarioDestinarioTicket = usuarioDestinatarioLista.ToList();

                destinatarioTickets.Add(destinatarioTicket);
            }

            tickets.Add(ticket);

            return tickets;
        }

        public async Task TicketErroBoletoItau(int matriculaId, int usuarioLogadoId, List<Pagamento> pagamentos)
        {
            try
            {
                var matricula = await _matriculaAlunoService.BuscarPorId(matriculaId);

                var usuarioLogado = await _usuarioService.BuscarPorId(usuarioLogadoId);

                var usuarioLista = await _usuarioService.BuscarUsuarioAtendente();

                var unidade = await _unidadeService.BuscarPorId(usuarioLogado.Unidade.Id);

                var assunto = await _assuntoTicketRepository.BuscarAssuntoSolicitacao();

                var ticket = new Dto.TicketVO.DtoTicket
                {
                    DataAbertura = DateTime.Now,
                    Status = Core.Model.Tickets.StatusTicketEnum.Aberto,
                    AssuntoTicketId = assunto.Id,
                    UsuarioLogadoId = usuarioLogadoId,
                    IdFuncionarioAtendente = usuarioLogadoId,
                    MatriculaId = matriculaId,
                    BaixaBoleto = true
                };

                ticket.DestinatarioTicket = new Dto.TicketVO.DtoDestinatarioTicket();

                ticket.DestinatarioTicket.UnidadeId = unidade.Id;

                ticket.DestinatarioTicket.DepartamentoId = unidade.CentroCusto.FirstOrDefault().Id;

                string descricao = $"Aluno(a): {matricula.Aluno.Nome}\nRM: {matricula.NumeroMatricula}\nCPF: {Core.Helpers.CoreHelpers.FormatCNPJouCPF(matricula.Aluno.CPF)}\n\n";

                foreach (var pagamento in pagamentos)
                {
                    if (pagamentos.Count() == 1)
                    {
                        descricao = descricao + $"Nosso Número: {pagamento.NossoNumero} \n" +
                            $"Seu Número: {pagamento.NumeroRegistro} \n" +
                            $"Data Vencimento: {pagamento.DataVencimento.Value.ToString("dd/MM/yyyy")} \n" +
                            $"Valor: R$ {pagamento.Valor.ToString("N2")}\n";
                    }
                    else
                    {
                        descricao = descricao + $"| Nosso Número: {pagamento.NossoNumero} \n" +
                            $"Seu Número: {pagamento.NumeroRegistro} \n" +
                            $"Data Vencimento: {pagamento.DataVencimento.Value.ToString("dd/MM/yyyy")} \n" +
                            $"Valor: R$ {pagamento.Valor.ToString("N2")}\n";
                    }
                }

                ticket.DestinatarioTicket.Mensagem = descricao;
                ticket.DestinatarioTicket.UsuarioLogadoId = usuarioLogadoId;
                ticket.DestinatarioTicket.StatusTicket = Core.Model.Tickets.StatusTicketEnum.Aberto;
                ticket.DataAbertura = DateTime.Now;
                ticket.NumeroProtocolo = new Random(DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + DateTime.Now.Minute).Next().ToString();

                ticket.DestinatarioTicket.UsuarioDestinarioTicket = new List<DtoUsuarioDestinarioTicket>();

                foreach (var item in usuarioLista)
                {
                    var usuario = await _usuarioService.FiltrarUsuario(new Dto.UsuarioVO.DtoFiltrarUsuario { FuncionarioId = item.Funcionario?.Id });

                    ticket.DestinatarioTicket.UsuarioDestinarioTicket.Add(new Dto.TicketVO.DtoUsuarioDestinarioTicket
                    {
                        FuncionarioId = usuario.FirstOrDefault().Id
                    });
                }

                await Inserir(ticket);

            }
            catch
            {
                throw;
            }
        }
    }
}
