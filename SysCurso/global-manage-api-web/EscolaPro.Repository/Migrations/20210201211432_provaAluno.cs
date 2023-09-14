using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class provaAluno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TipoTransporte",
                table: "ProvaAluno",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataProva",
                table: "ProvaAluno",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "ColegioAutorizadoId",
                table: "ProvaAluno",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInscricao",
                table: "ProvaAluno",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentificacaoUsuario",
                table: "ProvaAluno",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SenhaProva",
                table: "ProvaAluno",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProvaAluno_ColegioAutorizadoId",
                table: "ProvaAluno",
                column: "ColegioAutorizadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProvaAluno_ColegioAutorizado_ColegioAutorizadoId",
                table: "ProvaAluno",
                column: "ColegioAutorizadoId",
                principalTable: "ColegioAutorizado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProvaAluno_ColegioAutorizado_ColegioAutorizadoId",
                table: "ProvaAluno");

            migrationBuilder.DropIndex(
                name: "IX_ProvaAluno_ColegioAutorizadoId",
                table: "ProvaAluno");

            migrationBuilder.DropColumn(
                name: "ColegioAutorizadoId",
                table: "ProvaAluno");

            migrationBuilder.DropColumn(
                name: "DataInscricao",
                table: "ProvaAluno");

            migrationBuilder.DropColumn(
                name: "IdentificacaoUsuario",
                table: "ProvaAluno");

            migrationBuilder.DropColumn(
                name: "SenhaProva",
                table: "ProvaAluno");

            migrationBuilder.AlterColumn<int>(
                name: "TipoTransporte",
                table: "ProvaAluno",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataProva",
                table: "ProvaAluno",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
