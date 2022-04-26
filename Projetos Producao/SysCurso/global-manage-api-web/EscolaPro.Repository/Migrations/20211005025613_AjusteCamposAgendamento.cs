using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class AjusteCamposAgendamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           migrationBuilder.AddColumn<int>(
                name: "AgendamentodaMatricula",
                table: "Atendimento",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataeHora",
                table: "Atendimento",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DiadoAgendamento",
                table: "Atendimento",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "HoradoAgendamento",
                table: "Atendimento",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgendamentodaMatricula",
                table: "Atendimento");

            migrationBuilder.DropColumn(
                name: "DataeHora",
                table: "Atendimento");

            migrationBuilder.DropColumn(
                name: "DiadoAgendamento",
                table: "Atendimento");

            migrationBuilder.DropColumn(
                name: "HoradoAgendamento",
                table: "Atendimento");
        }
    }
}
