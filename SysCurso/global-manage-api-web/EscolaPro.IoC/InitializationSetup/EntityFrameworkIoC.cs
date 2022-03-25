using EscolaPro.Repository;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Interfaces.AgendaProvas;
using EscolaPro.Repository.Interfaces.Atendimentos;
using EscolaPro.Repository.Interfaces.AulasOnline;
using EscolaPro.Repository.Interfaces.CadastroAluno;
using EscolaPro.Repository.Interfaces.ContasAPagar;
using EscolaPro.Repository.Interfaces.EmailEnviados;
using EscolaPro.Repository.Interfaces.EstoqueProdutos;
using EscolaPro.Repository.Interfaces.FolhaPagamentos;
using EscolaPro.Repository.Interfaces.Fornecedores;
using EscolaPro.Repository.Interfaces.MatriculaAlunos;
using EscolaPro.Repository.Interfaces.MetasComissoes;
using EscolaPro.Repository.Interfaces.Pagamentos;
using EscolaPro.Repository.Interfaces.PortalAlunoProfessor;
using EscolaPro.Repository.Interfaces.ReguaContato;
using EscolaPro.Repository.Interfaces.Solicitacoes;
using EscolaPro.Repository.Interfaces.Tickets;
using EscolaPro.Repository.Repository;
using EscolaPro.Repository.Repository.AgendaProvas;
using EscolaPro.Repository.Repository.Atendimentos;
using EscolaPro.Repository.Repository.AulasOnline;
using EscolaPro.Repository.Repository.CadastroAluno;
using EscolaPro.Repository.Repository.ContasAPagar;
using EscolaPro.Repository.Repository.EmailEnviados;
using EscolaPro.Repository.Repository.EstoqueProdutos;
using EscolaPro.Repository.Repository.FolhaPagamentos;
using EscolaPro.Repository.Repository.Fornecedores;
using EscolaPro.Repository.Repository.MatriculaAlunos;
using EscolaPro.Repository.Repository.MetasComissoes;
using EscolaPro.Repository.Repository.Pagamentos;
using EscolaPro.Repository.Repository.PortalAlunoProfessor;
using EscolaPro.Repository.Repository.ReguaContato;
using EscolaPro.Repository.Repository.Solicitacoes;
using EscolaPro.Repository.Repository.Tickets;
using EscolaPro.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EscolaPro.IoC
{
    public class EntityFrameworkIoC : OrmTypes
    {
        internal override IServiceCollection AddOrm(IServiceCollection services, IConfiguration configuration = null)
        {
            string conn = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(conn));

            services.AddScoped(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
            services.AddScoped<IUnidadeRepository, UnidadeRepository>();
            services.AddScoped<IUnidadeDespesaRepository, UnidadeDespesaRepository>();

            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IHistoricoOcorrenciasRepository, HistoricoOcorrenciasRepository>();
            services.AddScoped<IContatoRepository, ContatoRepository>();
            services.AddScoped<IDadosBancarioRepository, DadosBancarioRepository>();
            services.AddScoped<IHistoricoOcorrenciasRepository, HistoricoOcorrenciasRepository>();
            services.AddScoped<IContratoLocacaoRepository, ContratoLocacaoRepository>();
            services.AddScoped<IHorarioFuncionamentoRepository, HorarioFuncionamentoRepository>();

            services.AddScoped<ICentroCustoRepository, CentroCustoRepository>();

            services.AddScoped<IAnexoRepository, AnexoRepository>();

            services.AddScoped<ICursoRepository, CursoRepository>();

            services.AddScoped<ITurmaRepository, TurmaRepository>();
            services.AddScoped<IUnidadeTurmaRepository, UnidadeTurmaRepository>();
            services.AddScoped<ITurmaCursoRepository, TurmaCursoRepository>();

            services.AddScoped<IPlanoPagamentoRepository, PlanoPagamentoRepository>();

            services.AddScoped<IPlanoPagamentoUnidadeRepository, PlanoPagamentoUnidadeRepository>();

            services.AddScoped<IInstituicaoBancariaRepository, InstituicaoBancariaRepository>();

            services.AddScoped<ICampanhaRepository, CampanhaRepository>();

            services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();

            services.AddScoped<IControlePontoRepository, ControlePontoRepository>();

            services.AddScoped<IArquivoPontoRepository, ArquivoPontoRepository>();

            services.AddScoped<IFeriasFuncionarioRepository, FeriasFuncionarioRepository>();

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            services.AddScoped<IPerfilUsuarioRepository, PerfilUsuarioRepository>();

            services.AddScoped<ICursoProfessorRepository, CursoProfessorRepository>();

            services.AddScoped<IMateriaCursoProfessorRepository, MateriaCursoProfessorRepository>();

            services.AddScoped<IAssuntoTicketRepository, AssuntoTicketRepository>();

            services.AddScoped<IFuncionarioAssuntoTicketRepository, FuncionarioAssuntoTicketRepository>();

            services.AddScoped<ITicketRepository, TicketRepository>();

            services.AddScoped<IOcorrenciaRepository, OcorrenciaRepository>();

            services.AddScoped<IMensagemTicketRepository, MensagemTicketRepository>();

            services.AddScoped<IMetasRepository, MetasRepository>();

            services.AddScoped<IComissoesRepository, ComissoesRepository>();

            services.AddScoped<IFornecedorRepository, FornecedorRepository>();

            services.AddScoped<ICategoriaRepository, CategoriaRepository>();

            services.AddScoped<IEstoqueRepository, EstoqueRepository>();

            services.AddScoped<IFolhaPagamentoRepository, FolhaPagamentoRepository>();

            services.AddScoped<IAgendaProvaRepository, AgendaProvaRepository>();

            services.AddScoped<IAlunoRepository, AlunoRepository>();

            services.AddScoped<IEstoqueHistoricoRepository, EstoqueHistoricoRepository>();
            
            services.AddScoped<IItemProdutoEstoqueRepository, ItemProdutoEstoqueRepository>();

            services.AddScoped<IContasPagarRepository, ContasPagarRepository>();

            services.AddScoped<IDespesaParcelaRepository, DespesaParcelaRepository>();

            // Aulas On-line
            services.AddScoped<IVideoAulaRepository, VideoAulaRepository>();
            services.AddScoped<IAulaOnlineRepository, AulaOnlineRepository>();
            services.AddScoped<IMateriaOnlineRepository, MateriaOnlineRepository>();
            services.AddScoped<IRespostaRepository, RespostaRepository>();
            services.AddScoped<IPerguntaRepository, PerguntaRepository>();
            services.AddScoped<IAlunoQuestionarioRepository, AlunoQuestionarioRepository>();

            // Matricula Aluno
            services.AddScoped<IMatriculaAlunoRepository, MatriculaAlunoRepository>();

            services.AddScoped<INacionalidadeRepository, NacionalidadeRepository>();

            services.AddScoped<INaturalidadeRepository, NaturalidadeRepository>();

            services.AddScoped<ICancelamentoMatriculaRepository, CancelamentoMatriculaRepository>();

            services.AddScoped<ICancelamentoIsencaoRepository, CancelamentoIsencaoRepository>();

            services.AddScoped<ICancelamentoIsencaoPagamentoRepository, CancelamentoIsencaoPagamentoRepository>();

            services.AddScoped<ISolicitacaoRepository, SolicitacaoRepository>();

            services.AddScoped<ISolicitacaoAlunoRepository, SolicitacaoAlunoRepository>();

            services.AddScoped<IUsuarioDestinarioTicketRepository, UsuarioDestinarioTicketRepository>();

            services.AddScoped<IColegioAutorizadoRepository, ColegioAutorizadoRepository>();

            services.AddScoped<IMensagemAlunoProfessorRepository, MensagemAlunoProfessorRepository>();

            services.AddScoped<IInconsistenciaDocumentoRepository, InconsistenciaDocumentoRepository>();
            // Financeiro Aluno
            services.AddScoped<IAlunoFinanceiroContratoRepository, AlunoFinanceiroContratoRepository>();

            // E-mail
            services.AddScoped<IEmailEnviadoRepository, EmailEnviadoRepository>();

            // Provas
            services.AddScoped<IProvaAlunoRepository, ProvaAlunoRepository>();
            services.AddScoped<IUnidadeTransporteProvaRepository, UnidadeTransporteProvaRepository>();
            services.AddScoped<IProvaMateriaAlunoRepository, ProvaMateriaAlunoRepository>();
            services.AddScoped<ICertificadoProvaRepository, CertificadoProvaRepository>();

            services.AddScoped<IHistoricoProvasRepository, HistoricoProvasRepository>();

            services.AddScoped<IReguaContatoHistoricoRepository, ReguaContatoHistoricoRepository>();
            services.AddScoped<IReguaContatoFilaRepository, ReguaContatoFilaRepository>();
            services.AddScoped<IReguaContatoRegraRepository, ReguaContatoRegraRepository>();
            services.AddScoped<IReguaContatoParametroRepository, ReguaContatoParametroRepository>();

            // Atendimentos
            services.AddScoped<IAtendimentoRepository, AtendimentoRepository>();
            services.AddScoped<IAtendimentoOutboundRepository, AtendimentoOutboundRepository>();
            services.AddScoped<IAtendimentoAgendamentoRepository, AtendimentoAgendamentoRepository>();
            services.AddScoped<ILeadsRepository, LeadsRepository>();

            return services;
        }
    }
}
