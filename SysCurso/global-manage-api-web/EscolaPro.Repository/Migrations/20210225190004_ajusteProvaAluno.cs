using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class ajusteProvaAluno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProvaAluno_AgendaProva_AgendaProvaId",
                table: "ProvaAluno");

            migrationBuilder.AlterColumn<int>(
                name: "AgendaProvaId",
                table: "ProvaAluno",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ProvaAluno_AgendaProva_AgendaProvaId",
                table: "ProvaAluno",
                column: "AgendaProvaId",
                principalTable: "AgendaProva",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProvaAluno_AgendaProva_AgendaProvaId",
                table: "ProvaAluno");

            migrationBuilder.AlterColumn<int>(
                name: "AgendaProvaId",
                table: "ProvaAluno",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProvaAluno_AgendaProva_AgendaProvaId",
                table: "ProvaAluno",
                column: "AgendaProvaId",
                principalTable: "AgendaProva",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
