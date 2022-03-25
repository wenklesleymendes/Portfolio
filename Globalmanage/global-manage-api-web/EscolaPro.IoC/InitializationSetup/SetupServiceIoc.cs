using EscolaPro.ControleBoleto;
using EscolaPro.ControlePonto;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Repository;
using EscolaPro.Service;
using EscolaPro.Service.Concretes;
using EscolaPro.Service.Concretes.AgendaProvas;
using EscolaPro.Service.Concretes.Atendimentos;
using EscolaPro.Service.Concretes.AulasOnlineVimeo;
using EscolaPro.Service.Concretes.Documentos;
using EscolaPro.Service.Concretes.MatriculaAlunos;
using EscolaPro.Service.Concretes.PortalAluno;
using EscolaPro.Service.Concretes.ProvasCertificados;
using EscolaPro.Service.Concretes.ReguaContato;
using EscolaPro.Service.Interfaces;
using EscolaPro.Service.Interfaces.AgendaProvas;
using EscolaPro.Service.Interfaces.Atendimentos;
using EscolaPro.Service.Interfaces.AulasOnlineVimeo;
using EscolaPro.Service.Interfaces.Documentos;
using EscolaPro.Service.Interfaces.MatriculaAlunos;
using EscolaPro.Service.Interfaces.PortalAluno;
using EscolaPro.Service.Interfaces.ProvasCertificados;
using EscolaPro.Service.Interfaces.ReguaContato;
using Microsoft.Extensions.DependencyInjection;

namespace EscolaPro.IoC
{
    public class SetupServiceIoc
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Unidades
            services.AddTransient<IUnidadeService, UnidadeService>();

            // Centro de Custo
            services.AddScoped<ICentroCustoService, CentroCustoService>();

            // Anexo
            services.AddScoped<IAnexoService, AnexoService>();
            services.AddScoped<IInconsistenciaDocumentoService, InconsistenciaDocumentoService>();

            //services.AddScoped<IAnexoRepository, AnexoRepository>();

            // Cursos e Turmas
            services.AddScoped<ICursoService, CursoService>();
            services.AddScoped<ITurmaService, TurmaService>();

            // Plano de Pagamento
            services.AddScoped<IPlanoPagamentoService, PlanoPagamentoService>();
            services.AddScoped<IPlanoPagamentoCursoRepository, PlanoPagamentoCursoRepository>();

            // Banco
            services.AddScoped<IInstituicaoBancariaService, InstituicaoBancariaService>();

            // Campanha
            services.AddScoped<ICampanhaService, CampanhaService>();

            // Funcionario
            services.AddScoped<IFuncionarioService, FuncionarioService>();

            // Controle de Ponto
            services.AddScoped<IControlePontoService, ControlePontoService>();
            services.AddScoped<IPontoEletronicoRobo, PontoEletronicoRobo>();

            // Ferias Funcionario
            services.AddScoped<IFeriasFuncionarioService, FeriasFuncionarioService>();

            // Usuario e Perfil
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IPerfilUsuarioService, PerfilUsuarioService>();

            // Professor e MateriaProfessor
            services.AddScoped<IMateriaRepository, MateriaRepository>();

            // Tickets
            services.AddScoped<IAssuntoTicketService, AssuntoTicketService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IOcorrenciaService, OcorrenciaService>();

            // Metas e Comissões
            services.AddScoped<IMetasService, MetasService>();
            services.AddScoped<IComissoesService, ComissoesService>();

            // Fornecedor
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<ICategoriaService, CategoriaService>();

            // Estoque
            services.AddScoped<IEstoqueService, EstoqueService>();
            services.AddScoped<IEstoqueHistoricoService, EstoqueHistoricoService>();

            // Folha de Pagamento
            services.AddScoped<IFolhaPagamentoService, FolhaPagamentoService>();

            // Agenda de Provas
            services.AddScoped<IAgendaProvaService, AgendaProvaService>();
            services.AddScoped<IColegioAutorizadoService, ColegioAutorizadoService>();
            services.AddScoped<IUnidadeTransporteProvaService, UnidadeTransporteProvaService>();

            // Alunos
            services.AddScoped<IAlunoService, AlunoService>();

            // Registro de Cobranças e Pagamentos
            services.AddScoped<IRegistroCobrancaService, RegistroCobrancaService>();
            services.AddScoped<IBoletoManager, BoletoManager>();
            services.AddScoped<IAuthItauService, AuthItauService>();

            //Contas a pagar 
            services.AddScoped<IContasPagarService, ContasPagarService>();

            //Criação de Aulas Online
            services.AddScoped<IAulaOnlineService, AulaOnlineService>();
            services.AddScoped<IVideoAulaService, VideoAulaService>();
            services.AddScoped<IRespostaService, RespostaService>();
            services.AddScoped<IPerguntaService, PerguntaService>();
            services.AddScoped<IAlunoQuestionarioService, AlunoQuestionarioService>();

            //Enviar E-mail, WhatsApp e SMS
            services.AddScoped<IEmailSenderService, EmailSenderService>();
            services.AddScoped<IDisparoSmsService, DisparoSmsService>();
            services.AddScoped<IWhatsAppService, WhatsAppService>();

            // Matricula Aluno
            services.AddScoped<IMatriculaAlunoService, MatriculaAlunoService>();
            services.AddScoped<INacionalidadeService, NacionalidadeService>();
            services.AddScoped<INaturalidadeService, NaturalidadeService>();
            services.AddScoped<ICancelamentoMatriculaService, CancelamentoMatriculaService>();

            // Solicitações
            services.AddScoped<ISolicitacaoService, SolicitacaoService>();
            services.AddScoped<ISolicitacaoAlunoService, SolicitacaoAlunoService>();

            // Aulas On-line
            services.AddScoped<IMateriaOnlineService, MateriaOnlineService>();

            // Chat Aluno e Professor
            services.AddScoped<IMensagemAlunoProfessorService, MensagemAlunoProfessorService>();

            //Painel de financeiro aluno
            services.AddScoped<IAlunoFinanceiroContratoService, AlunoFinanceiroContratoService>();

            // Provas
            services.AddScoped<IProvaAlunoService, ProvaAlunoService>();
            services.AddScoped<IProvaMateriaAlunoService, ProvaMateriaAlunoService>();
            services.AddScoped<ICertificadoProvaService, CertificadoProvaService>();

            services.AddScoped<IHistoricoProvasService, HistoricoProvasService>();

            services.AddScoped<IReguaContatoHistoricoService, ReguaContatoHistoricoService>();
            services.AddScoped<IReguaContatoService, ReguaContatoService>();

            // Atendimento
            services.AddScoped<IAtendimentoService, AtendimentoService>();
            services.AddScoped<IAtendimentoOutboundService, AtendimentoOutboundService>();
            services.AddScoped<IAtendimentoAgendamentoService, AtendimentoAgendamentoService>();
            services.AddScoped<ILeadsService, LeadsService>();
            //services.AddScoped<IAtendimentoScoreService, AtendimentoScoreService>();

            // Contatos
            services.AddScoped<IContatosService, ContatosService>();


            //ApostilaOnline

            services.AddScoped<IApostilaOnlineRepository, ApostilaOnlineRepository>();
            services.AddScoped<IApostilaOnlineService, ApostilaOnlineService>();



        }
    }
}
