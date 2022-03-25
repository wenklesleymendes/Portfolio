using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class AddTBsReguaContato : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReguaContatoRegras",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    TipoMensagem = table.Column<int>(nullable: false),
                    ContaOrigem = table.Column<string>(nullable: true),
                    Titulo = table.Column<string>(nullable: true),
                    Texto = table.Column<string>(nullable: true),
                    DataEnvio = table.Column<DateTime>(nullable: false),
                    TipoRegra = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReguaContatoRegras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReguaContatoFila",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    ReguaContatoRegrasId = table.Column<int>(nullable: false),
                    AlunoId = table.Column<int>(nullable: false),
                    Prioridade = table.Column<int>(nullable: false),
                    StatusFila = table.Column<int>(nullable: false),
                    DataEnvio = table.Column<DateTime>(nullable: false),
                    MensagemErro = table.Column<string>(nullable: true),
                    EnviadaComSucesso = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReguaContatoFila", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReguaContatoFila_Aluno_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Aluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReguaContatoFila_ReguaContatoRegras_ReguaContatoRegrasId",
                        column: x => x.ReguaContatoRegrasId,
                        principalTable: "ReguaContatoRegras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReguaContatoHistorico",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    ReguaContatoRegrasId = table.Column<int>(nullable: false),
                    AlunoId = table.Column<int>(nullable: false),
                    ReguaContatoFilaId = table.Column<int>(nullable: false),
                    TipoMensagem = table.Column<int>(nullable: false),
                    Titulo = table.Column<string>(nullable: true),
                    Texto = table.Column<string>(nullable: true),
                    DataEnvio = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReguaContatoHistorico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReguaContatoHistorico_Aluno_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Aluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReguaContatoHistorico_ReguaContatoFila_ReguaContatoFilaId",
                        column: x => x.ReguaContatoFilaId,
                        principalTable: "ReguaContatoFila",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReguaContatoHistorico_ReguaContatoRegras_ReguaContatoRegrasId",
                        column: x => x.ReguaContatoRegrasId,
                        principalTable: "ReguaContatoRegras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReguaContatoFila_AlunoId",
                table: "ReguaContatoFila",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_ReguaContatoFila_ReguaContatoRegrasId",
                table: "ReguaContatoFila",
                column: "ReguaContatoRegrasId");

            migrationBuilder.CreateIndex(
                name: "IX_ReguaContatoHistorico_AlunoId",
                table: "ReguaContatoHistorico",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_ReguaContatoHistorico_ReguaContatoFilaId",
                table: "ReguaContatoHistorico",
                column: "ReguaContatoFilaId");

            migrationBuilder.CreateIndex(
                name: "IX_ReguaContatoHistorico_ReguaContatoRegrasId",
                table: "ReguaContatoHistorico",
                column: "ReguaContatoRegrasId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReguaContatoHistorico");

            migrationBuilder.DropTable(
                name: "ReguaContatoFila");

            migrationBuilder.DropTable(
                name: "ReguaContatoRegras");
        }
    }
}
