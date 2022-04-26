using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class ModificaCampoMatriculaId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatriculaId",
                table: "Atendimento");

            migrationBuilder.AddColumn<bool>(
                name: "ExisteMatricula",
                table: "Atendimento",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExisteMatricula",
                table: "Atendimento");

            migrationBuilder.AddColumn<int>(
                name: "MatriculaId",
                table: "Atendimento",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
