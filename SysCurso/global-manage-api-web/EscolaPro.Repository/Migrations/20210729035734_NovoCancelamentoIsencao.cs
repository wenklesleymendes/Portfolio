using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class NovoCancelamentoIsencao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CancelamentoIsencao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    CancelamentoId = table.Column<int>(nullable: false),
                    MatriculaId = table.Column<int>(nullable: false),
                    Justificativa = table.Column<string>(nullable: true),
                    MotivoIsencao = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancelamentoIsencao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CancelamentoIsencaoPagamento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    PagamentoId = table.Column<int>(nullable: false),
                    CancelamentoIsencaoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancelamentoIsencaoPagamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CancelamentoIsencaoPagamento_CancelamentoIsencao_CancelamentoIsencaoId",
                        column: x => x.CancelamentoIsencaoId,
                        principalTable: "CancelamentoIsencao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CancelamentoIsencaoPagamento_Pagamento_PagamentoId",
                        column: x => x.PagamentoId,
                        principalTable: "Pagamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CancelamentoIsencaoPagamento_CancelamentoIsencaoId",
                table: "CancelamentoIsencaoPagamento",
                column: "CancelamentoIsencaoId");

            migrationBuilder.CreateIndex(
                name: "IX_CancelamentoIsencaoPagamento_PagamentoId",
                table: "CancelamentoIsencaoPagamento",
                column: "PagamentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CancelamentoIsencaoPagamento");

            migrationBuilder.DropTable(
                name: "CancelamentoIsencao");
        }
    }
}
