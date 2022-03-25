using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace EscolaPro.Repository.Migrations
{
    public partial class RecriandoAtendimento4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
         name: "Atendimento",
         columns: table => new
         {
             Id = table.Column<int>(nullable: false)
                           .Annotation("SqlServer:Identity", "1, 1"),
             IsActive = table.Column<bool>(nullable: false),
             UpdatedAt = table.Column<DateTime>(nullable: true),
             DeletedAt = table.Column<DateTime>(nullable: true),
             IsDelete = table.Column<bool>(nullable: false),
             CanaldeAtendimento = table.Column<int>(nullable: false),
             NomedoCliente = table.Column<string>(nullable: true),
             CursodeInteresse = table.Column<int>(nullable: false),
             Celular = table.Column<string>(nullable: true),
             TelefoneFixo = table.Column<string>(nullable: true),
             Periodo = table.Column<int>(nullable: false),
             MotivodeInteressenoCurso = table.Column<int>(nullable: false),
             ComonosConheceu = table.Column<int>(nullable: false),
             Email = table.Column<string>(nullable: true),
             OperadorLogado = table.Column<int>(nullable: false),
             OperadorCadastro = table.Column<int>(nullable: false),
             Score = table.Column<int>(nullable: false),
             Status = table.Column<string>(nullable: false),
             AgendamentodaMatricula = table.Column<int>(nullable: false),
             DataeHora = table.Column<string>(nullable: false),
             DiadoAgendamento = table.Column<string>(nullable: false),
             HoradoAgendamento = table.Column<string>(nullable: false)

         },
             constraints: table =>
             {
                 table.PrimaryKey("PK_Atendimento", x => x.Id);
             });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
            name: "Atendimento");
        }
    }
}
