using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class PermitirNull_ReguaContatoFilaId_ReguaContatoHistorico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReguaContatoHistorico_ReguaContatoFila_ReguaContatoFilaId",
                table: "ReguaContatoHistorico");

            migrationBuilder.DropForeignKey(
                name: "FK_ReguaContatoHistorico_ReguaContatoRegras_ReguaContatoRegrasId",
                table: "ReguaContatoHistorico");

            migrationBuilder.DropIndex(
                name: "IX_ReguaContatoHistorico_ReguaContatoFilaId",
                table: "ReguaContatoHistorico");

            migrationBuilder.DropIndex(
                name: "IX_ReguaContatoHistorico_ReguaContatoRegrasId",
                table: "ReguaContatoHistorico");

            migrationBuilder.AlterColumn<int>(
                name: "ReguaContatoRegrasId",
                table: "ReguaContatoHistorico",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ReguaContatoFilaId",
                table: "ReguaContatoHistorico",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ReguaContatoRegrasId",
                table: "ReguaContatoHistorico",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReguaContatoFilaId",
                table: "ReguaContatoHistorico",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReguaContatoHistorico_ReguaContatoFilaId",
                table: "ReguaContatoHistorico",
                column: "ReguaContatoFilaId");

            migrationBuilder.CreateIndex(
                name: "IX_ReguaContatoHistorico_ReguaContatoRegrasId",
                table: "ReguaContatoHistorico",
                column: "ReguaContatoRegrasId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReguaContatoHistorico_ReguaContatoFila_ReguaContatoFilaId",
                table: "ReguaContatoHistorico",
                column: "ReguaContatoFilaId",
                principalTable: "ReguaContatoFila",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReguaContatoHistorico_ReguaContatoRegras_ReguaContatoRegrasId",
                table: "ReguaContatoHistorico",
                column: "ReguaContatoRegrasId",
                principalTable: "ReguaContatoRegras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
