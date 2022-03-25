using EscolaPro.Core.Model;
using EscolaPro.Core.Model.Atendimentos;
using EscolaPro.Core.Model.AulasOnline;
using EscolaPro.Core.Model.BolsaConvenio;
using EscolaPro.Core.Model.ContasPagar;
using EscolaPro.Core.Model.ControlePontoEletronico;
using EscolaPro.Core.Model.DadosFuncionario;
using EscolaPro.Core.Model.EstoqueProdutos;
using EscolaPro.Core.Model.FolhaPagamentos;
using EscolaPro.Core.Model.Fornecedores;
using EscolaPro.Core.Model.MetasComissoes;
using EscolaPro.Core.Model.Pagamentos;
using EscolaPro.Core.Model.PainelMatricula;
using EscolaPro.Core.Model.Provas;
using EscolaPro.Core.Model.ReguaContato;
using EscolaPro.Core.Model.Solicitacoes;
using EscolaPro.Core.Model.Tickets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EscolaPro.Repository
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Contato> Contato { get; set; }
        public DbSet<Unidade> Unidade { get; set; }
        public DbSet<UnidadeDespesa> UnidadeDespesa { get; set; }
        public DbSet<DadosBancario> DadosBancario { get; set; }
        public DbSet<CentroCusto> CentroCusto { get; set; }
        public DbSet<Anexo> Anexo { get; set; }
        public DbSet<HorarioFuncionamento> HorarioFuncionamento { get; set; }
        public DbSet<HistoricoOcorrencias> HistoricoOcorrencias { get; set; }
        public DbSet<ContratoLocacao> ContratoLocacao { get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<Turma> Turma { get; set; }
        public DbSet<PlanoPagamento> PlanoPagamento { get; set; }
        public DbSet<InstituicaoBancaria> InstituicaoBancaria { get; set; }
        public DbSet<Funcionario> Funcionario { get; set; }
        public DbSet<Campanha> Campanha { get; set; }
        public DbSet<CampanhaTipoPagamento> CampanhaTipoPagamento { get; set; }
        public DbSet<Parametro> Parametro { get; set; }
        public DbSet<CursoProfessor> CursoProfessor { get; set; }
        public DbSet<MateriaCursoProfessor> MateriaCursoProfessor { get; set; }
        public DbSet<PontoEletronico> PontoEletronico { get; set; }
        public DbSet<ArquivoPonto> ArquivoPonto { get; set; }
        /// <summary>
        /// Ticket
        /// </summary>
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Ocorrencia> Ocorrencia { get; set; }
        public DbSet<AssuntoTicket> AssuntoTicket { get; set; }
        public DbSet<FuncionarioAssuntoTicket> FuncionarioAssuntoTicket { get; set; }
        public DbSet<DestinatarioTicket> DestinatarioTicket { get; set; }
        public DbSet<MensagemTicket> MensagemTicket { get; set; }

        /// <summary>
        /// Metas e Comissoes
        /// </summary>
        public DbSet<Meta> Meta { get; set; }
        public DbSet<DetalhamentoMeta> MetaPeriodo { get; set; }
        public DbSet<MetaUnidade> MetaUnidade { get; set; }
        public DbSet<Comissoes> Comissoes { get; set; }
        public DbSet<ComissaoUnidade> ComissaoUnidade { get; set; }

        /// <summary>
        /// Fornecedores
        /// </summary>
        public DbSet<Fornecedor> Fornecedor { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<ItemProduto> ItemProduto { get; set; }
        public DbSet<HistoricoEstoque> HistoricoEstoque { get; set; }

        /// <summary>
        /// Folha de Pagamentos
        /// </summary>
        public DbSet<FolhaPagamento> FolhaPagamento { get; set; }
        public DbSet<HoraExtra> HoraExtra { get; set; }

        /// <summary>
        /// Contas a pagar
        /// </summary>
        public DbSet<Despesa> Despesa { get; set; }
        public DbSet<DespesaParcela> DespesaParcela { get; set; }

        /// <summary>
        /// Aluno e Matriculas
        /// </summary>
        public DbSet<Aluno> Aluno { get; set; }
        public DbSet<MatriculaAluno> MatriculaAluno { get; set; }
        public DbSet<CancelamentoMatricula> CancelamentoMatricula { get; set; }
        public DbSet<CancelamentoIsencao> CancelamentoIsencao { get; set; }
        public DbSet<CancelamentoIsencaoPagamento> CancelamentoIsencaoPagamento { get; set; }
        /// <summary>
        /// Agenda de Provas
        /// </summary>
        public DbSet<AgendaProva> AgendaProva { get; set; }
        public DbSet<UnidadeParticipanteProva> UnidadeParticipanteProva { get; set; }
        public DbSet<UnidadeTransporteProva> UnidadeTransporteProva { get; set; }

        /// <summary>
        /// Criação de AulasOnline
        /// </summary>
        public DbSet<AulaOnline> AulaOnline { get; set; }
        public DbSet<VideoAula> VideoAula { get; set; }
        public DbSet<VideoPausado> VideoPausado { get; set; }
        public DbSet<ProvaMateriaAluno> ProvaMateriaAluno { get; set; }
        /// <summary>
        /// Solicitação
        /// </summary>
        public DbSet<Solicitacao> Solicitacao { get; set; }

        /// <summary>
        /// Financeiro Aluno
        /// </summary>
        public DbSet<Pagamento> Pagamento { get; set; }

        /// <summary>
        /// Regua Contato
        /// </summary>
        public DbSet<ReguaContatoRegra> ReguaContatoRegras { get; set; }
        public DbSet<ReguaContatoFila> ReguaContatoFila { get; set; }
        public DbSet<ReguaContatoHistorico> ReguaContatoHistorico { get; set; }
        public DbSet<Atendimento> Atendimento { get; set; }
        public DbSet<AtendimentoOutbound> AtendimentoOutbound { get; set; }
        public DbSet<ReguaContatoParametro> ReguaContatoParametro { get; set; }
        public DbSet<AtendimentoAgendamento> AtendimentoAgendamento { get; set; }
        public DbSet<Leads> Leads { get; set; }

        /* Creating DatabaseContext without Dependency Injection */
        public ApplicationContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlServer(DatabaseConnection.ConnectionConfiguration
                                                    .GetConnectionString("DefaultConnection"));
            }

            dbContextOptionsBuilder.EnableSensitiveDataLogging();
        }

        /* Creating DatabaseContext configured outside with Dependency Injection */
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Aluno>(entity =>
            //{
            //    entity.HasOne(d => d.Endereco)
            //        .WithMany()
            //        .HasForeignKey(d => d.IdEndereco);

            //    entity.HasOne(d => d.Contato)
            //      .WithMany()
            //      .HasForeignKey(d => d.IdContato);
            //});

            //modelBuilder.Entity<Turma>(entity =>
            //{
            //    entity
            //    .Ignore(x => x.TurmaCurso)
            //    .Ignore(x => x.Curso);
            //});

            modelBuilder.Entity<TurmaUnidade>().HasKey(x => new { x.TurmaId, x.UnidadeId });

            modelBuilder.Entity<TurmaCurso>().HasKey(x => new { x.TurmaId, x.CursoId });

            modelBuilder.Entity<PlanoPagamentoCurso>().HasKey(x => new { x.PlanoPagamentoId, x.CursoId });

            modelBuilder.Entity<PlanoPagamentoUnidade>().HasKey(x => new { x.PlanoPagamentoId, x.UnidadeId });

            modelBuilder.Entity<CampanhaUnidade>().HasKey(x => new { x.CampanhaId, x.UnidadeId });

            //modelBuilder.Entity<CampanhaCurso>().HasKey(x => new { x.CampanhaId, x.CursoId });

            modelBuilder.Entity<SalarioUnidade>().HasKey(x => new { x.FuncionarioId, x.UnidadeId });           
        }
    }
}
