using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class AjusteNomeReguaContatoParametro2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReguaContatoParamero_ReguaContatoRegras_ReguaContatoRegraId",
                table: "ReguaContatoParamero");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReguaContatoParamero",
                table: "ReguaContatoParamero");

            migrationBuilder.RenameTable(
                name: "ReguaContatoParamero",
                newName: "ReguaContatoParametro");

            migrationBuilder.RenameIndex(
                name: "IX_ReguaContatoParamero_ReguaContatoRegraId",
                table: "ReguaContatoParametro",
                newName: "IX_ReguaContatoParametro_ReguaContatoRegraId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReguaContatoParametro",
                table: "ReguaContatoParametro",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReguaContatoParametro_ReguaContatoRegras_ReguaContatoRegraId",
                table: "ReguaContatoParametro",
                column: "ReguaContatoRegraId",
                principalTable: "ReguaContatoRegras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReguaContatoParametro_ReguaContatoRegras_ReguaContatoRegraId",
                table: "ReguaContatoParametro");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReguaContatoParametro",
                table: "ReguaContatoParametro");

            migrationBuilder.RenameTable(
                name: "ReguaContatoParametro",
                newName: "ReguaContatoParamero");

            migrationBuilder.RenameIndex(
                name: "IX_ReguaContatoParametro_ReguaContatoRegraId",
                table: "ReguaContatoParamero",
                newName: "IX_ReguaContatoParamero_ReguaContatoRegraId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReguaContatoParamero",
                table: "ReguaContatoParamero",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReguaContatoParamero_ReguaContatoRegras_ReguaContatoRegraId",
                table: "ReguaContatoParamero",
                column: "ReguaContatoRegraId",
                principalTable: "ReguaContatoRegras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
