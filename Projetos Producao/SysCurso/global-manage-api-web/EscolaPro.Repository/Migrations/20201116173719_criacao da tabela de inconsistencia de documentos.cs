using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class criacaodatabeladeinconsistenciadedocumentos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InconsistenciaDocumento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    DocumentoEnum = table.Column<int>(nullable: false),
                    MatriculaAlunoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InconsistenciaDocumento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InconsistenciaDocumento_MatriculaAluno_MatriculaAlunoId",
                        column: x => x.MatriculaAlunoId,
                        principalTable: "MatriculaAluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InconsistenciaDocumento_MatriculaAlunoId",
                table: "InconsistenciaDocumento",
                column: "MatriculaAlunoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InconsistenciaDocumento");
        }
    }
}
