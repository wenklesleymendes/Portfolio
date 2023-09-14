using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class Ocorrencias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ocorrencia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    UsuarioLogadoId = table.Column<int>(nullable: false),
                    DataAtendimento = table.Column<DateTime>(nullable: true),
                    DataAbertura = table.Column<DateTime>(nullable: true),
                    NumeroProtocolo = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Mensagem = table.Column<string>(nullable: true),
                    MatriculaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ocorrencia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ocorrencia_MatriculaAluno_MatriculaId",
                        column: x => x.MatriculaId,
                        principalTable: "MatriculaAluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ocorrencia_MatriculaId",
                table: "Ocorrencia",
                column: "MatriculaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ocorrencia");
        }
    }
}
