using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolaPro.Repository.Migrations
{
    public partial class ApostilaOnline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<string>(
            //    name: "Observacoes",
            //    table: "AtendimentoAgendamento",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "Situacao",
            //    table: "AtendimentoAgendamento",
            //    nullable: false,
            //    defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ApostilaOnline",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    URL = table.Column<string>(nullable: true),
                    MateriaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApostilaOnline", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApostilaOnline");

            //migrationBuilder.DropColumn(
            //    name: "Observacoes",
            //    table: "AtendimentoAgendamento");

            //migrationBuilder.DropColumn(
            //    name: "Situacao",
            //    table: "AtendimentoAgendamento");
        }
    }
}
