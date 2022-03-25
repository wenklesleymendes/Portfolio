using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class AjusteReguaContato : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataInclusao",
                table: "ReguaContatoFila",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "MatriculaAlunoId",
                table: "ReguaContatoFila",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PagamentoId",
                table: "ReguaContatoFila",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ReguaContatoFila_MatriculaAlunoId",
                table: "ReguaContatoFila",
                column: "MatriculaAlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_ReguaContatoFila_PagamentoId",
                table: "ReguaContatoFila",
                column: "PagamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReguaContatoFila_MatriculaAluno_MatriculaAlunoId",
                table: "ReguaContatoFila",
                column: "MatriculaAlunoId",
                principalTable: "MatriculaAluno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReguaContatoFila_Pagamento_PagamentoId",
                table: "ReguaContatoFila",
                column: "PagamentoId",
                principalTable: "Pagamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReguaContatoFila_MatriculaAluno_MatriculaAlunoId",
                table: "ReguaContatoFila");

            migrationBuilder.DropForeignKey(
                name: "FK_ReguaContatoFila_Pagamento_PagamentoId",
                table: "ReguaContatoFila");

            migrationBuilder.DropIndex(
                name: "IX_ReguaContatoFila_MatriculaAlunoId",
                table: "ReguaContatoFila");

            migrationBuilder.DropIndex(
                name: "IX_ReguaContatoFila_PagamentoId",
                table: "ReguaContatoFila");

            migrationBuilder.DropColumn(
                name: "DataInclusao",
                table: "ReguaContatoFila");

            migrationBuilder.DropColumn(
                name: "MatriculaAlunoId",
                table: "ReguaContatoFila");

            migrationBuilder.DropColumn(
                name: "PagamentoId",
                table: "ReguaContatoFila");
        }
    }
}
