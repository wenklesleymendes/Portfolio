using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class RemoverCampoDublicadoCAncelamentoMatricula : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Usuario",
                table: "CancelamentoMatricula");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Usuario",
                table: "CancelamentoMatricula",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
