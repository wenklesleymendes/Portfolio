using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class Ajuste2AdministracaoTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DestinatarioTicketId",
                table: "FuncionarioAssuntoTicket");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DestinatarioTicketId",
                table: "FuncionarioAssuntoTicket",
                type: "int",
                nullable: true);
        }
    }
}
