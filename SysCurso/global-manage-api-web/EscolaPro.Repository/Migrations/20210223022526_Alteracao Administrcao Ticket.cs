using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class AlteracaoAdministrcaoTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CentroCustoId",
                table: "AssuntoTicket",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnidadeId",
                table: "AssuntoTicket",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FuncionarioAssuntoTicket",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    FuncionarioId = table.Column<int>(nullable: false),
                    AssuntoTicketId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuncionarioAssuntoTicket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FuncionarioAssuntoTicket_AssuntoTicket_AssuntoTicketId",
                        column: x => x.AssuntoTicketId,
                        principalTable: "AssuntoTicket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FuncionarioAssuntoTicket_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssuntoTicket_CentroCustoId",
                table: "AssuntoTicket",
                column: "CentroCustoId");

            migrationBuilder.CreateIndex(
                name: "IX_AssuntoTicket_UnidadeId",
                table: "AssuntoTicket",
                column: "UnidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_FuncionarioAssuntoTicket_AssuntoTicketId",
                table: "FuncionarioAssuntoTicket",
                column: "AssuntoTicketId");

            migrationBuilder.CreateIndex(
                name: "IX_FuncionarioAssuntoTicket_FuncionarioId",
                table: "FuncionarioAssuntoTicket",
                column: "FuncionarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssuntoTicket_CentroCusto_CentroCustoId",
                table: "AssuntoTicket",
                column: "CentroCustoId",
                principalTable: "CentroCusto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssuntoTicket_Unidade_UnidadeId",
                table: "AssuntoTicket",
                column: "UnidadeId",
                principalTable: "Unidade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssuntoTicket_CentroCusto_CentroCustoId",
                table: "AssuntoTicket");

            migrationBuilder.DropForeignKey(
                name: "FK_AssuntoTicket_Unidade_UnidadeId",
                table: "AssuntoTicket");

            migrationBuilder.DropTable(
                name: "FuncionarioAssuntoTicket");

            migrationBuilder.DropIndex(
                name: "IX_AssuntoTicket_CentroCustoId",
                table: "AssuntoTicket");

            migrationBuilder.DropIndex(
                name: "IX_AssuntoTicket_UnidadeId",
                table: "AssuntoTicket");

            migrationBuilder.DropColumn(
                name: "CentroCustoId",
                table: "AssuntoTicket");

            migrationBuilder.DropColumn(
                name: "UnidadeId",
                table: "AssuntoTicket");
        }
    }
}
