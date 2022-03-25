using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class alteracaodatabeladeperfil : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InconsistenciaDocumento_MatriculaAluno_MatriculaAlunoId",
                table: "InconsistenciaDocumento");

            migrationBuilder.AddColumn<int>(
                name: "PerfilSistemaEnum",
                table: "PerfilUsuario",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "MatriculaAlunoId",
                table: "InconsistenciaDocumento",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InconsistenciaDocumento_MatriculaAluno_MatriculaAlunoId",
                table: "InconsistenciaDocumento",
                column: "MatriculaAlunoId",
                principalTable: "MatriculaAluno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InconsistenciaDocumento_MatriculaAluno_MatriculaAlunoId",
                table: "InconsistenciaDocumento");

            migrationBuilder.DropColumn(
                name: "PerfilSistemaEnum",
                table: "PerfilUsuario");

            migrationBuilder.AlterColumn<int>(
                name: "MatriculaAlunoId",
                table: "InconsistenciaDocumento",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_InconsistenciaDocumento_MatriculaAluno_MatriculaAlunoId",
                table: "InconsistenciaDocumento",
                column: "MatriculaAlunoId",
                principalTable: "MatriculaAluno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
