using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class AjusteProvaCertificado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CertificadoProva_MatriculaAluno_MatriculaAlunoId",
                table: "CertificadoProva");

            migrationBuilder.DropColumn(
                name: "MatriculaId",
                table: "CertificadoProva");

            migrationBuilder.AlterColumn<int>(
                name: "MatriculaAlunoId",
                table: "CertificadoProva",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CertificadoProva_MatriculaAluno_MatriculaAlunoId",
                table: "CertificadoProva",
                column: "MatriculaAlunoId",
                principalTable: "MatriculaAluno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CertificadoProva_MatriculaAluno_MatriculaAlunoId",
                table: "CertificadoProva");

            migrationBuilder.AlterColumn<int>(
                name: "MatriculaAlunoId",
                table: "CertificadoProva",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "MatriculaId",
                table: "CertificadoProva",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_CertificadoProva_MatriculaAluno_MatriculaAlunoId",
                table: "CertificadoProva",
                column: "MatriculaAlunoId",
                principalTable: "MatriculaAluno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
