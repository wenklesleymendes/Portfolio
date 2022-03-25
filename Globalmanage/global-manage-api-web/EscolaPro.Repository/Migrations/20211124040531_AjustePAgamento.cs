using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class AjustePAgamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MatriculaAlunoId",
                table: "Pagamento",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pagamento_MatriculaAlunoId",
                table: "Pagamento",
                column: "MatriculaAlunoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pagamento_MatriculaAluno_MatriculaAlunoId",
                table: "Pagamento",
                column: "MatriculaAlunoId",
                principalTable: "MatriculaAluno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pagamento_MatriculaAluno_MatriculaAlunoId",
                table: "Pagamento");

            migrationBuilder.DropIndex(
                name: "IX_Pagamento_MatriculaAlunoId",
                table: "Pagamento");

            migrationBuilder.DropColumn(
                name: "MatriculaAlunoId",
                table: "Pagamento");
        }
    }
}
