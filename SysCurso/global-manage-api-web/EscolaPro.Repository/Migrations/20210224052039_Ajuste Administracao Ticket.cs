using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class AjusteAdministracaoTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FuncionarioAssuntoTicket_AssuntoTicket_AssuntoTicketId",
                table: "FuncionarioAssuntoTicket");

            migrationBuilder.DropForeignKey(
                name: "FK_FuncionarioAssuntoTicket_Funcionario_FuncionarioId",
                table: "FuncionarioAssuntoTicket");

            migrationBuilder.DropIndex(
                name: "IX_FuncionarioAssuntoTicket_FuncionarioId",
                table: "FuncionarioAssuntoTicket");

            migrationBuilder.AlterColumn<int>(
                name: "FuncionarioId",
                table: "FuncionarioAssuntoTicket",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AssuntoTicketId",
                table: "FuncionarioAssuntoTicket",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DestinatarioTicketId",
                table: "FuncionarioAssuntoTicket",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FuncionarioAssuntoTicket_AssuntoTicket_AssuntoTicketId",
                table: "FuncionarioAssuntoTicket",
                column: "AssuntoTicketId",
                principalTable: "AssuntoTicket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FuncionarioAssuntoTicket_AssuntoTicket_AssuntoTicketId",
                table: "FuncionarioAssuntoTicket");

            migrationBuilder.DropColumn(
                name: "DestinatarioTicketId",
                table: "FuncionarioAssuntoTicket");

            migrationBuilder.AlterColumn<int>(
                name: "FuncionarioId",
                table: "FuncionarioAssuntoTicket",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AssuntoTicketId",
                table: "FuncionarioAssuntoTicket",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FuncionarioAssuntoTicket_FuncionarioId",
                table: "FuncionarioAssuntoTicket",
                column: "FuncionarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_FuncionarioAssuntoTicket_AssuntoTicket_AssuntoTicketId",
                table: "FuncionarioAssuntoTicket",
                column: "AssuntoTicketId",
                principalTable: "AssuntoTicket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FuncionarioAssuntoTicket_Funcionario_FuncionarioId",
                table: "FuncionarioAssuntoTicket",
                column: "FuncionarioId",
                principalTable: "Funcionario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
