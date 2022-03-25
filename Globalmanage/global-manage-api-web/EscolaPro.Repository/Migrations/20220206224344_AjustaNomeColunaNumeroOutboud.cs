using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class AjustaNomeColunaNumeroOutboud : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.RenameColumn(
                name: "NumeroTentativa",
                table: "AtendimentoOutbound",
                newName: "NumeroOutbound");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumeroOutbound",
                table: "AtendimentoOutbound",
                newName: "NumeroTentativa");
        }
    }
}
