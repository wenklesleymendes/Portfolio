using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class CancelamentoMatricula : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CancelamentoMatricula",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    MatriculaAlunoId = table.Column<int>(nullable: false),
                    DataCancelamento = table.Column<DateTime>(nullable: false),
                    MotivoCancelamento = table.Column<int>(nullable: false),
                    Comentario = table.Column<string>(nullable: true),
                    ValorMultaCancelamento = table.Column<decimal>(nullable: false),
                    IsentarCancelamento = table.Column<bool>(nullable: false),
                    MotivoIsencao = table.Column<int>(nullable: false),
                    UsuarioIsencaoId = table.Column<int>(nullable: false),
                    UsuarioLogadoId = table.Column<int>(nullable: false),
                    AnexoAtestadoMedicoId = table.Column<int>(nullable: false),
                    AnexoCartaCancelamentoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancelamentoMatricula", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CancelamentoMatricula_MatriculaAluno_MatriculaAlunoId",
                        column: x => x.MatriculaAlunoId,
                        principalTable: "MatriculaAluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CancelamentoMatricula_MatriculaAlunoId",
                table: "CancelamentoMatricula",
                column: "MatriculaAlunoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CancelamentoMatricula");
        }
    }
}
