using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class AdiçãoTipoUnidade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "FuncionarioId",
            //    table: "FuncionarioAssuntoTicket");

            migrationBuilder.AddColumn<int>(
                name: "TipoUnidade",
                table: "Unidade",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.AddColumn<int>(
            //    name: "UsuarioId",
            //    table: "FuncionarioAssuntoTicket",
            //    nullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_FuncionarioAssuntoTicket_UsuarioId",
            //    table: "FuncionarioAssuntoTicket",
            //    column: "UsuarioId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_FuncionarioAssuntoTicket_Usuario_UsuarioId",
            //    table: "FuncionarioAssuntoTicket",
            //    column: "UsuarioId",
            //    principalTable: "Usuario",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_FuncionarioAssuntoTicket_Usuario_UsuarioId",
            //    table: "FuncionarioAssuntoTicket");

            //migrationBuilder.DropIndex(
            //    name: "IX_FuncionarioAssuntoTicket_UsuarioId",
            //    table: "FuncionarioAssuntoTicket");

            migrationBuilder.DropColumn(
                name: "TipoUnidade",
                table: "Unidade");

            //migrationBuilder.DropColumn(
            //    name: "UsuarioId",
            //    table: "FuncionarioAssuntoTicket");

            //migrationBuilder.AddColumn<int>(
            //    name: "FuncionarioId",
            //    table: "FuncionarioAssuntoTicket",
            //    type: "int",
            //    nullable: true);
        }
    }
}
