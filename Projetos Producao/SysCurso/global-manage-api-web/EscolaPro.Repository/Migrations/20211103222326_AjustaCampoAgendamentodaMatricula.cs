using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class AjustaCampoAgendamentodaMatricula : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegraAplicada",
                table: "Atendimento");

            migrationBuilder.AddColumn<int>(
                name: "AgendamentodaMatricula",
                table: "Atendimento",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgendamentodaMatricula",
                table: "Atendimento");

            migrationBuilder.AddColumn<int>(
                name: "RegraAplicada",
                table: "Atendimento",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
