using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class AjustaNomeCampo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatriculaAgendada",
                table: "Atendimento");

            migrationBuilder.AddColumn<bool>(
                name: "ExisteAgendamento",
                table: "Atendimento",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExisteAgendamento",
                table: "Atendimento");

            migrationBuilder.AddColumn<bool>(
                name: "MatriculaAgendada",
                table: "Atendimento",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
