using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class unidadeTransporteProva : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnidadeTransporteProvaId",
                table: "ProvaAluno",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UnidadeTransporteProva",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    NumeroOnibus = table.Column<int>(nullable: false),
                    TotalVagas = table.Column<int>(nullable: false),
                    AgendaProvaId = table.Column<int>(nullable: false),
                    UnidadeParticipanteProvaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadeTransporteProva", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnidadeTransporteProva_AgendaProva_AgendaProvaId",
                        column: x => x.AgendaProvaId,
                        principalTable: "AgendaProva",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnidadeTransporteProva_UnidadeParticipanteProva_UnidadeParticipanteProvaId",
                        column: x => x.UnidadeParticipanteProvaId,
                        principalTable: "UnidadeParticipanteProva",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProvaAluno_UnidadeTransporteProvaId",
                table: "ProvaAluno",
                column: "UnidadeTransporteProvaId");

            migrationBuilder.CreateIndex(
                name: "IX_UnidadeTransporteProva_AgendaProvaId",
                table: "UnidadeTransporteProva",
                column: "AgendaProvaId");

            migrationBuilder.CreateIndex(
                name: "IX_UnidadeTransporteProva_UnidadeParticipanteProvaId",
                table: "UnidadeTransporteProva",
                column: "UnidadeParticipanteProvaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProvaAluno_UnidadeTransporteProva_UnidadeTransporteProvaId",
                table: "ProvaAluno",
                column: "UnidadeTransporteProvaId",
                principalTable: "UnidadeTransporteProva",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProvaAluno_UnidadeTransporteProva_UnidadeTransporteProvaId",
                table: "ProvaAluno");

            migrationBuilder.DropTable(
                name: "UnidadeTransporteProva");

            migrationBuilder.DropIndex(
                name: "IX_ProvaAluno_UnidadeTransporteProvaId",
                table: "ProvaAluno");

            migrationBuilder.DropColumn(
                name: "UnidadeTransporteProvaId",
                table: "ProvaAluno");
        }
    }
}
