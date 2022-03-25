using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class modificaNome_Operador_Usuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OperadorCadastro",
                table: "AtendimentoOutbound");

            migrationBuilder.DropColumn(
                name: "OperadorLogado",
                table: "AtendimentoOutbound");

            migrationBuilder.DropColumn(
                name: "OperadorCadastro",
                table: "Atendimento");

            migrationBuilder.DropColumn(
                name: "OperadorLogado",
                table: "Atendimento");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioCadastro",
                table: "AtendimentoOutbound",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioLogado",
                table: "AtendimentoOutbound",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioCadastro",
                table: "Atendimento",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioLogado",
                table: "Atendimento",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioCadastro",
                table: "AtendimentoOutbound");

            migrationBuilder.DropColumn(
                name: "UsuarioLogado",
                table: "AtendimentoOutbound");

            migrationBuilder.DropColumn(
                name: "UsuarioCadastro",
                table: "Atendimento");

            migrationBuilder.DropColumn(
                name: "UsuarioLogado",
                table: "Atendimento");

            migrationBuilder.AddColumn<int>(
                name: "OperadorCadastro",
                table: "AtendimentoOutbound",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OperadorLogado",
                table: "AtendimentoOutbound",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OperadorCadastro",
                table: "Atendimento",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OperadorLogado",
                table: "Atendimento",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
