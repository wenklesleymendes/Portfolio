using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class AcessoMenuAtendimento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CancelamentoIsencao_CancelamentoMatricula_CancelamentoMatriculaId",
                table: "CancelamentoIsencao");

            migrationBuilder.AlterColumn<int>(
                name: "CancelamentoMatriculaId",
                table: "CancelamentoIsencao",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CancelamentoIsencao_CancelamentoMatricula_CancelamentoMatriculaId",
                table: "CancelamentoIsencao",
                column: "CancelamentoMatriculaId",
                principalTable: "CancelamentoMatricula",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddColumn<int>(
                name: "AcessoMenuAtendimento",
                table: "PerfilUsuario",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CancelamentoIsencao_CancelamentoMatricula_CancelamentoMatriculaId",
                table: "CancelamentoIsencao");

            migrationBuilder.AlterColumn<int>(
                name: "CancelamentoMatriculaId",
                table: "CancelamentoIsencao",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));


            migrationBuilder.AddForeignKey(
                name: "FK_CancelamentoIsencao_CancelamentoMatricula_CancelamentoMatriculaId",
                table: "CancelamentoIsencao",
                column: "CancelamentoMatriculaId",
                principalTable: "CancelamentoMatricula",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddColumn<bool>(
                name: "AcessoMenuAtendimento",
                table: "PerfilUsuario",
                nullable: true);

        }
    }
}
