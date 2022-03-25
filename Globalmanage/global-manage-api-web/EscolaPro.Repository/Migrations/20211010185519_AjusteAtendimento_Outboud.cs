using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class AjusteAtendimento_Outboud : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "MotivodoNaoAgendamento",
                table: "Atendimento");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Atendimento");

            migrationBuilder.AlterColumn<bool>(
                name: "MatriculaAgendada",
                table: "AtendimentoOutbound",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataeHoraAgendamento",
                table: "AtendimentoOutbound",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MotivodoNaoAgendamento",
                table: "AtendimentoOutbound",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ScoreAplicado",
                table: "AtendimentoOutbound",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ScoreInicial",
                table: "AtendimentoOutbound",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataeHoradoAgendamento",
                table: "Atendimento",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataeHoradoAtendimento",
                table: "Atendimento",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataeHoradoContato",
                table: "Atendimento",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmContato",
                table: "Atendimento",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MatriculaAgendada",
                table: "Atendimento",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MatriculaId",
                table: "Atendimento",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataeHoraAgendamento",
                table: "AtendimentoOutbound");

            migrationBuilder.DropColumn(
                name: "MotivodoNaoAgendamento",
                table: "AtendimentoOutbound");

            migrationBuilder.DropColumn(
                name: "ScoreAplicado",
                table: "AtendimentoOutbound");

            migrationBuilder.DropColumn(
                name: "ScoreInicial",
                table: "AtendimentoOutbound");

            migrationBuilder.DropColumn(
                name: "DataeHoradoAgendamento",
                table: "Atendimento");

            migrationBuilder.DropColumn(
                name: "DataeHoradoAtendimento",
                table: "Atendimento");

            migrationBuilder.DropColumn(
                name: "DataeHoradoContato",
                table: "Atendimento");

            migrationBuilder.DropColumn(
                name: "EmContato",
                table: "Atendimento");

            migrationBuilder.DropColumn(
                name: "MatriculaAgendada",
                table: "Atendimento");

            migrationBuilder.DropColumn(
                name: "MatriculaId",
                table: "Atendimento");

            migrationBuilder.AlterColumn<string>(
                name: "MatriculaAgendada",
                table: "AtendimentoOutbound",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AddColumn<int>(
                name: "AgendamentodaMatricula",
                table: "Atendimento",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DataeHora",
                table: "Atendimento",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiadoAgendamento",
                table: "Atendimento",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HoradoAgendamento",
                table: "Atendimento",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MotivodoNaoAgendamento",
                table: "Atendimento",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Atendimento",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
