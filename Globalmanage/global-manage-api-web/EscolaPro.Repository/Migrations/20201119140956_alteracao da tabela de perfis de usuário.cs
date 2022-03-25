using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class alteracaodatabeladeperfisdeusuário : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProvaAluno_MatriculaAluno_MatriculaAlunoId",
                table: "ProvaAluno");

            migrationBuilder.DropColumn(
                name: "CadastroFornecedor",
                table: "PerfilUsuario");

            migrationBuilder.AlterColumn<int>(
                name: "MatriculaAlunoId",
                table: "ProvaAluno",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AgendaProvaId",
                table: "ProvaAluno",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Observacao",
                table: "ProvaAluno",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CadastroFornecedorCliente",
                table: "PerfilUsuario",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CadastroFuncionario",
                table: "PerfilUsuario",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ConfiguradorParametros",
                table: "PerfilUsuario",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ControlePonto",
                table: "PerfilUsuario",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CriarColegioAutorizado",
                table: "PerfilUsuario",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EscalaServico",
                table: "PerfilUsuario",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Solicitacao",
                table: "PerfilUsuario",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UploadPonto",
                table: "PerfilUsuario",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ProvaMateriaAluno",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    NomeMateria = table.Column<string>(nullable: true),
                    Aprovado = table.Column<bool>(nullable: false),
                    ProvaAlunoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvaMateriaAluno", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProvaMateriaAluno_ProvaAluno_ProvaAlunoId",
                        column: x => x.ProvaAlunoId,
                        principalTable: "ProvaAluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProvaAluno_AgendaProvaId",
                table: "ProvaAluno",
                column: "AgendaProvaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProvaMateriaAluno_ProvaAlunoId",
                table: "ProvaMateriaAluno",
                column: "ProvaAlunoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProvaAluno_AgendaProva_AgendaProvaId",
                table: "ProvaAluno",
                column: "AgendaProvaId",
                principalTable: "AgendaProva",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProvaAluno_MatriculaAluno_MatriculaAlunoId",
                table: "ProvaAluno",
                column: "MatriculaAlunoId",
                principalTable: "MatriculaAluno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProvaAluno_AgendaProva_AgendaProvaId",
                table: "ProvaAluno");

            migrationBuilder.DropForeignKey(
                name: "FK_ProvaAluno_MatriculaAluno_MatriculaAlunoId",
                table: "ProvaAluno");

            migrationBuilder.DropTable(
                name: "ProvaMateriaAluno");

            migrationBuilder.DropIndex(
                name: "IX_ProvaAluno_AgendaProvaId",
                table: "ProvaAluno");

            migrationBuilder.DropColumn(
                name: "AgendaProvaId",
                table: "ProvaAluno");

            migrationBuilder.DropColumn(
                name: "Observacao",
                table: "ProvaAluno");

            migrationBuilder.DropColumn(
                name: "CadastroFornecedorCliente",
                table: "PerfilUsuario");

            migrationBuilder.DropColumn(
                name: "CadastroFuncionario",
                table: "PerfilUsuario");

            migrationBuilder.DropColumn(
                name: "ConfiguradorParametros",
                table: "PerfilUsuario");

            migrationBuilder.DropColumn(
                name: "ControlePonto",
                table: "PerfilUsuario");

            migrationBuilder.DropColumn(
                name: "CriarColegioAutorizado",
                table: "PerfilUsuario");

            migrationBuilder.DropColumn(
                name: "EscalaServico",
                table: "PerfilUsuario");

            migrationBuilder.DropColumn(
                name: "Solicitacao",
                table: "PerfilUsuario");

            migrationBuilder.DropColumn(
                name: "UploadPonto",
                table: "PerfilUsuario");

            migrationBuilder.AlterColumn<int>(
                name: "MatriculaAlunoId",
                table: "ProvaAluno",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<bool>(
                name: "CadastroFornecedor",
                table: "PerfilUsuario",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_ProvaAluno_MatriculaAluno_MatriculaAlunoId",
                table: "ProvaAluno",
                column: "MatriculaAlunoId",
                principalTable: "MatriculaAluno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
