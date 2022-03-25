using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class NullableNaturalidade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aluno_Naturalidade_NaturalidadeId",
                table: "Aluno");

            migrationBuilder.AlterColumn<int>(
                name: "NaturalidadeId",
                table: "Aluno",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Aluno_Naturalidade_NaturalidadeId",
                table: "Aluno",
                column: "NaturalidadeId",
                principalTable: "Naturalidade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aluno_Naturalidade_NaturalidadeId",
                table: "Aluno");

            migrationBuilder.AlterColumn<int>(
                name: "NaturalidadeId",
                table: "Aluno",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Aluno_Naturalidade_NaturalidadeId",
                table: "Aluno",
                column: "NaturalidadeId",
                principalTable: "Naturalidade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
