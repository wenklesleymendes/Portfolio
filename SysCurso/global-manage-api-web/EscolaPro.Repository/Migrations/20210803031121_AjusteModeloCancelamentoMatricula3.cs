using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class AjusteModeloCancelamentoMatricula3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CancelamentoMatriculaId",
                table: "CancelamentoIsencao",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CancelamentoIsencao_CancelamentoMatriculaId",
                table: "CancelamentoIsencao",
                column: "CancelamentoMatriculaId");

            migrationBuilder.AddForeignKey(
                name: "FK_CancelamentoIsencao_CancelamentoMatricula_CancelamentoMatriculaId",
                table: "CancelamentoIsencao",
                column: "CancelamentoMatriculaId",
                principalTable: "CancelamentoMatricula",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CancelamentoIsencao_CancelamentoMatricula_CancelamentoMatriculaId",
                table: "CancelamentoIsencao");

            migrationBuilder.DropIndex(
                name: "IX_CancelamentoIsencao_CancelamentoMatriculaId",
                table: "CancelamentoIsencao");

            migrationBuilder.DropColumn(
                name: "CancelamentoMatriculaId",
                table: "CancelamentoIsencao");
        }
    }
}
