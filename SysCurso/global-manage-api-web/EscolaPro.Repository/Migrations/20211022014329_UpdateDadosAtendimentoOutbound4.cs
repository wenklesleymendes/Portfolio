using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class UpdateDadosAtendimentoOutbound4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataContato",
                table: "AtendimentoOutbound");

            migrationBuilder.DropColumn(
                name: "DataeHoraAgendamento",
                table: "AtendimentoOutbound");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataHoraContato",
                table: "AtendimentoOutbound",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataHoraContato",
                table: "AtendimentoOutbound");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataContato",
                table: "AtendimentoOutbound",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataeHoraAgendamento",
                table: "AtendimentoOutbound",
                type: "datetime2",
                nullable: true);
        }
    }
}
