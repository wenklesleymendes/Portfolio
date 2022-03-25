using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class criacaodomatriculaIdnatabeladeticket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlunoId",
                table: "Ticket");

            migrationBuilder.AddColumn<int>(
                name: "MatriculaId",
                table: "Ticket",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatriculaId",
                table: "Ticket");

            migrationBuilder.AddColumn<int>(
                name: "AlunoId",
                table: "Ticket",
                type: "int",
                nullable: true);
        }
    }
}
