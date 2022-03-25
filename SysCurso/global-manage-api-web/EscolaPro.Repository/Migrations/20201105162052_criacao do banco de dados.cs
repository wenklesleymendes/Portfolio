using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class criacaodobancodedados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgenteIntegracao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Site = table.Column<string>(nullable: true),
                    PessoaContato = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgenteIntegracao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArquivoPonto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    NomeArquivo = table.Column<string>(nullable: true),
                    DataCadastro = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArquivoPonto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssuntoTicket",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    TempoEmDias = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssuntoTicket", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AulaOnline",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    NomeAulaOnline = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AulaOnline", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Campanha",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    NomeCampanha = table.Column<string>(nullable: true),
                    CodigoPromocao = table.Column<string>(nullable: true),
                    Parcela = table.Column<int>(nullable: false),
                    DescontoPlanoPagamento = table.Column<double>(nullable: false),
                    DescontoTaxaMatricula = table.Column<double>(nullable: false),
                    DescontoTaxaMateriaDidatico = table.Column<double>(nullable: false),
                    DescontoTaxaInscricaoProvas = table.Column<double>(nullable: false),
                    ExigeComprovante = table.Column<bool>(nullable: false),
                    CursoId = table.Column<int>(nullable: true),
                    DescricaoCurso = table.Column<string>(nullable: true),
                    InicioCampanha = table.Column<DateTime>(nullable: false),
                    TerminoCampanha = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campanha", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComissaoUnidade",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    UnidadeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComissaoUnidade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comissoes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    TipoPagamento = table.Column<int>(nullable: false),
                    TipoComissao = table.Column<bool>(nullable: false),
                    DataInicioVirgencia = table.Column<DateTime>(nullable: true),
                    DataFimVirgencia = table.Column<DateTime>(nullable: true),
                    PeriodoIndeterminado = table.Column<bool>(nullable: false),
                    TotalParcelas = table.Column<bool>(nullable: false),
                    UnidadeId = table.Column<int>(nullable: false),
                    StatusPagamento = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comissoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contato",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    ReceberEmail = table.Column<bool>(nullable: false),
                    Celular = table.Column<string>(nullable: true),
                    RecebeSMS = table.Column<bool>(nullable: false),
                    TelefoneFixo = table.Column<string>(nullable: true),
                    ComoConheceuEnum = table.Column<int>(nullable: false),
                    TelefoneFixoPrincipal = table.Column<string>(nullable: true),
                    TelefoneFixo1 = table.Column<string>(nullable: true),
                    TelefoneFixo2 = table.Column<string>(nullable: true),
                    TelefoneFixo3 = table.Column<string>(nullable: true),
                    TelefoneFixo4 = table.Column<string>(nullable: true),
                    TelefoneFixo5 = table.Column<string>(nullable: true),
                    WhatsApp = table.Column<string>(nullable: true),
                    FaceBook = table.Column<string>(nullable: true),
                    Instagram = table.Column<string>(nullable: true),
                    Site = table.Column<string>(nullable: true),
                    Ramal = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    ReceberWhatsApp = table.Column<bool>(nullable: false),
                    ReceberFacebook = table.Column<bool>(nullable: false),
                    ReceberInstagram = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contato", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContratoLocacao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    NomeProprietario = table.Column<string>(nullable: true),
                    TelefoneProprietario = table.Column<string>(nullable: true),
                    NomeImobiliaria = table.Column<string>(nullable: true),
                    TelefoneFixo = table.Column<string>(nullable: true),
                    Celular = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    VigenciaInicio = table.Column<DateTime>(nullable: true),
                    VigenciaTermino = table.Column<DateTime>(nullable: true),
                    ValorAluguel = table.Column<decimal>(nullable: true),
                    ValorCondominio = table.Column<decimal>(nullable: true),
                    ValorIPTU = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContratoLocacao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Curso",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    NacionatalTec = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curso", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DadosBancario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    CodigoBanco = table.Column<string>(nullable: true),
                    NomeBanco = table.Column<string>(nullable: true),
                    NumeroAgencia = table.Column<string>(nullable: true),
                    NumeroConta = table.Column<string>(nullable: true),
                    TipoContaBancaria = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DadosBancario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DadosCartao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    NumeroCartao = table.Column<string>(nullable: true),
                    NomePessoa = table.Column<string>(nullable: true),
                    MesValidade = table.Column<string>(nullable: true),
                    AnoValidade = table.Column<string>(nullable: true),
                    BandeiraCartao = table.Column<string>(nullable: true),
                    AcquirersEnum = table.Column<int>(nullable: false),
                    ValorParcela = table.Column<decimal>(nullable: false),
                    ValorTotal = table.Column<decimal>(nullable: false),
                    QuantidadeParcela = table.Column<int>(nullable: false),
                    TID = table.Column<string>(nullable: true),
                    CodigoAutorizacao = table.Column<string>(nullable: true),
                    TipoPagamento = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DadosCartao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DadosContratacao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Matricula = table.Column<string>(nullable: true),
                    TipoRegimeContratacao = table.Column<int>(nullable: false),
                    DataAtestadoAdmissao = table.Column<DateTime>(nullable: true),
                    DataAtestadoDemissao = table.Column<DateTime>(nullable: true),
                    DataRecisao = table.Column<DateTime>(nullable: true),
                    TempoAlmoco = table.Column<string>(nullable: true),
                    Salario = table.Column<decimal>(nullable: true),
                    ValeTransporte = table.Column<decimal>(nullable: true),
                    ValeAlimentacao = table.Column<decimal>(nullable: true),
                    NumeroCT = table.Column<string>(nullable: true),
                    SerieCT = table.Column<string>(nullable: true),
                    DataEmissaoCT = table.Column<DateTime>(nullable: true),
                    CargaHorarioSemanalCT = table.Column<string>(nullable: true),
                    NumeroPIS = table.Column<string>(nullable: true),
                    NumeroTituloEleitor = table.Column<string>(nullable: true),
                    CargaCT = table.Column<string>(nullable: true),
                    ZonaTituloEleitor = table.Column<string>(nullable: true),
                    SecaoTituloEleitor = table.Column<string>(nullable: true),
                    TipoRegimeContratacaoAnterior = table.Column<int>(nullable: true),
                    DataAlteracaoRegime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DadosContratacao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DespesaDARF",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    NomeContribuinte = table.Column<string>(nullable: true),
                    PeriodoApuracao = table.Column<DateTime>(nullable: false),
                    CnpjCpf = table.Column<string>(nullable: true),
                    CodigoReceita = table.Column<string>(nullable: true),
                    NumeroReferencia = table.Column<string>(nullable: true),
                    DataVencimento = table.Column<DateTime>(nullable: false),
                    ValorPrincipal = table.Column<decimal>(nullable: true),
                    ValorMulta = table.Column<decimal>(nullable: true),
                    ValorJurosEncargos = table.Column<decimal>(nullable: true),
                    ValorTotal = table.Column<decimal>(nullable: true),
                    DataPagamento = table.Column<DateTime>(nullable: true),
                    ReferenciaEmpresa = table.Column<string>(nullable: true),
                    IdentificacaoComprovante = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DespesaDARF", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DespesaGPS",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    NomeContribuinte = table.Column<string>(nullable: true),
                    CodigoPagamento = table.Column<string>(nullable: true),
                    Competencia = table.Column<DateTime>(nullable: false),
                    Identificador = table.Column<string>(nullable: true),
                    ValorINSS = table.Column<decimal>(nullable: true),
                    ValorOutrasEntidades = table.Column<decimal>(nullable: true),
                    AtualizacaoMonetariaJuroMora = table.Column<decimal>(nullable: true),
                    ValorTotalRecolher = table.Column<decimal>(nullable: true),
                    DataPagamento = table.Column<DateTime>(nullable: true),
                    IdentificaçãoComprovante = table.Column<string>(nullable: true),
                    ReferenciaEmpresa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DespesaGPS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    CEP = table.Column<string>(nullable: true),
                    Rua = table.Column<string>(nullable: true),
                    Numero = table.Column<string>(nullable: true),
                    Complemento = table.Column<string>(nullable: true),
                    Bairro = table.Column<string>(nullable: true),
                    Cidade = table.Column<string>(nullable: true),
                    Estado = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistoricoEstoque",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    IdEstoque = table.Column<int>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    TipoHistorico = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoEstoque", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InstituicaoBancaria",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    NomeBanco = table.Column<string>(nullable: true),
                    CodigoBanco = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstituicaoBancaria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JornadaTrabalho",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    SegundaFeiraInicio = table.Column<string>(nullable: true),
                    SegundaFeiraTermino = table.Column<string>(nullable: true),
                    TercaFeiraInicio = table.Column<string>(nullable: true),
                    TercaFeiraTermino = table.Column<string>(nullable: true),
                    QuartaFeiraInicio = table.Column<string>(nullable: true),
                    QuartaFeiraTermino = table.Column<string>(nullable: true),
                    QuintaFeiraInicio = table.Column<string>(nullable: true),
                    QuintaFeiraTermino = table.Column<string>(nullable: true),
                    SextaFeiraInicio = table.Column<string>(nullable: true),
                    SextaFeiraTermino = table.Column<string>(nullable: true),
                    SabadoInicio = table.Column<string>(nullable: true),
                    SabadoTermino = table.Column<string>(nullable: true),
                    DomingoInicio = table.Column<string>(nullable: true),
                    DomingoTermino = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JornadaTrabalho", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MateriaProfessor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    LinguaPortuguesa = table.Column<bool>(nullable: false),
                    Artes = table.Column<bool>(nullable: false),
                    Matematica = table.Column<bool>(nullable: false),
                    Biologia = table.Column<bool>(nullable: false),
                    Quimica = table.Column<bool>(nullable: false),
                    Historia = table.Column<bool>(nullable: false),
                    Geografia = table.Column<bool>(nullable: false),
                    Filosofia = table.Column<bool>(nullable: false),
                    Sociologia = table.Column<bool>(nullable: false),
                    Ingles = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MateriaProfessor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MetaUnidade",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    UnidadeId = table.Column<int>(nullable: false),
                    MetaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetaUnidade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nacionalidade",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nacionalidade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Naturalidade",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Naturalidade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parametro",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Chave = table.Column<string>(nullable: true),
                    Valor = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parametro", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerfilUsuario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    CadastrarAluno = table.Column<bool>(nullable: false),
                    ConsultarAluno = table.Column<bool>(nullable: false),
                    Relatorios = table.Column<bool>(nullable: false),
                    CriacaoUsuario = table.Column<bool>(nullable: false),
                    CriacaoPerfil = table.Column<bool>(nullable: false),
                    TicketAdministracao = table.Column<bool>(nullable: false),
                    TicketPainel = table.Column<bool>(nullable: false),
                    Comunicacao = table.Column<bool>(nullable: false),
                    CadastroFornecedor = table.Column<bool>(nullable: false),
                    ContasAPagar = table.Column<bool>(nullable: false),
                    Estoque = table.Column<bool>(nullable: false),
                    FolhaPagamento = table.Column<bool>(nullable: false),
                    CriarAgendaProva = table.Column<bool>(nullable: false),
                    ListaPassageiros = table.Column<bool>(nullable: false),
                    HistoricoViagem = table.Column<bool>(nullable: false),
                    CursoTurma = table.Column<bool>(nullable: false),
                    Unidade = table.Column<bool>(nullable: false),
                    PlanoPagamento = table.Column<bool>(nullable: false),
                    PromocoesBolsaConvenio = table.Column<bool>(nullable: false),
                    CriarMeta = table.Column<bool>(nullable: false),
                    MetaPainel = table.Column<bool>(nullable: false),
                    CriarComissoes = table.Column<bool>(nullable: false),
                    CriarAulaOnline = table.Column<bool>(nullable: false),
                    MinhasAulas = table.Column<bool>(nullable: false),
                    SemanaInicio = table.Column<string>(nullable: true),
                    SemanaTermino = table.Column<string>(nullable: true),
                    SabadoInicio = table.Column<string>(nullable: true),
                    SabadoTermino = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilUsuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanoPagamento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    TipoPagamento = table.Column<int>(nullable: false),
                    QuantidadeParcela = table.Column<int>(nullable: false),
                    ValorParcela = table.Column<decimal>(nullable: false),
                    ValorTotalPlano = table.Column<decimal>(nullable: false),
                    PorcentagemDescontoPontualidade = table.Column<decimal>(nullable: false),
                    ValorTotalInscricaoProva = table.Column<decimal>(nullable: false),
                    ValorMaterialDidatico = table.Column<decimal>(nullable: false),
                    IsentarMaterialDidatico = table.Column<bool>(nullable: false),
                    ValorTaxaMatricula = table.Column<decimal>(nullable: false),
                    IsentarMatricula = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanoPagamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanoPagamentoAluno",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    TipoPagamento = table.Column<int>(nullable: false),
                    PlanoPagamentoId = table.Column<int>(nullable: true),
                    CampanhaId = table.Column<int>(nullable: true),
                    DataPrimeiraParcela = table.Column<DateTime>(nullable: true),
                    DataSegundaParcela = table.Column<DateTime>(nullable: true),
                    TemApostila = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanoPagamentoAluno", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SolicitacaoEmail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    DataEnvio = table.Column<DateTime>(nullable: false),
                    CorpoEmail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitacaoEmail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Turma",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Presencial = table.Column<bool>(nullable: false),
                    Ano = table.Column<string>(nullable: true),
                    Semestre = table.Column<string>(nullable: true),
                    Periodo = table.Column<int>(nullable: false),
                    HorarioInicio = table.Column<string>(nullable: true),
                    HorarioTermino = table.Column<string>(nullable: true),
                    Sala = table.Column<int>(nullable: false),
                    QuantidadeVagas = table.Column<int>(nullable: false),
                    Disponivel = table.Column<bool>(nullable: false),
                    Segunda = table.Column<bool>(nullable: false),
                    Terca = table.Column<bool>(nullable: false),
                    Quarta = table.Column<bool>(nullable: false),
                    Quinta = table.Column<bool>(nullable: false),
                    Sexta = table.Column<bool>(nullable: false),
                    Sabado = table.Column<bool>(nullable: false),
                    Domingo = table.Column<bool>(nullable: false),
                    InicioTurma = table.Column<DateTime>(nullable: true),
                    TerminoTurma = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turma", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VideoPausado",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    URL = table.Column<string>(nullable: true),
                    MatriculaId = table.Column<int>(nullable: false),
                    VideoId = table.Column<int>(nullable: false),
                    Tempo = table.Column<float>(nullable: false),
                    DataUltimaVisualizacao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoPausado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    UsuarioLogadoId = table.Column<int>(nullable: false),
                    IdFuncionarioAtendente = table.Column<int>(nullable: true),
                    DataAtendimento = table.Column<DateTime>(nullable: true),
                    DataAbertura = table.Column<DateTime>(nullable: true),
                    NumeroProtocolo = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    AssuntoTicketId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticket_AssuntoTicket_AssuntoTicketId",
                        column: x => x.AssuntoTicketId,
                        principalTable: "AssuntoTicket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CursoOnline",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    CursoId = table.Column<int>(nullable: false),
                    AulaOnlineId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursoOnline", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CursoOnline_AulaOnline_AulaOnlineId",
                        column: x => x.AulaOnlineId,
                        principalTable: "AulaOnline",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MateriaOnline",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    NomeMateriaOnline = table.Column<string>(nullable: true),
                    ProfessorId = table.Column<int>(nullable: false),
                    AulaOnlineId = table.Column<int>(nullable: false),
                    MateriaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MateriaOnline", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MateriaOnline_AulaOnline_AulaOnlineId",
                        column: x => x.AulaOnlineId,
                        principalTable: "AulaOnline",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CampanhaTipoPagamento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    TipoPagamento = table.Column<int>(nullable: false),
                    CampanhaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampanhaTipoPagamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CampanhaTipoPagamento_Campanha_CampanhaId",
                        column: x => x.CampanhaId,
                        principalTable: "Campanha",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CampanhaUnidade",
                columns: table => new
                {
                    CampanhaId = table.Column<int>(nullable: false),
                    UnidadeId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampanhaUnidade", x => new { x.CampanhaId, x.UnidadeId });
                    table.ForeignKey(
                        name: "FK_CampanhaUnidade_Campanha_CampanhaId",
                        column: x => x.CampanhaId,
                        principalTable: "Campanha",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComissaoParcela",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    NumeroParcela = table.Column<int>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false),
                    ComissoesId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComissaoParcela", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComissaoParcela_Comissoes_ComissoesId",
                        column: x => x.ComissoesId,
                        principalTable: "Comissoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Materia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    NomeMateria = table.Column<string>(nullable: true),
                    CursoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Materia_Curso_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Curso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pagamento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    TipoPagamento = table.Column<int>(nullable: false),
                    NossoNumero = table.Column<string>(nullable: true),
                    DataEmissao = table.Column<DateTime>(nullable: true),
                    DataVencimento = table.Column<DateTime>(nullable: true),
                    DataPagamento = table.Column<DateTime>(nullable: true),
                    CodigoBarras = table.Column<string>(nullable: true),
                    NumeroLinhaDigitavel = table.Column<string>(nullable: true),
                    Valor = table.Column<decimal>(nullable: false),
                    ValorPago = table.Column<decimal>(nullable: false),
                    TarifaBanco = table.Column<decimal>(nullable: true),
                    Desconto = table.Column<decimal>(nullable: true),
                    Acrescimo = table.Column<decimal>(nullable: true),
                    PromocaoBolsaConvenio = table.Column<decimal>(nullable: true),
                    TipoSituacao = table.Column<int>(nullable: false),
                    MatriculaId = table.Column<int>(nullable: false),
                    BoletoHTML = table.Column<string>(nullable: true),
                    PagamentoIdOld = table.Column<int>(nullable: true),
                    NumeroRegistro = table.Column<string>(nullable: true),
                    DadosCartaoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pagamento_DadosCartao_DadosCartaoId",
                        column: x => x.DadosCartaoId,
                        principalTable: "DadosCartao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ColegioAutorizado",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    NomeColegioAutorizado = table.Column<string>(nullable: true),
                    PrimeiroContatoNome = table.Column<string>(nullable: true),
                    PrimeiroContatoTelefone = table.Column<string>(nullable: true),
                    PrimeiroContatoEmail = table.Column<string>(nullable: true),
                    SegundoContatoNome = table.Column<string>(nullable: true),
                    SegundoContatoTelefone = table.Column<string>(nullable: true),
                    SegundoContatoEmail = table.Column<string>(nullable: true),
                    Site = table.Column<string>(nullable: true),
                    RazaoSocial = table.Column<string>(nullable: true),
                    CNPJ = table.Column<string>(nullable: true),
                    EnderecoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColegioAutorizado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ColegioAutorizado_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Fornecedor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    TipoPessoa = table.Column<int>(nullable: false),
                    RazaoSocial = table.Column<string>(nullable: true),
                    NomeFantasia = table.Column<string>(nullable: true),
                    CpfCnpj = table.Column<string>(nullable: true),
                    DataNascimento = table.Column<DateTime>(nullable: false),
                    InscricaoMunicipal = table.Column<string>(nullable: true),
                    InscricaoEstadual = table.Column<string>(nullable: true),
                    Isento = table.Column<bool>(nullable: false),
                    Observacao = table.Column<string>(nullable: true),
                    ContatoId = table.Column<int>(nullable: true),
                    EnderecoId = table.Column<int>(nullable: true),
                    DadosBancarioId = table.Column<int>(nullable: true),
                    CategoriaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fornecedor_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fornecedor_Contato_ContatoId",
                        column: x => x.ContatoId,
                        principalTable: "Contato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fornecedor_DadosBancario_DadosBancarioId",
                        column: x => x.DadosBancarioId,
                        principalTable: "DadosBancario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fornecedor_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Unidade",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    CNPJ = table.Column<string>(nullable: true),
                    RazaoSocial = table.Column<string>(nullable: true),
                    NomeFantasia = table.Column<string>(nullable: true),
                    VigenciaInicioAVCB = table.Column<DateTime>(nullable: true),
                    VigenciaTerminoAVCB = table.Column<DateTime>(nullable: true),
                    VigenciaInicioAlvara = table.Column<DateTime>(nullable: true),
                    VigenciaTerminoAlvara = table.Column<DateTime>(nullable: true),
                    EnderecoId = table.Column<int>(nullable: true),
                    ContatoId = table.Column<int>(nullable: true),
                    DadosBancarioId = table.Column<int>(nullable: true),
                    ContratoLocacaoId = table.Column<int>(nullable: true),
                    Sigla = table.Column<string>(nullable: true),
                    Foto = table.Column<byte[]>(nullable: true),
                    Extensao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unidade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Unidade_Contato_ContatoId",
                        column: x => x.ContatoId,
                        principalTable: "Contato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Unidade_ContratoLocacao_ContratoLocacaoId",
                        column: x => x.ContratoLocacaoId,
                        principalTable: "ContratoLocacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Unidade_DadosBancario_DadosBancarioId",
                        column: x => x.DadosBancarioId,
                        principalTable: "DadosBancario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Unidade_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Funcionario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    CPF = table.Column<string>(nullable: true),
                    RG = table.Column<string>(nullable: true),
                    DataNascimento = table.Column<DateTime>(nullable: false),
                    ContatoId = table.Column<int>(nullable: true),
                    EnderecoId = table.Column<int>(nullable: true),
                    DadosBancarioId = table.Column<int>(nullable: true),
                    AgenteIntegracaoId = table.Column<int>(nullable: true),
                    DadosContratacaoId = table.Column<int>(nullable: true),
                    JornadaTrabalhoId = table.Column<int>(nullable: true),
                    MateriaProfessorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Funcionario_AgenteIntegracao_AgenteIntegracaoId",
                        column: x => x.AgenteIntegracaoId,
                        principalTable: "AgenteIntegracao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Funcionario_Contato_ContatoId",
                        column: x => x.ContatoId,
                        principalTable: "Contato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Funcionario_DadosBancario_DadosBancarioId",
                        column: x => x.DadosBancarioId,
                        principalTable: "DadosBancario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Funcionario_DadosContratacao_DadosContratacaoId",
                        column: x => x.DadosContratacaoId,
                        principalTable: "DadosContratacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Funcionario_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Funcionario_JornadaTrabalho_JornadaTrabalhoId",
                        column: x => x.JornadaTrabalhoId,
                        principalTable: "JornadaTrabalho",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Funcionario_MateriaProfessor_MateriaProfessorId",
                        column: x => x.MateriaProfessorId,
                        principalTable: "MateriaProfessor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlanoPagamentoCurso",
                columns: table => new
                {
                    PlanoPagamentoId = table.Column<int>(nullable: false),
                    CursoId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanoPagamentoCurso", x => new { x.PlanoPagamentoId, x.CursoId });
                    table.ForeignKey(
                        name: "FK_PlanoPagamentoCurso_PlanoPagamento_PlanoPagamentoId",
                        column: x => x.PlanoPagamentoId,
                        principalTable: "PlanoPagamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlanoPagamentoUnidade",
                columns: table => new
                {
                    PlanoPagamentoId = table.Column<int>(nullable: false),
                    UnidadeId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanoPagamentoUnidade", x => new { x.PlanoPagamentoId, x.UnidadeId });
                    table.ForeignKey(
                        name: "FK_PlanoPagamentoUnidade_PlanoPagamento_PlanoPagamentoId",
                        column: x => x.PlanoPagamentoId,
                        principalTable: "PlanoPagamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TurmaCurso",
                columns: table => new
                {
                    CursoId = table.Column<int>(nullable: false),
                    TurmaId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurmaCurso", x => new { x.TurmaId, x.CursoId });
                    table.ForeignKey(
                        name: "FK_TurmaCurso_Turma_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TurmaUnidade",
                columns: table => new
                {
                    UnidadeId = table.Column<int>(nullable: false),
                    TurmaId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurmaUnidade", x => new { x.TurmaId, x.UnidadeId });
                    table.ForeignKey(
                        name: "FK_TurmaUnidade_Turma_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoAula",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    TituloAula = table.Column<string>(nullable: true),
                    URLVideo = table.Column<string>(nullable: true),
                    MateriaId = table.Column<int>(nullable: false),
                    MateriaOnlineId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoAula", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoAula_MateriaOnline_MateriaOnlineId",
                        column: x => x.MateriaOnlineId,
                        principalTable: "MateriaOnline",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmailEnviado",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    DataEnvio = table.Column<DateTime>(nullable: false),
                    EmailPara = table.Column<string>(nullable: true),
                    CorpoEmail = table.Column<string>(nullable: true),
                    PagamentoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailEnviado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailEnviado_Pagamento_PagamentoId",
                        column: x => x.PagamentoId,
                        principalTable: "Pagamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgendaProva",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    InicioInscricao = table.Column<DateTime>(nullable: true),
                    TerminoInscricao = table.Column<DateTime>(nullable: true),
                    DataProva = table.Column<DateTime>(nullable: true),
                    QuantidadeVagas = table.Column<int>(nullable: false),
                    TipoProva = table.Column<int>(nullable: false),
                    ColegioAutorizadoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendaProva", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgendaProva_ColegioAutorizado_ColegioAutorizadoId",
                        column: x => x.ColegioAutorizadoId,
                        principalTable: "ColegioAutorizado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Aluno",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    DataNascimento = table.Column<DateTime>(nullable: false),
                    NomeMae = table.Column<string>(nullable: true),
                    Sexo = table.Column<int>(nullable: false),
                    CPF = table.Column<string>(nullable: true),
                    RG = table.Column<string>(nullable: true),
                    OrgaoExpedicao = table.Column<string>(nullable: true),
                    EstadoCivil = table.Column<int>(nullable: false),
                    TituloEleitoral = table.Column<string>(nullable: true),
                    Zona = table.Column<string>(nullable: true),
                    Secao = table.Column<string>(nullable: true),
                    NomeResponsavel = table.Column<string>(nullable: true),
                    RGResponsavel = table.Column<string>(nullable: true),
                    CPFResponsavel = table.Column<string>(nullable: true),
                    UnidadeId = table.Column<int>(nullable: false),
                    ContatoId = table.Column<int>(nullable: false),
                    EnderecoId = table.Column<int>(nullable: false),
                    Foto = table.Column<byte[]>(nullable: true),
                    Extensao = table.Column<string>(nullable: true),
                    NacionalidadeId = table.Column<int>(nullable: false),
                    NaturalidadeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aluno", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aluno_Contato_ContatoId",
                        column: x => x.ContatoId,
                        principalTable: "Contato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Aluno_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Aluno_Nacionalidade_NacionalidadeId",
                        column: x => x.NacionalidadeId,
                        principalTable: "Nacionalidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Aluno_Naturalidade_NaturalidadeId",
                        column: x => x.NaturalidadeId,
                        principalTable: "Naturalidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Aluno_Unidade_UnidadeId",
                        column: x => x.UnidadeId,
                        principalTable: "Unidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CentroCusto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    UnidadeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentroCusto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CentroCusto_Unidade_UnidadeId",
                        column: x => x.UnidadeId,
                        principalTable: "Unidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoricoOcorrencias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    UnidadeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoOcorrencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricoOcorrencias_Unidade_UnidadeId",
                        column: x => x.UnidadeId,
                        principalTable: "Unidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HorarioFuncionamento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    FinalSemana = table.Column<bool>(nullable: false),
                    ComAula = table.Column<bool>(nullable: false),
                    SemanaInicio = table.Column<string>(nullable: true),
                    SemanaTermino = table.Column<string>(nullable: true),
                    SabadoInicio = table.Column<string>(nullable: true),
                    SabadoTermino = table.Column<string>(nullable: true),
                    UnidadeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorarioFuncionamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HorarioFuncionamento_Unidade_UnidadeId",
                        column: x => x.UnidadeId,
                        principalTable: "Unidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    InicioMeta = table.Column<DateTime>(nullable: true),
                    TerminoMeta = table.Column<DateTime>(nullable: true),
                    Quantidade = table.Column<int>(nullable: false),
                    BonusPeriodo = table.Column<decimal>(nullable: false),
                    StatusPagamento = table.Column<int>(nullable: false),
                    UnidadeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meta_Unidade_UnidadeId",
                        column: x => x.UnidadeId,
                        principalTable: "Unidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    NomeProduto = table.Column<string>(nullable: true),
                    DataEntrada = table.Column<DateTime>(nullable: false),
                    AlertaQuantidadeMinima = table.Column<int>(nullable: false),
                    CodigoNCM = table.Column<string>(nullable: true),
                    CodigoInterno = table.Column<string>(nullable: true),
                    UnidadeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produto_Unidade_UnidadeId",
                        column: x => x.UnidadeId,
                        principalTable: "Unidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UnidadeDespesa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    UnidadeId = table.Column<int>(nullable: false),
                    Valor = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadeDespesa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnidadeDespesa_Unidade_UnidadeId",
                        column: x => x.UnidadeId,
                        principalTable: "Unidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CursoProfessor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    NomeCurso = table.Column<string>(nullable: true),
                    FuncionarioId = table.Column<int>(nullable: false),
                    IdCurso = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursoProfessor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CursoProfessor_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeriasFuncionario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Inicio = table.Column<DateTime>(nullable: false),
                    Termino = table.Column<DateTime>(nullable: false),
                    TipoFeriasFolgaFalta = table.Column<int>(nullable: false),
                    ApenasFerias = table.Column<bool>(nullable: false),
                    Observacao = table.Column<string>(nullable: true),
                    FuncionarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeriasFuncionario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeriasFuncionario_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FolhaPagamento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    DataCadastro = table.Column<DateTime>(nullable: true),
                    DataPagamento = table.Column<DateTime>(nullable: true),
                    StatusPagamento = table.Column<int>(nullable: false),
                    SalarioBruto = table.Column<decimal>(nullable: true),
                    SalarioLiquido = table.Column<decimal>(nullable: true),
                    Alimentacao = table.Column<decimal>(nullable: true),
                    Transporte = table.Column<decimal>(nullable: true),
                    QuantidadeDias = table.Column<int>(nullable: true),
                    MonitoriaProva = table.Column<decimal>(nullable: true),
                    ComissaoPrimeiraParcelaPaga = table.Column<decimal>(nullable: true),
                    BonusMetaMes = table.Column<decimal>(nullable: true),
                    BonusMetaPeriodo = table.Column<decimal>(nullable: true),
                    ValorAdicional = table.Column<decimal>(nullable: true),
                    JustificativaValorAdicional = table.Column<string>(nullable: true),
                    ValorDiasDSR = table.Column<decimal>(nullable: true),
                    JustificativaDSR = table.Column<string>(nullable: true),
                    ValorFerias = table.Column<decimal>(nullable: true),
                    JustificativaFerias = table.Column<string>(nullable: true),
                    ValorDecimoTerceiro = table.Column<decimal>(nullable: true),
                    JustificativaDecimoTerceiro = table.Column<string>(nullable: true),
                    ValorTotalDesconto = table.Column<decimal>(nullable: true),
                    JustificativaDesconto = table.Column<string>(nullable: true),
                    ValorTotalPagamento = table.Column<decimal>(nullable: true),
                    FuncionarioId = table.Column<int>(nullable: true),
                    UnidadeId = table.Column<int>(nullable: false),
                    NomeUsuario = table.Column<string>(nullable: true),
                    BancoPagamento = table.Column<string>(nullable: true),
                    Competencia = table.Column<DateTime>(nullable: true),
                    InicioHoraExtraPaga = table.Column<DateTime>(nullable: true),
                    TerminoHoraExtraPaga = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FolhaPagamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FolhaPagamento_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MensagemTicket",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Mensagem = table.Column<string>(nullable: true),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    FuncionarioId = table.Column<int>(nullable: true),
                    TicketId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MensagemTicket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MensagemTicket_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PontoEletronico",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Entrada1 = table.Column<DateTime>(nullable: true),
                    Saida1 = table.Column<DateTime>(nullable: true),
                    Entrada2 = table.Column<DateTime>(nullable: true),
                    Saida2 = table.Column<DateTime>(nullable: true),
                    Entrada3 = table.Column<DateTime>(nullable: true),
                    Saida3 = table.Column<DateTime>(nullable: true),
                    Entrada4 = table.Column<DateTime>(nullable: true),
                    Saida4 = table.Column<DateTime>(nullable: true),
                    TipoOcorrenciaPonto = table.Column<int>(nullable: false),
                    Observacao = table.Column<string>(nullable: true),
                    UsuarioId = table.Column<int>(nullable: true),
                    FeriasId = table.Column<int>(nullable: true),
                    ApenasFerias = table.Column<bool>(nullable: false),
                    FuncionarioId = table.Column<int>(nullable: true),
                    NumeroPIS = table.Column<string>(nullable: true),
                    NomeArquivo = table.Column<string>(nullable: true),
                    RegimeContratacao = table.Column<int>(nullable: false),
                    Pago = table.Column<bool>(nullable: false),
                    FolhaPagamentoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PontoEletronico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PontoEletronico_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalarioUnidade",
                columns: table => new
                {
                    FuncionarioId = table.Column<int>(nullable: false),
                    UnidadeId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    DescricaoCargo = table.Column<string>(nullable: true),
                    ValorSalario = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalarioUnidade", x => new { x.FuncionarioId, x.UnidadeId });
                    table.ForeignKey(
                        name: "FK_SalarioUnidade_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pergunta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    DescricaoPergunta = table.Column<string>(nullable: true),
                    VideoAulaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pergunta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pergunta_VideoAula_VideoAulaId",
                        column: x => x.VideoAulaId,
                        principalTable: "VideoAula",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgendaCurso",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    CursoId = table.Column<int>(nullable: false),
                    AgendaProvaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendaCurso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgendaCurso_AgendaProva_AgendaProvaId",
                        column: x => x.AgendaProvaId,
                        principalTable: "AgendaProva",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgendaCurso_Curso_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Curso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnidadeParticipanteProva",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    HoraSaida = table.Column<string>(nullable: true),
                    LocalSaida = table.Column<string>(nullable: true),
                    UnidadeId = table.Column<int>(nullable: false),
                    AgendaProvaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadeParticipanteProva", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnidadeParticipanteProva_AgendaProva_AgendaProvaId",
                        column: x => x.AgendaProvaId,
                        principalTable: "AgendaProva",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatriculaAluno",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    NumeroMatricula = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    DataMatricula = table.Column<DateTime>(nullable: false),
                    CursoId = table.Column<int>(nullable: false),
                    TurmaId = table.Column<int>(nullable: false),
                    AlunoId = table.Column<int>(nullable: false),
                    UnidadeId = table.Column<int>(nullable: true),
                    PlanoPagamentoAlunoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatriculaAluno", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatriculaAluno_Aluno_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Aluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatriculaAluno_Curso_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Curso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatriculaAluno_PlanoPagamentoAluno_PlanoPagamentoAlunoId",
                        column: x => x.PlanoPagamentoAlunoId,
                        principalTable: "PlanoPagamentoAluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatriculaAluno_Turma_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatriculaAluno_Unidade_UnidadeId",
                        column: x => x.UnidadeId,
                        principalTable: "Unidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Despesa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    NomeDespesa = table.Column<string>(nullable: true),
                    DataVencimento = table.Column<DateTime>(nullable: true),
                    ValorTotalDespesa = table.Column<decimal>(nullable: false),
                    DataEmissao = table.Column<DateTime>(nullable: true),
                    NumeroDocumento = table.Column<string>(nullable: true),
                    TipoParcela = table.Column<int>(nullable: false),
                    QuantidadeParcela = table.Column<int>(nullable: false),
                    Observacao = table.Column<string>(nullable: true),
                    CentroCustoId = table.Column<int>(nullable: true),
                    CategoriaId = table.Column<int>(nullable: true),
                    UnidadeId = table.Column<int>(nullable: true),
                    FornecedorId = table.Column<int>(nullable: true),
                    TipoDespesa = table.Column<int>(nullable: false),
                    CodigoBanco = table.Column<string>(nullable: true),
                    NomeBanco = table.Column<string>(nullable: true),
                    NumeroAgencia = table.Column<string>(nullable: true),
                    NumeroConta = table.Column<string>(nullable: true),
                    TipoContaBancaria = table.Column<int>(nullable: true),
                    DespesaGPSId = table.Column<int>(nullable: true),
                    DespesaDARFId = table.Column<int>(nullable: true),
                    DataCriacao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Despesa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Despesa_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Despesa_CentroCusto_CentroCustoId",
                        column: x => x.CentroCustoId,
                        principalTable: "CentroCusto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Despesa_DespesaDARF_DespesaDARFId",
                        column: x => x.DespesaDARFId,
                        principalTable: "DespesaDARF",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Despesa_DespesaGPS_DespesaGPSId",
                        column: x => x.DespesaGPSId,
                        principalTable: "DespesaGPS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Despesa_Fornecedor_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Despesa_Unidade_UnidadeId",
                        column: x => x.UnidadeId,
                        principalTable: "Unidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DestinatarioTicket",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketId = table.Column<int>(nullable: false),
                    UsuarioLogadoId = table.Column<int>(nullable: false),
                    Mensagem = table.Column<string>(nullable: true),
                    DataAtendimento = table.Column<DateTime>(nullable: false),
                    StatusTicket = table.Column<int>(nullable: false),
                    DepartamentoId = table.Column<int>(nullable: true),
                    UnidadeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DestinatarioTicket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DestinatarioTicket_CentroCusto_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "CentroCusto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DestinatarioTicket_Ticket_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DestinatarioTicket_Unidade_UnidadeId",
                        column: x => x.UnidadeId,
                        principalTable: "Unidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Solicitacao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    TipoSolicitacao = table.Column<int>(nullable: false),
                    Valor = table.Column<decimal>(nullable: true),
                    IsBalcao = table.Column<bool>(nullable: false),
                    IsPreDefinida = table.Column<bool>(nullable: false),
                    IsAprovadoProva = table.Column<bool>(nullable: false),
                    UnidadeId = table.Column<int>(nullable: true),
                    CentroCustoId = table.Column<int>(nullable: true),
                    QuantidadeParcelaPaga = table.Column<int>(nullable: true),
                    IsCursoQuitado = table.Column<bool>(nullable: false),
                    EnviaTicket = table.Column<bool>(nullable: false),
                    EnviaEmail = table.Column<bool>(nullable: false),
                    EmailTitulo = table.Column<string>(nullable: true),
                    EmailConteudo = table.Column<string>(nullable: true),
                    IsPendenciaDocumental = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Solicitacao_CentroCusto_CentroCustoId",
                        column: x => x.CentroCustoId,
                        principalTable: "CentroCusto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Solicitacao_Unidade_UnidadeId",
                        column: x => x.UnidadeId,
                        principalTable: "Unidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    PerfilUsuarioId = table.Column<int>(nullable: true),
                    DepartamentoId = table.Column<int>(nullable: true),
                    FuncionarioId = table.Column<int>(nullable: true),
                    UnidadeId = table.Column<int>(nullable: true),
                    AlunoId = table.Column<int>(nullable: true),
                    IsAluno = table.Column<bool>(nullable: false),
                    IsPrimeiroAcesso = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Aluno_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Aluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Usuario_CentroCusto_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "CentroCusto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Usuario_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Usuario_PerfilUsuario_PerfilUsuarioId",
                        column: x => x.PerfilUsuarioId,
                        principalTable: "PerfilUsuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Usuario_Unidade_UnidadeId",
                        column: x => x.UnidadeId,
                        principalTable: "Unidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MetaPeriodo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    DataPeriodo = table.Column<DateTime>(nullable: false),
                    Quantidade = table.Column<int>(nullable: false),
                    MetaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetaPeriodo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MetaPeriodo_Meta_MetaId",
                        column: x => x.MetaId,
                        principalTable: "Meta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemProduto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    DataEntrada = table.Column<DateTime>(nullable: false),
                    NomeFornecedor = table.Column<string>(nullable: true),
                    CNPJ = table.Column<string>(nullable: true),
                    NumeroNotaFiscal = table.Column<string>(nullable: true),
                    QuantidadeEntrada = table.Column<int>(nullable: false),
                    QuantidadeSaida = table.Column<int>(nullable: false),
                    ProdutoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemProduto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemProduto_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MateriaCursoProfessor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    NomeMateria = table.Column<string>(nullable: true),
                    CursoProfessorId = table.Column<int>(nullable: false),
                    IdMateria = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MateriaCursoProfessor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MateriaCursoProfessor_CursoProfessor_CursoProfessorId",
                        column: x => x.CursoProfessorId,
                        principalTable: "CursoProfessor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HoraExtra",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Porcentagem = table.Column<float>(nullable: false),
                    QuantidadeHoras = table.Column<string>(nullable: true),
                    Valor = table.Column<decimal>(nullable: false),
                    FolhaPagamentoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoraExtra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HoraExtra_FolhaPagamento_FolhaPagamentoId",
                        column: x => x.FolhaPagamentoId,
                        principalTable: "FolhaPagamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlunoQuestionario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    PerguntaId = table.Column<int>(nullable: false),
                    AlunoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunoQuestionario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlunoQuestionario_Aluno_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Aluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlunoQuestionario_Pergunta_PerguntaId",
                        column: x => x.PerguntaId,
                        principalTable: "Pergunta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resposta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    Opcao = table.Column<string>(nullable: true),
                    PerguntaId = table.Column<int>(nullable: true),
                    Correta = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resposta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resposta_Pergunta_PerguntaId",
                        column: x => x.PerguntaId,
                        principalTable: "Pergunta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CertificadoProva",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    DataRecebimentoSuporte = table.Column<DateTime>(nullable: true),
                    DataEntregaAluno = table.Column<DateTime>(nullable: true),
                    GDAE = table.Column<string>(nullable: true),
                    MatriculaAlunoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificadoProva", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CertificadoProva_MatriculaAluno_MatriculaAlunoId",
                        column: x => x.MatriculaAlunoId,
                        principalTable: "MatriculaAluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MensagemAlunoProfessor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Mensagem = table.Column<string>(nullable: true),
                    AlunoId = table.Column<int>(nullable: false),
                    ProfessorId = table.Column<int>(nullable: false),
                    DataEnvio = table.Column<DateTime>(nullable: false),
                    TipoMensagem = table.Column<int>(nullable: false),
                    MatriculaAlunoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MensagemAlunoProfessor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MensagemAlunoProfessor_MatriculaAluno_MatriculaAlunoId",
                        column: x => x.MatriculaAlunoId,
                        principalTable: "MatriculaAluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProvaAluno",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    LocalProva = table.Column<string>(nullable: true),
                    DataProva = table.Column<DateTime>(nullable: false),
                    TipoTransporte = table.Column<int>(nullable: false),
                    MatriculaAlunoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvaAluno", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProvaAluno_MatriculaAluno_MatriculaAlunoId",
                        column: x => x.MatriculaAlunoId,
                        principalTable: "MatriculaAluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DespesaParcela",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    DataVencimento = table.Column<DateTime>(nullable: false),
                    DataPagamento = table.Column<DateTime>(nullable: true),
                    ValorParcela = table.Column<decimal>(nullable: false),
                    TipoPagamento = table.Column<int>(nullable: false),
                    DespesaId = table.Column<int>(nullable: false),
                    StatusPagamento = table.Column<int>(nullable: false),
                    ValorPago = table.Column<decimal>(nullable: true),
                    DescontoTaxa = table.Column<decimal>(nullable: true),
                    Juros = table.Column<decimal>(nullable: true),
                    Observacao = table.Column<string>(nullable: true),
                    UnidadeId = table.Column<int>(nullable: true),
                    CodigoBarras = table.Column<string>(nullable: true),
                    LancamentoManual = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DespesaParcela", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DespesaParcela_Despesa_DespesaId",
                        column: x => x.DespesaId,
                        principalTable: "Despesa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DespesaParcela_Unidade_UnidadeId",
                        column: x => x.UnidadeId,
                        principalTable: "Unidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioDestinarioTicket",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    FuncionarioId = table.Column<int>(nullable: true),
                    DestinatarioTicketId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioDestinarioTicket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioDestinarioTicket_DestinatarioTicket_DestinatarioTicketId",
                        column: x => x.DestinatarioTicketId,
                        principalTable: "DestinatarioTicket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmailDestinatario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    SolicitacaoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailDestinatario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailDestinatario_Solicitacao_SolicitacaoId",
                        column: x => x.SolicitacaoId,
                        principalTable: "Solicitacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolicitacaoAluno",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    DataSolicitacao = table.Column<DateTime>(nullable: false),
                    StatusPagamento = table.Column<int>(nullable: false),
                    SolicitacaoId = table.Column<int>(nullable: false),
                    SolicitacaoEmailId = table.Column<int>(nullable: true),
                    MatriculaId = table.Column<int>(nullable: false),
                    MatriculaAlunoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitacaoAluno", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitacaoAluno_MatriculaAluno_MatriculaAlunoId",
                        column: x => x.MatriculaAlunoId,
                        principalTable: "MatriculaAluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitacaoAluno_SolicitacaoEmail_SolicitacaoEmailId",
                        column: x => x.SolicitacaoEmailId,
                        principalTable: "SolicitacaoEmail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitacaoAluno_Solicitacao_SolicitacaoId",
                        column: x => x.SolicitacaoId,
                        principalTable: "Solicitacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolicitacaoCurso",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CursoId = table.Column<int>(nullable: false),
                    SolicitacaoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitacaoCurso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitacaoCurso_Curso_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Curso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitacaoCurso_Solicitacao_SolicitacaoId",
                        column: x => x.SolicitacaoId,
                        principalTable: "Solicitacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolicitacaoFuncionarioTicket",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    FuncionarioId = table.Column<int>(nullable: false),
                    SolicitacaoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitacaoFuncionarioTicket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitacaoFuncionarioTicket_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitacaoFuncionarioTicket_Solicitacao_SolicitacaoId",
                        column: x => x.SolicitacaoId,
                        principalTable: "Solicitacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StatusCertificado",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    StatusCertificadoEnum = table.Column<int>(nullable: false),
                    SolicitacaoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusCertificado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatusCertificado_Solicitacao_SolicitacaoId",
                        column: x => x.SolicitacaoId,
                        principalTable: "Solicitacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StatusProva",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    StatusProvaEnum = table.Column<int>(nullable: false),
                    SolicitacaoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusProva", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatusProva_Solicitacao_SolicitacaoId",
                        column: x => x.SolicitacaoId,
                        principalTable: "Solicitacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlunoQuestionarioReposta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Opcao = table.Column<string>(nullable: true),
                    AlunoQuestionarioId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunoQuestionarioReposta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlunoQuestionarioReposta_AlunoQuestionario_AlunoQuestionarioId",
                        column: x => x.AlunoQuestionarioId,
                        principalTable: "AlunoQuestionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Anexo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    Arquivo = table.Column<byte[]>(nullable: true),
                    ArquivoString = table.Column<string>(nullable: true),
                    DataAnexo = table.Column<DateTime>(nullable: false),
                    TipoAnexo = table.Column<int>(nullable: false),
                    Extensao = table.Column<string>(nullable: true),
                    UnidadeId = table.Column<int>(nullable: true),
                    AlunoId = table.Column<int>(nullable: true),
                    FuncionarioId = table.Column<int>(nullable: true),
                    FeriasFuncionarioId = table.Column<int>(nullable: true),
                    PontoEletronicoId = table.Column<int>(nullable: true),
                    MensagemTicketId = table.Column<int>(nullable: true),
                    FornecedorId = table.Column<int>(nullable: true),
                    FolhaPagamentoId = table.Column<int>(nullable: true),
                    DespesaId = table.Column<int>(nullable: true),
                    DestinatarioTicketId = table.Column<int>(nullable: true),
                    PerguntaId = table.Column<int>(nullable: true),
                    RespostaId = table.Column<int>(nullable: true),
                    MatriculaAlunoId = table.Column<int>(nullable: true),
                    MensagemAlunoProfessorId = table.Column<int>(nullable: true),
                    SolicitacaoId = table.Column<int>(nullable: true),
                    Mensagem = table.Column<string>(nullable: true),
                    IsRecusado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anexo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Anexo_Aluno_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Aluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Anexo_Despesa_DespesaId",
                        column: x => x.DespesaId,
                        principalTable: "Despesa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Anexo_DestinatarioTicket_DestinatarioTicketId",
                        column: x => x.DestinatarioTicketId,
                        principalTable: "DestinatarioTicket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Anexo_FeriasFuncionario_FeriasFuncionarioId",
                        column: x => x.FeriasFuncionarioId,
                        principalTable: "FeriasFuncionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Anexo_FolhaPagamento_FolhaPagamentoId",
                        column: x => x.FolhaPagamentoId,
                        principalTable: "FolhaPagamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Anexo_Fornecedor_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Anexo_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Anexo_MatriculaAluno_MatriculaAlunoId",
                        column: x => x.MatriculaAlunoId,
                        principalTable: "MatriculaAluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Anexo_MensagemAlunoProfessor_MensagemAlunoProfessorId",
                        column: x => x.MensagemAlunoProfessorId,
                        principalTable: "MensagemAlunoProfessor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Anexo_MensagemTicket_MensagemTicketId",
                        column: x => x.MensagemTicketId,
                        principalTable: "MensagemTicket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Anexo_Pergunta_PerguntaId",
                        column: x => x.PerguntaId,
                        principalTable: "Pergunta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Anexo_PontoEletronico_PontoEletronicoId",
                        column: x => x.PontoEletronicoId,
                        principalTable: "PontoEletronico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Anexo_Resposta_RespostaId",
                        column: x => x.RespostaId,
                        principalTable: "Resposta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Anexo_Solicitacao_SolicitacaoId",
                        column: x => x.SolicitacaoId,
                        principalTable: "Solicitacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Anexo_Unidade_UnidadeId",
                        column: x => x.UnidadeId,
                        principalTable: "Unidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgendaCurso_AgendaProvaId",
                table: "AgendaCurso",
                column: "AgendaProvaId");

            migrationBuilder.CreateIndex(
                name: "IX_AgendaCurso_CursoId",
                table: "AgendaCurso",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_AgendaProva_ColegioAutorizadoId",
                table: "AgendaProva",
                column: "ColegioAutorizadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_ContatoId",
                table: "Aluno",
                column: "ContatoId");

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_EnderecoId",
                table: "Aluno",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_NacionalidadeId",
                table: "Aluno",
                column: "NacionalidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_NaturalidadeId",
                table: "Aluno",
                column: "NaturalidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_UnidadeId",
                table: "Aluno",
                column: "UnidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_AlunoQuestionario_AlunoId",
                table: "AlunoQuestionario",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_AlunoQuestionario_PerguntaId",
                table: "AlunoQuestionario",
                column: "PerguntaId");

            migrationBuilder.CreateIndex(
                name: "IX_AlunoQuestionarioReposta_AlunoQuestionarioId",
                table: "AlunoQuestionarioReposta",
                column: "AlunoQuestionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Anexo_AlunoId",
                table: "Anexo",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Anexo_DespesaId",
                table: "Anexo",
                column: "DespesaId");

            migrationBuilder.CreateIndex(
                name: "IX_Anexo_DestinatarioTicketId",
                table: "Anexo",
                column: "DestinatarioTicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Anexo_FeriasFuncionarioId",
                table: "Anexo",
                column: "FeriasFuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Anexo_FolhaPagamentoId",
                table: "Anexo",
                column: "FolhaPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Anexo_FornecedorId",
                table: "Anexo",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Anexo_FuncionarioId",
                table: "Anexo",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Anexo_MatriculaAlunoId",
                table: "Anexo",
                column: "MatriculaAlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Anexo_MensagemAlunoProfessorId",
                table: "Anexo",
                column: "MensagemAlunoProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Anexo_MensagemTicketId",
                table: "Anexo",
                column: "MensagemTicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Anexo_PerguntaId",
                table: "Anexo",
                column: "PerguntaId");

            migrationBuilder.CreateIndex(
                name: "IX_Anexo_PontoEletronicoId",
                table: "Anexo",
                column: "PontoEletronicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Anexo_RespostaId",
                table: "Anexo",
                column: "RespostaId");

            migrationBuilder.CreateIndex(
                name: "IX_Anexo_SolicitacaoId",
                table: "Anexo",
                column: "SolicitacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Anexo_UnidadeId",
                table: "Anexo",
                column: "UnidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_CampanhaTipoPagamento_CampanhaId",
                table: "CampanhaTipoPagamento",
                column: "CampanhaId");

            migrationBuilder.CreateIndex(
                name: "IX_CentroCusto_UnidadeId",
                table: "CentroCusto",
                column: "UnidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificadoProva_MatriculaAlunoId",
                table: "CertificadoProva",
                column: "MatriculaAlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_ColegioAutorizado_EnderecoId",
                table: "ColegioAutorizado",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_ComissaoParcela_ComissoesId",
                table: "ComissaoParcela",
                column: "ComissoesId");

            migrationBuilder.CreateIndex(
                name: "IX_CursoOnline_AulaOnlineId",
                table: "CursoOnline",
                column: "AulaOnlineId");

            migrationBuilder.CreateIndex(
                name: "IX_CursoProfessor_FuncionarioId",
                table: "CursoProfessor",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Despesa_CategoriaId",
                table: "Despesa",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Despesa_CentroCustoId",
                table: "Despesa",
                column: "CentroCustoId");

            migrationBuilder.CreateIndex(
                name: "IX_Despesa_DespesaDARFId",
                table: "Despesa",
                column: "DespesaDARFId");

            migrationBuilder.CreateIndex(
                name: "IX_Despesa_DespesaGPSId",
                table: "Despesa",
                column: "DespesaGPSId");

            migrationBuilder.CreateIndex(
                name: "IX_Despesa_FornecedorId",
                table: "Despesa",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Despesa_UnidadeId",
                table: "Despesa",
                column: "UnidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_DespesaParcela_DespesaId",
                table: "DespesaParcela",
                column: "DespesaId");

            migrationBuilder.CreateIndex(
                name: "IX_DespesaParcela_UnidadeId",
                table: "DespesaParcela",
                column: "UnidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_DestinatarioTicket_DepartamentoId",
                table: "DestinatarioTicket",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_DestinatarioTicket_TicketId",
                table: "DestinatarioTicket",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_DestinatarioTicket_UnidadeId",
                table: "DestinatarioTicket",
                column: "UnidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailDestinatario_SolicitacaoId",
                table: "EmailDestinatario",
                column: "SolicitacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailEnviado_PagamentoId",
                table: "EmailEnviado",
                column: "PagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_FeriasFuncionario_FuncionarioId",
                table: "FeriasFuncionario",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_FolhaPagamento_FuncionarioId",
                table: "FolhaPagamento",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedor_CategoriaId",
                table: "Fornecedor",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedor_ContatoId",
                table: "Fornecedor",
                column: "ContatoId");

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedor_DadosBancarioId",
                table: "Fornecedor",
                column: "DadosBancarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedor_EnderecoId",
                table: "Fornecedor",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_AgenteIntegracaoId",
                table: "Funcionario",
                column: "AgenteIntegracaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_ContatoId",
                table: "Funcionario",
                column: "ContatoId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_DadosBancarioId",
                table: "Funcionario",
                column: "DadosBancarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_DadosContratacaoId",
                table: "Funcionario",
                column: "DadosContratacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_EnderecoId",
                table: "Funcionario",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_JornadaTrabalhoId",
                table: "Funcionario",
                column: "JornadaTrabalhoId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_MateriaProfessorId",
                table: "Funcionario",
                column: "MateriaProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoOcorrencias_UnidadeId",
                table: "HistoricoOcorrencias",
                column: "UnidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_HoraExtra_FolhaPagamentoId",
                table: "HoraExtra",
                column: "FolhaPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_HorarioFuncionamento_UnidadeId",
                table: "HorarioFuncionamento",
                column: "UnidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemProduto_ProdutoId",
                table: "ItemProduto",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Materia_CursoId",
                table: "Materia",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_MateriaCursoProfessor_CursoProfessorId",
                table: "MateriaCursoProfessor",
                column: "CursoProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_MateriaOnline_AulaOnlineId",
                table: "MateriaOnline",
                column: "AulaOnlineId");

            migrationBuilder.CreateIndex(
                name: "IX_MatriculaAluno_AlunoId",
                table: "MatriculaAluno",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_MatriculaAluno_CursoId",
                table: "MatriculaAluno",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_MatriculaAluno_PlanoPagamentoAlunoId",
                table: "MatriculaAluno",
                column: "PlanoPagamentoAlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_MatriculaAluno_TurmaId",
                table: "MatriculaAluno",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_MatriculaAluno_UnidadeId",
                table: "MatriculaAluno",
                column: "UnidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_MensagemAlunoProfessor_MatriculaAlunoId",
                table: "MensagemAlunoProfessor",
                column: "MatriculaAlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_MensagemTicket_FuncionarioId",
                table: "MensagemTicket",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Meta_UnidadeId",
                table: "Meta",
                column: "UnidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_MetaPeriodo_MetaId",
                table: "MetaPeriodo",
                column: "MetaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamento_DadosCartaoId",
                table: "Pagamento",
                column: "DadosCartaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pergunta_VideoAulaId",
                table: "Pergunta",
                column: "VideoAulaId");

            migrationBuilder.CreateIndex(
                name: "IX_PontoEletronico_FuncionarioId",
                table: "PontoEletronico",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_UnidadeId",
                table: "Produto",
                column: "UnidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProvaAluno_MatriculaAlunoId",
                table: "ProvaAluno",
                column: "MatriculaAlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Resposta_PerguntaId",
                table: "Resposta",
                column: "PerguntaId");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitacao_CentroCustoId",
                table: "Solicitacao",
                column: "CentroCustoId");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitacao_UnidadeId",
                table: "Solicitacao",
                column: "UnidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoAluno_MatriculaAlunoId",
                table: "SolicitacaoAluno",
                column: "MatriculaAlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoAluno_SolicitacaoEmailId",
                table: "SolicitacaoAluno",
                column: "SolicitacaoEmailId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoAluno_SolicitacaoId",
                table: "SolicitacaoAluno",
                column: "SolicitacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoCurso_CursoId",
                table: "SolicitacaoCurso",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoCurso_SolicitacaoId",
                table: "SolicitacaoCurso",
                column: "SolicitacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoFuncionarioTicket_FuncionarioId",
                table: "SolicitacaoFuncionarioTicket",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoFuncionarioTicket_SolicitacaoId",
                table: "SolicitacaoFuncionarioTicket",
                column: "SolicitacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_StatusCertificado_SolicitacaoId",
                table: "StatusCertificado",
                column: "SolicitacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_StatusProva_SolicitacaoId",
                table: "StatusProva",
                column: "SolicitacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_AssuntoTicketId",
                table: "Ticket",
                column: "AssuntoTicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Unidade_ContatoId",
                table: "Unidade",
                column: "ContatoId");

            migrationBuilder.CreateIndex(
                name: "IX_Unidade_ContratoLocacaoId",
                table: "Unidade",
                column: "ContratoLocacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Unidade_DadosBancarioId",
                table: "Unidade",
                column: "DadosBancarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Unidade_EnderecoId",
                table: "Unidade",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_UnidadeDespesa_UnidadeId",
                table: "UnidadeDespesa",
                column: "UnidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_UnidadeParticipanteProva_AgendaProvaId",
                table: "UnidadeParticipanteProva",
                column: "AgendaProvaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_AlunoId",
                table: "Usuario",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_DepartamentoId",
                table: "Usuario",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_FuncionarioId",
                table: "Usuario",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_PerfilUsuarioId",
                table: "Usuario",
                column: "PerfilUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_UnidadeId",
                table: "Usuario",
                column: "UnidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioDestinarioTicket_DestinatarioTicketId",
                table: "UsuarioDestinarioTicket",
                column: "DestinatarioTicketId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoAula_MateriaOnlineId",
                table: "VideoAula",
                column: "MateriaOnlineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgendaCurso");

            migrationBuilder.DropTable(
                name: "AlunoQuestionarioReposta");

            migrationBuilder.DropTable(
                name: "Anexo");

            migrationBuilder.DropTable(
                name: "ArquivoPonto");

            migrationBuilder.DropTable(
                name: "CampanhaTipoPagamento");

            migrationBuilder.DropTable(
                name: "CampanhaUnidade");

            migrationBuilder.DropTable(
                name: "CertificadoProva");

            migrationBuilder.DropTable(
                name: "ComissaoParcela");

            migrationBuilder.DropTable(
                name: "ComissaoUnidade");

            migrationBuilder.DropTable(
                name: "CursoOnline");

            migrationBuilder.DropTable(
                name: "DespesaParcela");

            migrationBuilder.DropTable(
                name: "EmailDestinatario");

            migrationBuilder.DropTable(
                name: "EmailEnviado");

            migrationBuilder.DropTable(
                name: "HistoricoEstoque");

            migrationBuilder.DropTable(
                name: "HistoricoOcorrencias");

            migrationBuilder.DropTable(
                name: "HoraExtra");

            migrationBuilder.DropTable(
                name: "HorarioFuncionamento");

            migrationBuilder.DropTable(
                name: "InstituicaoBancaria");

            migrationBuilder.DropTable(
                name: "ItemProduto");

            migrationBuilder.DropTable(
                name: "Materia");

            migrationBuilder.DropTable(
                name: "MateriaCursoProfessor");

            migrationBuilder.DropTable(
                name: "MetaPeriodo");

            migrationBuilder.DropTable(
                name: "MetaUnidade");

            migrationBuilder.DropTable(
                name: "Parametro");

            migrationBuilder.DropTable(
                name: "PlanoPagamentoCurso");

            migrationBuilder.DropTable(
                name: "PlanoPagamentoUnidade");

            migrationBuilder.DropTable(
                name: "ProvaAluno");

            migrationBuilder.DropTable(
                name: "SalarioUnidade");

            migrationBuilder.DropTable(
                name: "SolicitacaoAluno");

            migrationBuilder.DropTable(
                name: "SolicitacaoCurso");

            migrationBuilder.DropTable(
                name: "SolicitacaoFuncionarioTicket");

            migrationBuilder.DropTable(
                name: "StatusCertificado");

            migrationBuilder.DropTable(
                name: "StatusProva");

            migrationBuilder.DropTable(
                name: "TurmaCurso");

            migrationBuilder.DropTable(
                name: "TurmaUnidade");

            migrationBuilder.DropTable(
                name: "UnidadeDespesa");

            migrationBuilder.DropTable(
                name: "UnidadeParticipanteProva");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "UsuarioDestinarioTicket");

            migrationBuilder.DropTable(
                name: "VideoPausado");

            migrationBuilder.DropTable(
                name: "AlunoQuestionario");

            migrationBuilder.DropTable(
                name: "FeriasFuncionario");

            migrationBuilder.DropTable(
                name: "MensagemAlunoProfessor");

            migrationBuilder.DropTable(
                name: "MensagemTicket");

            migrationBuilder.DropTable(
                name: "PontoEletronico");

            migrationBuilder.DropTable(
                name: "Resposta");

            migrationBuilder.DropTable(
                name: "Campanha");

            migrationBuilder.DropTable(
                name: "Comissoes");

            migrationBuilder.DropTable(
                name: "Despesa");

            migrationBuilder.DropTable(
                name: "Pagamento");

            migrationBuilder.DropTable(
                name: "FolhaPagamento");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "CursoProfessor");

            migrationBuilder.DropTable(
                name: "Meta");

            migrationBuilder.DropTable(
                name: "PlanoPagamento");

            migrationBuilder.DropTable(
                name: "SolicitacaoEmail");

            migrationBuilder.DropTable(
                name: "Solicitacao");

            migrationBuilder.DropTable(
                name: "AgendaProva");

            migrationBuilder.DropTable(
                name: "PerfilUsuario");

            migrationBuilder.DropTable(
                name: "DestinatarioTicket");

            migrationBuilder.DropTable(
                name: "MatriculaAluno");

            migrationBuilder.DropTable(
                name: "Pergunta");

            migrationBuilder.DropTable(
                name: "DespesaDARF");

            migrationBuilder.DropTable(
                name: "DespesaGPS");

            migrationBuilder.DropTable(
                name: "Fornecedor");

            migrationBuilder.DropTable(
                name: "DadosCartao");

            migrationBuilder.DropTable(
                name: "Funcionario");

            migrationBuilder.DropTable(
                name: "ColegioAutorizado");

            migrationBuilder.DropTable(
                name: "CentroCusto");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "Aluno");

            migrationBuilder.DropTable(
                name: "Curso");

            migrationBuilder.DropTable(
                name: "PlanoPagamentoAluno");

            migrationBuilder.DropTable(
                name: "Turma");

            migrationBuilder.DropTable(
                name: "VideoAula");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "AgenteIntegracao");

            migrationBuilder.DropTable(
                name: "DadosContratacao");

            migrationBuilder.DropTable(
                name: "JornadaTrabalho");

            migrationBuilder.DropTable(
                name: "MateriaProfessor");

            migrationBuilder.DropTable(
                name: "AssuntoTicket");

            migrationBuilder.DropTable(
                name: "Nacionalidade");

            migrationBuilder.DropTable(
                name: "Naturalidade");

            migrationBuilder.DropTable(
                name: "Unidade");

            migrationBuilder.DropTable(
                name: "MateriaOnline");

            migrationBuilder.DropTable(
                name: "Contato");

            migrationBuilder.DropTable(
                name: "ContratoLocacao");

            migrationBuilder.DropTable(
                name: "DadosBancario");

            migrationBuilder.DropTable(
                name: "Endereco");

            migrationBuilder.DropTable(
                name: "AulaOnline");
        }
    }
}
