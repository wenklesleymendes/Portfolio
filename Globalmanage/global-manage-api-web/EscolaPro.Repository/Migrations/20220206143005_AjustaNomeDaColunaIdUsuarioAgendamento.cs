using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class AjustaNomeDaColunaIdUsuarioAgendamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "idUsuarioAgendamento",
                table: "AtendimentoAgendamento");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioCadastro",
                table: "AtendimentoAgendamento",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioCadastro",
                table: "AtendimentoAgendamento");

            migrationBuilder.AddColumn<int>(
                name: "idUsuarioAgendamento",
                table: "AtendimentoAgendamento",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
