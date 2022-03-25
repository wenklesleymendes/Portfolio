using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class AjusteModeloCancelamentoMatricula2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusCancelamento",
                table: "CancelamentoMatricula",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<int>(
                name: "UsarioId",
                table: "CancelamentoIsencao",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
               name: "StatusCancelamento",
               table: "CancelamentoMatricula");
            migrationBuilder.DropColumn(
               name: "UsarioId",
               table: "CancelamentoIsencao");
        }
    }
}
