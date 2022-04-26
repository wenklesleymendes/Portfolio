using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class criacaodotipodecertificado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProvaAluno_AgendaProva_AgendaProvaId",
                table: "ProvaAluno");

            migrationBuilder.AlterColumn<int>(
                name: "AgendaProvaId",
                table: "ProvaAluno",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoSituacaoCertificado",
                table: "MatriculaAluno",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ProvaAluno_AgendaProva_AgendaProvaId",
                table: "ProvaAluno",
                column: "AgendaProvaId",
                principalTable: "AgendaProva",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProvaAluno_AgendaProva_AgendaProvaId",
                table: "ProvaAluno");

            migrationBuilder.DropColumn(
                name: "TipoSituacaoCertificado",
                table: "MatriculaAluno");

            migrationBuilder.AlterColumn<int>(
                name: "AgendaProvaId",
                table: "ProvaAluno",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_ProvaAluno_AgendaProva_AgendaProvaId",
                table: "ProvaAluno",
                column: "AgendaProvaId",
                principalTable: "AgendaProva",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
