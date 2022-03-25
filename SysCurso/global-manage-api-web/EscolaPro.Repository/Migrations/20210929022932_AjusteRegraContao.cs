using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class AjusteRegraContao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_CancelamentoIsencao_CancelamentoMatricula_CancelamentoMatriculaId",
            //    table: "CancelamentoIsencao");

            //migrationBuilder.DropColumn(
            //    name: "CancelamentoId",
            //    table: "CancelamentoIsencao");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "ReguaContatoRegras",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Prioridade",
                table: "ReguaContatoRegras",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnidadeId",
                table: "ReguaContatoFila",
                nullable: true);

            //migrationBuilder.AddColumn<bool>(
            //    name: "AcessoMenuAtendimento",
            //    table: "PerfilUsuario",
            //    nullable: false,
            //    defaultValue: false);

            //migrationBuilder.AlterColumn<int>(
            //    name: "CancelamentoMatriculaId",
            //    table: "CancelamentoIsencao",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int",
            //    oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ReguaContatoParamero",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Valor = table.Column<string>(nullable: true),
                    TipoValor = table.Column<int>(nullable: false),
                    ReguaContatoRegraId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReguaContatoParamero", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReguaContatoParamero_ReguaContatoRegras_ReguaContatoRegraId",
                        column: x => x.ReguaContatoRegraId,
                        principalTable: "ReguaContatoRegras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReguaContatoFila_UnidadeId",
                table: "ReguaContatoFila",
                column: "UnidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReguaContatoParamero_ReguaContatoRegraId",
                table: "ReguaContatoParamero",
                column: "ReguaContatoRegraId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_CancelamentoIsencao_CancelamentoMatricula_CancelamentoMatriculaId",
            //    table: "CancelamentoIsencao",
            //    column: "CancelamentoMatriculaId",
            //    principalTable: "CancelamentoMatricula",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReguaContatoFila_Unidade_UnidadeId",
                table: "ReguaContatoFila",
                column: "UnidadeId",
                principalTable: "Unidade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_CancelamentoIsencao_CancelamentoMatricula_CancelamentoMatriculaId",
            //    table: "CancelamentoIsencao");

            migrationBuilder.DropForeignKey(
                name: "FK_ReguaContatoFila_Unidade_UnidadeId",
                table: "ReguaContatoFila");

            migrationBuilder.DropTable(
                name: "ReguaContatoParamero");

            migrationBuilder.DropIndex(
                name: "IX_ReguaContatoFila_UnidadeId",
                table: "ReguaContatoFila");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "ReguaContatoRegras");

            migrationBuilder.DropColumn(
                name: "Prioridade",
                table: "ReguaContatoRegras");

            migrationBuilder.DropColumn(
                name: "UnidadeId",
                table: "ReguaContatoFila");

            //migrationBuilder.DropColumn(
            //    name: "AcessoMenuAtendimento",
            //    table: "PerfilUsuario");

            //migrationBuilder.AlterColumn<int>(
            //    name: "CancelamentoMatriculaId",
            //    table: "CancelamentoIsencao",
            //    type: "int",
            //    nullable: true,
            //    oldClrType: typeof(int));

            //migrationBuilder.AddColumn<int>(
            //    name: "CancelamentoId",
            //    table: "CancelamentoIsencao",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_CancelamentoIsencao_CancelamentoMatricula_CancelamentoMatriculaId",
            //    table: "CancelamentoIsencao",
            //    column: "CancelamentoMatriculaId",
            //    principalTable: "CancelamentoMatricula",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }
    }
}
