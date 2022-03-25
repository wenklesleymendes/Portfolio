using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class MelhoraDesempenhoTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Ticket_MatriculaId",
                table: "Ticket",
                column: "MatriculaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_MatriculaAluno_MatriculaId",
                table: "Ticket",
                column: "MatriculaId",
                principalTable: "MatriculaAluno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_MatriculaAluno_MatriculaId",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_MatriculaId",
                table: "Ticket");
        }
    }
}
