using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class AjusteModeloCancelamentoMatricula : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           migrationBuilder.AlterColumn<int>(
                name: "MotivoIsencao",
                table: "CancelamentoMatricula",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AlunoId",
                table: "CancelamentoMatricula",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "DentroPrazoCancelamento",
                table: "CancelamentoMatricula",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PagoTotal",
                table: "CancelamentoMatricula",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorEmAtraso",
                table: "CancelamentoMatricula",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlunoId",
                table: "CancelamentoMatricula");

            migrationBuilder.DropColumn(
                name: "DentroPrazoCancelamento",
                table: "CancelamentoMatricula");

            migrationBuilder.DropColumn(
                name: "PagoTotal",
                table: "CancelamentoMatricula");

            migrationBuilder.DropColumn(
                name: "ValorEmAtraso",
                table: "CancelamentoMatricula");

            migrationBuilder.AlterColumn<int>(
                name: "MotivoIsencao",
                table: "CancelamentoMatricula",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
