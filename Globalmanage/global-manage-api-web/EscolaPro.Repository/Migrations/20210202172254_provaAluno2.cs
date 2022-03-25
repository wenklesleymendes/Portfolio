using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class provaAluno2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusProva",
                table: "ProvaAluno",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TipoProva",
                table: "ProvaAluno",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioLogadoId",
                table: "ProvaAluno",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusProva",
                table: "ProvaAluno");

            migrationBuilder.DropColumn(
                name: "TipoProva",
                table: "ProvaAluno");

            migrationBuilder.DropColumn(
                name: "UsuarioLogadoId",
                table: "ProvaAluno");
        }
    }
}
