using AutoMapper;
using EscolaPro.Core.Extensions;
using EscolaPro.Core.Model.Pagamentos;
using EscolaPro.Core.Model.PainelMatricula;
using EscolaPro.Core.Model.PainelMatricula.Enums;
using EscolaPro.Core.Model.Provas;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Interfaces.AgendaProvas;
using EscolaPro.Repository.Interfaces.CadastroAluno;
using EscolaPro.Repository.Interfaces.MatriculaAlunos;
using EscolaPro.Repository.Interfaces.Pagamentos;
using EscolaPro.Repository.Interfaces.Tickets;
using EscolaPro.Service.Dto;
using EscolaPro.Service.Dto.AgendaProvaVO;
using EscolaPro.Service.Dto.AlunosVO;
using EscolaPro.Service.Dto.MatriculaAlunoVO;
using EscolaPro.Service.Dto.PlanoPagamentoVO;
using EscolaPro.Service.Dto.TicketVO;
using EscolaPro.Service.Dto.UsuarioVO;
using EscolaPro.Service.Helpers;
using EscolaPro.Service.Interfaces;
using EscolaPro.Service.Interfaces.AgendaProvas;
using EscolaPro.Service.Interfaces.MatriculaAlunos;
using EscolaPro.Service.Interfaces.ProvasCertificados;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes.ProvasCertificados
{
    public class ProvaAlunoService : IProvaAlunoService
    {
        private readonly IProvaAlunoRepository _provaAlunoRepository;
        private readonly IAgendaProvaService _agendaProvaService;
        private readonly IMatriculaAlunoRepository _matriculaAlunoRepository;
        private readonly IPlanoPagamentoService _planoPagamentoService;
        private readonly IMatriculaAlunoService _matriculaAlunoService;
        private readonly IAlunoFinanceiroContratoRepository _alunoFinanceiroContratoRepository;
        private readonly IColegioAutorizadoService _colegioAutorizadoService;
        private readonly IAgendaProvaRepository _agendaProvaRepository;
        private readonly IUnidadeRepository _unidadeRepository;
        private readonly IUnidadeTransporteProvaRepository _unidadeTransporteProvaRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly INacionalidadeRepository _nacionalidadeRepository;
        private readonly INaturalidadeRepository _naturalidadeRepository;
        private readonly IAlunoRepository _alunoRepository;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IAnexoRepository _anexoRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly IUnidadeService _unidadeService;
        private readonly IProvaMateriaAlunoService _provaMateriaAlunoService;
        private readonly IAssuntoTicketRepository _assuntoTicketRepository;
        private readonly ITicketService _ticketService;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        private readonly IWhatsAppService _whatsAppService;
        private readonly IWebHostEnvironment _webHostingEnvironment;

        public ProvaAlunoService(
            IProvaAlunoRepository provaAlunoRepository,
            IAgendaProvaService agendaProvaService,
            IMatriculaAlunoRepository matriculaAlunoRepository,
            IPlanoPagamentoService planoPagamentoService,
            IMatriculaAlunoService matriculaAlunoService,
            IAlunoFinanceiroContratoRepository alunoFinanceiroContratoRepository,
            IColegioAutorizadoService colegioAutorizadoService,
            IAgendaProvaRepository agendaProvaRepository,
            IUnidadeRepository unidadeRepository,
            IUnidadeTransporteProvaRepository unidadeTransporteProvaRepository,
            IEnderecoRepository enderecoRepository,
            INacionalidadeRepository nacionalidadeRepository,
            INaturalidadeRepository naturalidadeRepository,
            IAlunoRepository alunoRepository,
            IEmailSenderService emailSenderService,
            IAnexoRepository anexoRepository,
            IProvaMateriaAlunoService provaMateriaAlunoService,
            IUsuarioService usuarioService,
            IUnidadeService unidadeService,
            IAssuntoTicketRepository assuntoTicketRepository,
            ITicketService ticketService,
            IOptions<AppSettings> appSettings,
            IMapper mapper,
            IWhatsAppService whatsAppService,
            IWebHostEnvironment webHostingEnvironment)
        {
            _provaAlunoRepository = provaAlunoRepository;
            _agendaProvaService = agendaProvaService;
            _matriculaAlunoRepository = matriculaAlunoRepository;
            _planoPagamentoService = planoPagamentoService;
            _matriculaAlunoService = matriculaAlunoService;
            _alunoFinanceiroContratoRepository = alunoFinanceiroContratoRepository;
            _colegioAutorizadoService = colegioAutorizadoService;
            _agendaProvaRepository = agendaProvaRepository;
            _unidadeRepository = unidadeRepository;
            _unidadeTransporteProvaRepository = unidadeTransporteProvaRepository;
            _enderecoRepository = enderecoRepository;
            _nacionalidadeRepository = nacionalidadeRepository;
            _naturalidadeRepository = naturalidadeRepository;
            _alunoRepository = alunoRepository;
            _emailSenderService = emailSenderService;
            _anexoRepository = anexoRepository;
            _usuarioService = usuarioService;
            _unidadeService = unidadeService;
            _ticketService = ticketService;
            _appSettings = appSettings.Value;
            _assuntoTicketRepository = assuntoTicketRepository;
            _provaMateriaAlunoService = provaMateriaAlunoService;
            _whatsAppService = whatsAppService;
            _mapper = mapper;

            _webHostingEnvironment = webHostingEnvironment;
        }

        public async Task<DtoProvaAluno> BuscarPorId(int provaAlunoId)
        {
            var prova = await _provaAlunoRepository.BuscarPorId(provaAlunoId);

            var dtoProva = _mapper.Map<DtoProvaAluno>(prova);
            //if (prova.Id >0)
            //{
            //    var materias = _provaMateriaAlunoRepository.BuscarPorId(provaAlunoId);
            //    dtoProva.ProvaMateriaAluno = new DtoProvaMateriaAluno();
            //    dtoProva.ProvaMateriaAluno.
            //}

            return dtoProva;
        }

        public DtoProvaAluno BuscarPorMatriculaId(int matriculaId)
        {
            var provas = _provaAlunoRepository.BuscarInscritoPorMatricula(matriculaId);

            return _mapper.Map<DtoProvaAluno>(provas);
        }

        public async Task<DtoProvaAluno> Inserir(DtoProvaAlunoRequest dtoProvaAluno)
        {
            ProvaAluno provaRetorno;
            DtoProvaAluno retorno;

            // Cadastro
            if (dtoProvaAluno.Id == 0)
            {
                if (dtoProvaAluno.AgendaProvaId == 0)
                    dtoProvaAluno.AgendaProvaId = null;

                ProvaAluno provaAluno = new ProvaAluno
                {
                    StatusProva = dtoProvaAluno.StatusProva,
                    AgendaProvaId = dtoProvaAluno.AgendaProvaId,
                    ColegioAutorizadoId = dtoProvaAluno.ColegioAutorizadoId,
                    DataProva = dtoProvaAluno.DataProva,
                    TipoProva = dtoProvaAluno.TipoProva,
                    DataInscricao = dtoProvaAluno.DataInscricao,
                    Observacao = dtoProvaAluno.Observacao,
                    LocalProva = dtoProvaAluno.LocalProva,
                    MatriculaAlunoId = dtoProvaAluno.MatriculaAlunoId,
                    TipoTransporte = dtoProvaAluno.TipoTransporte,
                    IdentificacaoUsuario = dtoProvaAluno.IdentificacaoUsuario,
                    SenhaProva = dtoProvaAluno.SenhaProva,
                    UsuarioLogadoId = dtoProvaAluno.UsuarioLogadoId,
                    UnidadeTransporteProvaId = dtoProvaAluno.UnidadeTransporteProva?.Id,
                    UnidadeTransporteProva = (dtoProvaAluno.UnidadeTransporteProvaId == null || dtoProvaAluno.UnidadeTransporteProvaId == 0) ? _mapper.Map<UnidadeTransporteProva>(dtoProvaAluno.UnidadeTransporteProva) : null
                };

                if (provaAluno.UnidadeTransporteProva != null && provaAluno.UnidadeTransporteProva.Id != 0)
                {
                    provaAluno.UnidadeTransporteProvaId = provaAluno.UnidadeTransporteProva.Id;
                    provaAluno.UnidadeTransporteProva = null;
                }
                if (provaAluno.UnidadeTransporteProva != null && provaAluno.AgendaProvaId.HasValue)
                {
                    provaAluno.UnidadeTransporteProva.AgendaProvaId = provaAluno.AgendaProvaId.Value;
                    provaAluno.UnidadeTransporteProva.UnidadeParticipanteProva = null;
                }

                provaRetorno = await _provaAlunoRepository.AddAsync(provaAluno);

                retorno = _mapper.Map<DtoProvaAluno>(provaRetorno);
            }
            // Edição
            else
            {
                provaRetorno = await _provaAlunoRepository.GetByIdAsync(dtoProvaAluno.Id);

                provaRetorno.StatusProva = dtoProvaAluno.StatusProva;
                provaRetorno.AgendaProvaId = dtoProvaAluno.AgendaProvaId == 0 ? null : dtoProvaAluno.AgendaProvaId;
                provaRetorno.ColegioAutorizadoId = dtoProvaAluno.ColegioAutorizadoId;
                provaRetorno.DataProva = dtoProvaAluno.DataProva;
                provaRetorno.LocalProva = dtoProvaAluno.LocalProva;
                provaRetorno.MatriculaAlunoId = dtoProvaAluno.MatriculaAlunoId;
                provaRetorno.TipoTransporte = dtoProvaAluno.TipoTransporte;

                if (provaRetorno.DataInscricao != null)
                    provaRetorno.DataInscricao = dtoProvaAluno.DataInscricao;

                if (provaRetorno.TipoProva == TipoProvaEnum.Online && dtoProvaAluno.TipoProva == TipoProvaEnum.Presencial)
                {
                    provaRetorno.IdentificacaoUsuario = null;
                    provaRetorno.SenhaProva = null;
                }

                // Se mudar o tipo de prova de presencial para online, remove o aluno do ônibus
                if (provaRetorno.UnidadeTransporteProvaId.HasValue &&
                    provaRetorno.TipoProva == TipoProvaEnum.Presencial &&
                    (dtoProvaAluno.TipoProva == TipoProvaEnum.Online ||
                     dtoProvaAluno.TipoTransporte == TipoTransporteEnum.Particular))
                {
                    await _unidadeTransporteProvaRepository.RemoveAsync(provaRetorno.UnidadeTransporteProvaId);
                    provaRetorno.UnidadeTransporteProvaId = null;
                }

                await _provaAlunoRepository.UpdateAsync(provaRetorno);

                retorno = _mapper.Map<DtoProvaAluno>(provaRetorno);
            }

            // Dados para preenchimento do formulário
            var matricula = await _matriculaAlunoRepository.BuscarPorId(dtoProvaAluno.MatriculaAlunoId);

            matricula.TipoSituacaoCertificado = TipoSituacaoCertificadoEnum.InscritoParaProva;

            await _matriculaAlunoRepository.UpdateAsync(matricula);

            var endereco = await _enderecoRepository.PorIdEndereco(matricula.Aluno.EnderecoId);
            var nacionalidade = await _nacionalidadeRepository.GetByIdAsync(matricula.Aluno.NacionalidadeId);
            var naturalidade = matricula.Aluno.NaturalidadeId.HasValue ? await _naturalidadeRepository.GetByIdAsync(matricula.Aluno.NaturalidadeId.Value) : null;

            var inscricaoProvaModel = new InscricaoProvaModel()
            {
                NomeCompleto = matricula.Aluno.Nome,
                DataNascimento = matricula.Aluno.DataNascimento.ToString("dd/MM/yyyy"),
                EstadoCivil = matricula.Aluno.EstadoCivil.ToString(),
                Rg = matricula.Aluno.RG,
                OrgaoExpeditor = matricula.Aluno.OrgaoExpedicao,
                UF = "",
                CPF = matricula.Aluno.CPF,
                TituloEleitoral = matricula.Aluno.TituloEleitoral,
                ZonaEleitoral = matricula.Aluno.Zona,
                SecaoEleitoral = matricula.Aluno.Secao,
                Nacionalidade = nacionalidade.Descricao,
                Naturalidade = naturalidade?.Descricao ?? "",
                NomeMae = matricula.Aluno.NomeMae,
                NomeResponsavel = matricula.Aluno.NomeResponsavel,
                Endereco = endereco.Rua,
                EnderecoComplemento = endereco.Complemento,
                EnderecoNumero = endereco.Numero,
                EnderecoBairro = endereco.Bairro,
                EnderecoCidade = endereco.Cidade,
                EnderecoUF = endereco.Estado,
                EnderecoCEP = endereco.CEP
            };

            switch (matricula.Aluno.Sexo)
            {
                case Core.Model.SexoEnum.Masculino:
                    inscricaoProvaModel.Sexo = "M";
                    break;
                case Core.Model.SexoEnum.Feminino:
                    inscricaoProvaModel.Sexo = "F";
                    break;
                default:
                    inscricaoProvaModel.Sexo = "";
                    break;
            }

            if (retorno.UnidadeTransporteProva != null)
                retorno.UnidadeTransporteProva.provaAlunos = null;

            //Enviar email
            if (matricula.UnidadeId.HasValue)
            {
                var matriculaMail = _matriculaAlunoRepository.GetInformacoesEmail(dtoProvaAluno.MatriculaAlunoId);

                string mailBody;

                if (dtoProvaAluno.TipoProva == TipoProvaEnum.Presencial)
                {
                    if (dtoProvaAluno.TipoTransporte == TipoTransporteEnum.Escola)
                    {
                        mailBody = CoreHelpers.MontarEmailInscricaoProva(dtoProvaAluno, _mapper.Map<DtoAluno>(matriculaMail.Aluno), matriculaMail.Unidade, matriculaMail.Curso.NacionatalTec);
                        await _emailSenderService.SendEmailAsync(new string[] { matriculaMail.Aluno.Contato.Email }, "Informações Sobre Sua Prova", mailBody, null, matriculaMail.Curso.NacionatalTec, null);
                    }
                    else
                    {
                        var colegioAutorizado = await _colegioAutorizadoService.BuscarPorId(dtoProvaAluno.ColegioAutorizadoId);
                        mailBody = CoreHelpers.MontarEmailInscricaoProva(dtoProvaAluno, _mapper.Map<DtoAluno>(matriculaMail.Aluno), matriculaMail.Unidade, matriculaMail.Curso.NacionatalTec, colegioAutorizado);
                        await _emailSenderService.SendEmailAsync(new string[] { matriculaMail.Aluno.Contato.Email }, "Importante Agendamento da Sua Prova", mailBody, null, matriculaMail.Curso.NacionatalTec, null);
                    }
                }
                else
                {
                    mailBody = CoreHelpers.MontarEmailInscricaoProvaOnline(_mapper.Map<DtoAluno>(matriculaMail.Aluno), matriculaMail.Unidade, matriculaMail.Curso.NacionatalTec);
                    await _emailSenderService.SendEmailAsync(new string[] { matriculaMail.Aluno.Contato.Email }, "Inscrição de Prova Realizada Com Sucesso", mailBody, null, matriculaMail.Curso.NacionatalTec, null);
                }



                EnviarWhatsAppIncrição(dtoProvaAluno, matriculaMail);
                //await _emailSenderService.SendEmailAsync(new string[] { matriculaMail.Aluno.Contato.Email }, "Informações sobre sua prova", mailBody, null, matriculaMail.Curso.NacionatalTec, null);
            }

            retorno.InscricaoProvaDocumento = InscricaoProvaHelper.InscricaoProva(inscricaoProvaModel, matricula.Curso.Descricao);

            return retorno;
        }

        public async Task<DtoProvaAluno> AtualizarStatusProva(DtoProvaAluno dtoProvaAluno)
        {
            ProvaAluno provaRetorno;
            DtoProvaAluno retorno;

            try
            {

                provaRetorno = await _provaAlunoRepository.GetByIdAsync(dtoProvaAluno.Id);

                provaRetorno.StatusProva = dtoProvaAluno.StatusProva;
                provaRetorno.Observacao = dtoProvaAluno.Observacao;

                await _provaAlunoRepository.UpdateAsync(provaRetorno);

                retorno = _mapper.Map<DtoProvaAluno>(provaRetorno);


                // Dados para preenchimento do formulário
                var matricula = await _matriculaAlunoRepository.BuscarPorId(dtoProvaAluno.MatriculaAlunoId);

                matricula.TipoSituacaoCertificado = (TipoSituacaoCertificadoEnum)(int)retorno.StatusProva;

                await _matriculaAlunoRepository.UpdateAsync(matricula);


            }
            catch (Exception ex)
            {

                throw;
            }

            return retorno;
        }

        public async Task<DtoProvaAluno> CadastrarCredenciais(DtoProvaAlunoRequest credenciais)
        {
            var provaAluno = await _provaAlunoRepository.GetByIdAsync(credenciais.Id);

            // Enviar email com credenciais

            provaAluno.IdentificacaoUsuario = credenciais.IdentificacaoUsuario;
            provaAluno.SenhaProva = credenciais.SenhaProva;

            await _provaAlunoRepository.UpdateAsync(provaAluno);

            var matricula = _matriculaAlunoRepository.GetInformacoesEmail(provaAluno.MatriculaAlunoId);

            var mailBody = CoreHelpers.MontarEmailInformacoesProvaOnline(provaAluno, _mapper.Map<DtoAluno>(matricula.Aluno), matricula.Unidade, matricula.Curso.NacionatalTec);

            await _emailSenderService.SendEmailAsync(new string[] { matricula.Aluno.Contato.Email }, "Informações Para Sua Prova On-line", mailBody, null, matricula.Curso.NacionatalTec, null);

            this.EnviarWhatsAppCredencialProvaOnline(matricula, provaAluno);

            return _mapper.Map<DtoProvaAluno>(provaAluno);
        }

        public async Task<DtoProvaAlunoInformacoes> InformacoesCadastro(int matriculaId)
        {
            var ret = new DtoProvaAlunoInformacoes();
            //var matricula = await _matriculaAlunoService.BuscarPorId(matriculaId);
            var matricula = _matriculaAlunoRepository.GetMatriculaProva(matriculaId);

            ret.AlunoRG = matricula.Aluno.RG;

            if (matricula.PlanoPagamentoAluno.PlanoPagamentoId.HasValue)
            {
                var pagamentos = await _alunoFinanceiroContratoRepository.ConsultarPainelFinanceiro(matriculaId);

                if (pagamentos.Any(x =>
                    (x.Descricao.Contains("Parcela - ") && x.TipoSituacao != TipoSituacaoEnum.Pago && x.TipoSituacao != TipoSituacaoEnum.Isento) ||
                    (x.Descricao.Contains("Taxa de Matricula") && x.TipoSituacao != TipoSituacaoEnum.Pago && x.TipoSituacao != TipoSituacaoEnum.Isento) ||
                    (x.Descricao.Contains("Taxa de Inscrição") && x.TipoSituacao != TipoSituacaoEnum.Pago && x.TipoSituacao != TipoSituacaoEnum.Isento)))
                    ret.PendenciaPagamento = true;
                else
                    ret.PendenciaPagamento = false;
            }
            else
                ret.PendenciaPagamento = true;

            var documentosPendentes = await _matriculaAlunoService.ConsultarDocumentosPendentes(_mapper.Map<MatriculaAluno>(matricula));
            var contrato = _anexoRepository.ExisteContrato(matriculaId);

            ret.PendenciaDocumental = documentosPendentes.DocumentosPendentes.Count() > 0 || !contrato;

            if (matricula.DataMatricula < DateTime.Now.AddMonths(11))
                ret.DentroPrazo = true;
            else
                ret.DentroPrazo = false;

            ret.ColegiosAutorizados = await _colegioAutorizadoService.BuscarTodos();

            return ret;
        }

        public IEnumerable<DtoAgendaProva> BuscarProvasDisponiveis(int colegioId, int cursoId, int unidadeId)
        {
            var provas = _agendaProvaRepository.BuscarProvasDisponiveis(colegioId, unidadeId, cursoId);

            var ret = new List<DtoAgendaProva>();

            foreach (var prova in provas)
            {
                var agenda = _mapper.Map<DtoAgendaProva>(prova);
                ret.Add(agenda);
            }

            return ret;
        }

        public IEnumerable<DtoProvaAluno> BuscarProvasRealizadas(int matriculaId)
        {
            var provas = _provaAlunoRepository.BuscarProvasRealizadas(matriculaId);
            return _mapper.Map<IEnumerable<DtoProvaAluno>>(provas);
        }
        public async Task<bool> CancelarInscricao(int provaAlunoId)
        {
            return await _provaAlunoRepository.RemoveAsync(provaAlunoId);
        }

        public async Task<DtoProvaAluno> ImprimirFormulario(int provaAlunoId)
        {
            var provaAluno = await _provaAlunoRepository.GetByIdAsync(provaAlunoId);
            var matricula = await _matriculaAlunoRepository.BuscarPorId(provaAluno.MatriculaAlunoId);

            var endereco = await _enderecoRepository.PorIdEndereco(matricula.Aluno.EnderecoId);
            var nacionalidade = await _nacionalidadeRepository.GetByIdAsync(matricula.Aluno.NacionalidadeId);
            var naturalidade = matricula.Aluno.NaturalidadeId.HasValue ? await _naturalidadeRepository.GetByIdAsync(matricula.Aluno.NaturalidadeId.Value) : null;

            var inscricaoProvaModel = new InscricaoProvaModel()
            {
                NomeCompleto = matricula.Aluno.Nome,
                DataNascimento = matricula.Aluno.DataNascimento.ToString("dd/MM/yyyy"),
                EstadoCivil = matricula.Aluno.EstadoCivil.ToString(),
                Rg = matricula.Aluno.RG,
                OrgaoExpeditor = matricula.Aluno.OrgaoExpedicao,
                UF = "",
                CPF = matricula.Aluno.CPF,
                TituloEleitoral = matricula.Aluno.TituloEleitoral,
                ZonaEleitoral = matricula.Aluno.Zona,
                SecaoEleitoral = matricula.Aluno.Secao,
                Nacionalidade = nacionalidade.Descricao,
                Naturalidade = naturalidade?.Descricao ?? "",
                NomeMae = matricula.Aluno.NomeMae,
                NomeResponsavel = matricula.Aluno.NomeResponsavel,
                Endereco = endereco.Rua,
                EnderecoComplemento = endereco.Complemento,
                EnderecoNumero = endereco.Numero,
                EnderecoBairro = endereco.Bairro,
                EnderecoCidade = endereco.Cidade,
                EnderecoUF = endereco.Estado,
                EnderecoCEP = endereco.CEP
            };

            switch (matricula.Aluno.Sexo)
            {
                case Core.Model.SexoEnum.Masculino:
                    inscricaoProvaModel.Sexo = "M";
                    break;
                case Core.Model.SexoEnum.Feminino:
                    inscricaoProvaModel.Sexo = "F";
                    break;
                default:
                    inscricaoProvaModel.Sexo = "";
                    break;
            }

            var retorno = new DtoProvaAluno();
            retorno.InscricaoProvaDocumento = InscricaoProvaHelper.InscricaoProva(inscricaoProvaModel, matricula.Curso.Descricao);

            return retorno;
        }

        public async Task TicketEnviar(int matriculaId, int UsuarioLogadoId)
        {


            var matricula = await _matriculaAlunoService.BuscarPorId(matriculaId);

            var usuarioLogado = await _usuarioService.BuscarPorId(UsuarioLogadoId);

            var usuarioLista = await _usuarioService.BuscarUsuarioAtendente();

            Dto.UnidadeVO.DtoUnidadeResponse unidade;
            if (usuarioLogado.Unidade != null)
                unidade = await _unidadeService.BuscarPorId(usuarioLogado.Unidade.Id);
            else
                unidade = await _unidadeService.BuscarPorId(matricula.UnidadeId);

            #region criar ticket para envio
            var assunto = _assuntoTicketRepository.BuscarAnaliseDocumentacaoProva();

            var ticket = new Dto.TicketVO.DtoTicket
            {
                DataAbertura = DateTime.Now,
                Status = Core.Model.Tickets.StatusTicketEnum.Aberto,
                AssuntoTicketId = assunto.Id,
                UsuarioLogadoId = UsuarioLogadoId,
                IdFuncionarioAtendente = UsuarioLogadoId,
                MatriculaId = matricula.Id
            };

            ticket.DestinatarioTicket = new Dto.TicketVO.DtoDestinatarioTicket();

            ticket.DestinatarioTicket.UnidadeId = assunto.UnidadeId;

            ticket.DestinatarioTicket.DepartamentoId = assunto.CentroCustoId;

            string descricao = string.Format(@"Análise de documentação para prova do aluno(a): {0} {1} RM: {2}{3} CPF: {4}"
                                         , matricula.Aluno.Nome, Environment.NewLine, matricula.NumeroMatricula, Environment.NewLine
                                         , Core.Helpers.CoreHelpers.FormatCNPJouCPF(matricula.Aluno.CPF));

            ticket.DestinatarioTicket.Mensagem = descricao;
            ticket.DestinatarioTicket.UsuarioLogadoId = UsuarioLogadoId;
            ticket.DestinatarioTicket.StatusTicket = Core.Model.Tickets.StatusTicketEnum.Aberto;
            ticket.DataAbertura = DateTime.Now;
            ticket.NumeroProtocolo = new Random(DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + DateTime.Now.Minute).Next().ToString();

            ticket.DestinatarioTicket.UsuarioDestinarioTicket = new List<DtoUsuarioDestinarioTicket>();
            if (assunto.FuncionarioAssuntoTicket.Count > 0)
            {

                foreach (var item in assunto.FuncionarioAssuntoTicket)
                {
                    var usuario = await _usuarioService.FiltrarUsuario(new Dto.UsuarioVO.DtoFiltrarUsuario { FuncionarioId = item.Usuario.FuncionarioId });

                    ticket.DestinatarioTicket.UsuarioDestinarioTicket.Add(new Dto.TicketVO.DtoUsuarioDestinarioTicket
                    {
                        FuncionarioId = usuario.FirstOrDefault().Id
                    });
                }
            }
            else
            {
                var usuarios = await _usuarioService.FiltrarUsuario(new DtoFiltrarUsuario() { CentroCustoId = assunto.CentroCustoId, UnidadeId = assunto.UnidadeId });
                foreach (var item in usuarios)
                {
                    if (item.Funcionario == null) break;
                    var usuario = await _usuarioService.FiltrarUsuario(new Dto.UsuarioVO.DtoFiltrarUsuario { FuncionarioId = item.Funcionario.Id });

                    ticket.DestinatarioTicket.UsuarioDestinarioTicket.Add(new Dto.TicketVO.DtoUsuarioDestinarioTicket
                    {
                        FuncionarioId = usuario.FirstOrDefault().Id
                    });
                }

            }

            await _ticketService.Inserir(ticket);

            #endregion
        }



        private async void EnviarWhatsAppIncrição(DtoProvaAlunoRequest dtoProvaAluno, MatriculaAluno matricula)
        {
            string mensagemWhatsApp = string.Empty;

            switch (dtoProvaAluno.TipoProva)
            {
                case TipoProvaEnum.Presencial:
                    switch (dtoProvaAluno.TipoTransporte)
                    {
                        case TipoTransporteEnum.Escola:
                            mensagemWhatsApp = await TextoWhatsApp(dtoProvaAluno, matricula, TipoMensagemProvaEnum.PresencialTransporteEscola);
                            break;
                        case TipoTransporteEnum.Particular:
                            mensagemWhatsApp = await TextoWhatsApp(dtoProvaAluno, matricula, TipoMensagemProvaEnum.PresencialTransporteProprio);
                            break;
                    }

                    break;
                case TipoProvaEnum.Online:
                    mensagemWhatsApp = await TextoWhatsApp(dtoProvaAluno, matricula, TipoMensagemProvaEnum.Online);
                    break;
            }

            string retorno = await _whatsAppService.SendMessage(matricula.Unidade.Contato.Token, matricula.Aluno.Contato.Celular, mensagemWhatsApp, matricula.Aluno.Id); // teste salvar aluno na tabela historico regua contato 
        }

        private async Task<string> TextoWhatsApp(DtoProvaAlunoRequest dtoProvaAluno, MatriculaAluno matricula, TipoMensagemProvaEnum tipoMensagem)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("*Olá, {0}*", matricula.Aluno.Nome.Trim());
            stringBuilder.AppendLine();
            stringBuilder.AppendLine();
            stringBuilder.AppendFormat("Sua inscrição para prova em colégio autorizado foi realizada com sucesso.");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine();
            switch (tipoMensagem)
            {
                case TipoMensagemProvaEnum.PresencialTransporteEscola:
                    string horarioSaida = string.Format("{0}:{1}h"
                       , dtoProvaAluno?.UnidadeTransporteProva?.UnidadeParticipanteProva?.HoraSaida.Substring(0, 2)
                       , dtoProvaAluno?.UnidadeTransporteProva?.UnidadeParticipanteProva?.HoraSaida.Substring(2, 2));

                    stringBuilder.AppendFormat("*A data da sua prova é:* {0}", dtoProvaAluno.DataProva?.ToString("dd/MM/yy"));
                    stringBuilder.AppendLine();
                    stringBuilder.AppendFormat("*O local de saída do seu ônibus é:* {0}", dtoProvaAluno?.UnidadeTransporteProva?.UnidadeParticipanteProva?.LocalSaida);
                    stringBuilder.AppendLine();
                    stringBuilder.AppendFormat("*O número do seu Ônibus é:* {0}", dtoProvaAluno?.UnidadeTransporteProva?.NumeroOnibus);
                    stringBuilder.AppendLine();
                    stringBuilder.AppendFormat("*Horário de embarque às:* {0}", horarioSaida);
                    stringBuilder.AppendLine();
                    stringBuilder.AppendLine();
                    stringBuilder.AppendFormat("_*Comparecer no local de embarque com 15 minutos de antecedência.*_");
                    stringBuilder.AppendLine();
                    stringBuilder.AppendLine();
                    stringBuilder.AppendFormat("*Lembre - se de levar:*");
                    stringBuilder.AppendLine();
                    stringBuilder.AppendFormat("• Lápis;");
                    stringBuilder.AppendLine();
                    stringBuilder.AppendFormat("• Borracha;");
                    stringBuilder.AppendLine();
                    stringBuilder.AppendFormat("• Caneta Azul;");
                    stringBuilder.AppendLine();
                    stringBuilder.AppendFormat("• Documento com foto.");
                    stringBuilder.AppendLine();
                    stringBuilder.AppendLine();
                    stringBuilder.AppendFormat("*Importante:*");
                    stringBuilder.AppendLine();
                    stringBuilder.AppendFormat("• Celular deve ser mantido desligado durante a prova;");
                    stringBuilder.AppendLine();
                    stringBuilder.AppendFormat("• A alimentação é por conta do aluno;");
                    stringBuilder.AppendLine();
                    stringBuilder.AppendFormat("• Não faltar na prova.");
                    break;
                case TipoMensagemProvaEnum.PresencialTransporteProprio:
                    var colegioAutorizado = await _colegioAutorizadoService.BuscarPorId(dtoProvaAluno.ColegioAutorizadoId);

                    stringBuilder.AppendFormat("*A data da sua prova é:* {0}", dtoProvaAluno.DataProva?.ToString("dd/MM/yy"));
                    stringBuilder.AppendLine();
                    stringBuilder.AppendLine();
                    stringBuilder.AppendFormat("*O endereço para realizar sua prova é:* {0}, {1}, {2}, {3}, {4}, {5}", colegioAutorizado.Endereco.Rua,
                                                                                                                      colegioAutorizado.Endereco.Numero,
                                                                                                                      colegioAutorizado.Endereco.Bairro,
                                                                                                                      colegioAutorizado.Endereco.Cidade,
                                                                                                                      colegioAutorizado.Endereco.Estado,
                                                                                                                      colegioAutorizado.Endereco.CEP);
                    stringBuilder.AppendLine();
                    stringBuilder.AppendLine();
                    stringBuilder.AppendFormat("*Lembre - se de levar:*");
                    stringBuilder.AppendLine();
                    stringBuilder.AppendFormat("• Lápis");
                    stringBuilder.AppendLine();
                    stringBuilder.AppendFormat("• Borracha");
                    stringBuilder.AppendLine();
                    stringBuilder.AppendFormat("• Caneta Azul");
                    stringBuilder.AppendLine();
                    stringBuilder.AppendFormat("• Documento com foto");
                    stringBuilder.AppendLine();
                    stringBuilder.AppendLine();
                    stringBuilder.AppendFormat("*Importante:*");
                    stringBuilder.AppendLine();
                    stringBuilder.AppendFormat("• Celular deve ser mantido desligado durante a prova");
                    stringBuilder.AppendLine();
                    stringBuilder.AppendFormat("• A alimentação é por conta do aluno");
                    stringBuilder.AppendLine();
                    stringBuilder.AppendFormat("• Não faltar na prova");
                    break;
                case TipoMensagemProvaEnum.Online:
                    stringBuilder.AppendFormat("Em até 45 dias úteis você receberá todas as informações necessárias para acessar sua prova.");
                    break;
                default:
                    break;
            }

            stringBuilder.AppendLine();
            stringBuilder.AppendLine();
            stringBuilder.AppendFormat("_*{0} {1}*_", matricula.Curso.NacionatalTec ? "Cursos" : "Supletivo", matricula.Unidade.NomeFantasia);

            return stringBuilder.ToString();
        }

        private async void EnviarWhatsAppCredencialProvaOnline(MatriculaAluno matricula, ProvaAluno provaAluno)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("*Olá, {0}*", matricula.Aluno.Nome.Trim());
            stringBuilder.AppendLine();
            stringBuilder.AppendLine();
            stringBuilder.AppendFormat("*Sua prova on-line já está disponível! Leia com atenção, caso contrário poderá ser reprovado.*");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine();
            if (matricula.Unidade.Id == 10)
            {
                stringBuilder.AppendFormat("• A partir de agora você tem 15 dias para concluir *cada etapa.*");
                stringBuilder.AppendLine();
                stringBuilder.AppendFormat("• Iniciando a prova você terá até *1h30* para finalizar cada disciplina.");
                stringBuilder.AppendLine();
                stringBuilder.AppendFormat("• Você pode realizar a prova on-line *apenas 2 vezes.* Caso ocorra uma 2ª reprova NÃO terá uma 3ª prova on - line, apenas presencial.");
                stringBuilder.AppendLine();
                stringBuilder.AppendFormat("• Caso ocorra reprova, na primeira tentativa do Exame de Avaliação Final, será liberado na hora uma nova chance.");
                stringBuilder.AppendLine();
                stringBuilder.AppendLine();
                stringBuilder.AppendFormat("*Segue o passo a passo para acessar sua prova:*");
                stringBuilder.AppendLine();
                stringBuilder.AppendLine();
                stringBuilder.AppendFormat("1.Acesse o Portal do Aluno com *Login* (seu cpf) e sua *Senha* (4 primeiros dígitos do seu cpf).");
                stringBuilder.AppendLine();
                stringBuilder.AppendFormat("2.Clique no botão EAD");
                stringBuilder.AppendLine();
                stringBuilder.AppendFormat("3.Assista o vídeo que consta na página e em seguida clique no botão laranja *'Instituições autorizadas'*.");
            }
            else
            {
                stringBuilder.AppendFormat("*A prova está dividida em duas etapas:*");
                stringBuilder.AppendLine();
                stringBuilder.AppendLine();
                stringBuilder.AppendFormat("1. *Exames de Reclassificação*");
                stringBuilder.AppendLine();
                stringBuilder.AppendFormat("2. *Exames de Certificação*");
                stringBuilder.AppendLine();
                stringBuilder.AppendLine();
                stringBuilder.AppendLine();
                stringBuilder.AppendFormat("• A partir de agora você tem 15 dias para concluir *cada etapa.*");
                stringBuilder.AppendLine();
                stringBuilder.AppendFormat("• Sendo aprovado em todos os exames de reclassificação, *após 10 dias úteis* os exames certificadores serão liberados no *sistema automaticamente.*");
                stringBuilder.AppendLine();
                stringBuilder.AppendFormat("• Iniciando a prova você terá até 2 horas para finalizar cada disciplina.");
                stringBuilder.AppendLine();
                stringBuilder.AppendFormat("• A redação deve ser feita de forma on-line. *Não copie textos da internet,* pois caso ocorra será reprovado.");
                stringBuilder.AppendLine();
                stringBuilder.AppendFormat("• Você pode realizar a prova on-line *apenas 2 vezes.* Caso ocorra uma 2ª reprova NÃO terá uma 3ª prova on - line, apenas presencial.");
                stringBuilder.AppendLine();
                stringBuilder.AppendLine();
                stringBuilder.AppendLine();
                stringBuilder.AppendFormat("*Segue o passo a passo para acessar sua prova:*");
                stringBuilder.AppendLine();
                stringBuilder.AppendLine();
                stringBuilder.AppendFormat("1.Acesse o Portal do Aluno com *Login* (seu cpf) e sua *Senha* (4 primeiros dígitos do seu cpf).");
                stringBuilder.AppendLine();
                stringBuilder.AppendFormat("2.Clique no botão EJA – ENCCEJA.");
                stringBuilder.AppendLine();
                stringBuilder.AppendFormat(@"3.Assista o vídeo que consta na página e em seguida clique no botão laranja *""EJA - Instituições autorizadas""*.");
            }
            stringBuilder.AppendLine();
            stringBuilder.AppendFormat("4. *Identificação do usuário:* {0} *Senha:* {1}", provaAluno.IdentificacaoUsuario, provaAluno.SenhaProva);
            stringBuilder.AppendLine();
            stringBuilder.AppendLine();
            stringBuilder.AppendFormat("*Boa prova!* Esta mensagem é automática, dúvidas entre em contato através do telefone: {0}", matricula.Unidade.Contato.TelefoneFixoPrincipal);
            stringBuilder.AppendLine();
            stringBuilder.AppendLine();
            stringBuilder.AppendFormat("_*{0} {1}*_", matricula.Curso.NacionatalTec ? "Cursos" : "Supletivo", matricula.Unidade.NomeFantasia);

            string retorno = await _whatsAppService.SendMessage(matricula.Unidade.Contato.Token, matricula.Aluno.Contato.Celular, stringBuilder.ToString(), matricula.Aluno.Id); // teste salvar aluno na tabela historico regua contato 
        }
    }
}
