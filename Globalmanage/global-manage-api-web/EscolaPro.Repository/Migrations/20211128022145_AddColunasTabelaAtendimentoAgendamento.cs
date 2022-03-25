using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class AddColunasTabelaAtendimentoAgendamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Observacoes",
                table: "AtendimentoAgendamento",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Situacao",
                table: "AtendimentoAgendamento",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "Observacoes",
                table: "AtendimentoAgendamento");

            migrationBuilder.DropColumn(
                name: "Situacao",
                table: "AtendimentoAgendamento");
        }
    }
}
