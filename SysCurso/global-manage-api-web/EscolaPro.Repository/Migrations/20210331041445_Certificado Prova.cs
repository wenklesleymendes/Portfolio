using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class CertificadoProva : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProvaMateriaAluno_ProvaAluno_ProvaAlunoId",
                table: "ProvaMateriaAluno");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "ProvaMateriaAluno",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProvaAlunoId",
                table: "ProvaMateriaAluno",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnexoId",
                table: "CertificadoProva",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MatriculaId",
                table: "CertificadoProva",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusCertificado",
                table: "CertificadoProva",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ProvaMateriaAluno_ProvaAluno_ProvaAlunoId",
                table: "ProvaMateriaAluno",
                column: "ProvaAlunoId",
                principalTable: "ProvaAluno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProvaMateriaAluno_ProvaAluno_ProvaAlunoId",
                table: "ProvaMateriaAluno");

            migrationBuilder.DropColumn(
                name: "AnexoId",
                table: "CertificadoProva");

            migrationBuilder.DropColumn(
                name: "MatriculaId",
                table: "CertificadoProva");

            migrationBuilder.DropColumn(
                name: "StatusCertificado",
                table: "CertificadoProva");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "ProvaMateriaAluno",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<int>(
                name: "ProvaAlunoId",
                table: "ProvaMateriaAluno",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_ProvaMateriaAluno_ProvaAluno_ProvaAlunoId",
                table: "ProvaMateriaAluno",
                column: "ProvaAlunoId",
                principalTable: "ProvaAluno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
