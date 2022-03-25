using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class AddCamposAgendamentoOutaboud4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataeHoradoAgendamento",
                table: "AtendimentoOutbound",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MotivodoNaoAgendamento",
                table: "AtendimentoOutbound",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MotivodoNaoAgendamento",
                table: "Atendimento",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataeHoradoAgendamento",
                table: "AtendimentoOutbound");

            migrationBuilder.DropColumn(
                name: "MotivodoNaoAgendamento",
                table: "AtendimentoOutbound");

            migrationBuilder.DropColumn(
                name: "MotivodoNaoAgendamento",
                table: "Atendimento");
        }
    }
}
