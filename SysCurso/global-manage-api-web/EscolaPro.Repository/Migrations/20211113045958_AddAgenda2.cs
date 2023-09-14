using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class AddAgenda2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "AtendimentoScore");

            migrationBuilder.DropColumn(
                name: "ValorScore",
                table: "AtendimentoScore");

            migrationBuilder.AddColumn<string>(
                name: "DataAgendamento",
                table: "AtendimentoScore",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataeHoradoUltimoContato",
                table: "AtendimentoScore",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "HoraAgendamento",
                table: "AtendimentoScore",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdAtendimento",
                table: "AtendimentoScore",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TipoAgendamento",
                table: "AtendimentoScore",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataAgendamento",
                table: "AtendimentoScore");

            migrationBuilder.DropColumn(
                name: "DataeHoradoUltimoContato",
                table: "AtendimentoScore");

            migrationBuilder.DropColumn(
                name: "HoraAgendamento",
                table: "AtendimentoScore");

            migrationBuilder.DropColumn(
                name: "IdAtendimento",
                table: "AtendimentoScore");

            migrationBuilder.DropColumn(
                name: "TipoAgendamento",
                table: "AtendimentoScore");

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "AtendimentoScore",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ValorScore",
                table: "AtendimentoScore",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
