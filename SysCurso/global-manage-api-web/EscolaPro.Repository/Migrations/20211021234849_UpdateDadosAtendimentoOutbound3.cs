using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class UpdateDadosAtendimentoOutbound3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MotivodoNaoAgendamento",
                table: "AtendimentoOutbound");

            migrationBuilder.AlterColumn<int>(
                name: "MatriculaAgendada",
                table: "AtendimentoOutbound",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "MatriculaAgendada",
                table: "AtendimentoOutbound",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "MotivodoNaoAgendamento",
                table: "AtendimentoOutbound",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
