using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class UpdateDadosAtendimentoOutbound5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmContato",
                table: "Atendimento");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Atendimento",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Atendimento");

            migrationBuilder.AddColumn<bool>(
                name: "EmContato",
                table: "Atendimento",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
