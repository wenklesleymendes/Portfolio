using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class AjusteModeloCancelamentoMatricula4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
               name: "IX_CancelamentoIsencaoPagamento_CancelamentoIsencaoId",
               table: "CancelamentoIsencaoPagamento",
               column: "CancelamentoMatriculaId");

            migrationBuilder.AddForeignKey(
                name: "FK_CancelamentoIsencaoPagamento_CancelamentoIsencao_CancelamentoIsencaoId",
                table: "CancelamentoIsencaoPagamento",
                column: "CancelamentoIsencaoId",
                principalTable: "CancelamentoIsencao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.DropColumn(
              name: "CancelamentoId",
              table: "CancelamentoIsencao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_CancelamentoIsencaoPagamento_CancelamentoIsencao_CancelamentoIsencaoId",
                table: "CancelamentoIsencaoPagamento");
            migrationBuilder.DropIndex(
               name: "IX_CancelamentoIsencaoPagamento_CancelamentoIsencaoId",
               table: "CancelamentoIsencaoPagamento");

            migrationBuilder.AddColumn<int>(
                name: "CancelamentoId",
                table: "CancelamentoIsencao",
                nullable: false,
                defaultValue: 0);
        }
    }
}
