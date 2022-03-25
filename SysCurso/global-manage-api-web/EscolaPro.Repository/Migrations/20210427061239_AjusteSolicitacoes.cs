using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class AjusteSolicitacoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioLogadoId",
                table: "SolicitacaoAluno",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "EnviaEmailPosPgto",
                table: "Solicitacao",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EnviaTicketPosPgto",
                table: "Solicitacao",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "HistoricoProvas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    parceiro = table.Column<string>(nullable: true),
                    nomecompleto = table.Column<string>(nullable: true),
                    datadenascimento = table.Column<string>(nullable: true),
                    sexo = table.Column<string>(nullable: true),
                    estadocivil = table.Column<string>(nullable: true),
                    naturalidade = table.Column<string>(nullable: true),
                    nacionalidade = table.Column<string>(nullable: true),
                    nomedamae = table.Column<string>(nullable: true),
                    nomedopai = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    rg = table.Column<string>(nullable: true),
                    orgaoexpedidor = table.Column<string>(nullable: true),
                    ufrg = table.Column<string>(nullable: true),
                    cpf = table.Column<string>(nullable: true),
                    titulodeeleitor = table.Column<string>(nullable: true),
                    zonaeleitoral = table.Column<string>(nullable: true),
                    secaoeleitoral = table.Column<string>(nullable: true),
                    enderecoresidencial = table.Column<string>(nullable: true),
                    numeroresidencial = table.Column<string>(nullable: true),
                    complementoresidencial = table.Column<string>(nullable: true),
                    bairroresidencial = table.Column<string>(nullable: true),
                    cidaderesidencial = table.Column<string>(nullable: true),
                    ufresidencial = table.Column<string>(nullable: true),
                    cepresidencial = table.Column<string>(nullable: true),
                    dddfixoresidencial = table.Column<string>(nullable: true),
                    telefonefixoresidencial = table.Column<string>(nullable: true),
                    dddcelularresidencial = table.Column<string>(nullable: true),
                    celularresidencial = table.Column<string>(nullable: true),
                    curso = table.Column<string>(nullable: true),
                    statusprova = table.Column<string>(nullable: true),
                    unidade = table.Column<string>(nullable: true),
                    docconferidopor = table.Column<string>(nullable: true),
                    dataconferenciadoc = table.Column<string>(nullable: true),
                    emailporcpf = table.Column<string>(nullable: true),
                    UnidadeTransporteProvaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoProvas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricoProvas_UnidadeTransporteProva_UnidadeTransporteProvaId",
                        column: x => x.UnidadeTransporteProvaId,
                        principalTable: "UnidadeTransporteProva",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoProvas_UnidadeTransporteProvaId",
                table: "HistoricoProvas",
                column: "UnidadeTransporteProvaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricoProvas");

            migrationBuilder.DropColumn(
                name: "UsuarioLogadoId",
                table: "SolicitacaoAluno");

            migrationBuilder.DropColumn(
                name: "EnviaEmailPosPgto",
                table: "Solicitacao");

            migrationBuilder.DropColumn(
                name: "EnviaTicketPosPgto",
                table: "Solicitacao");
        }
    }
}
