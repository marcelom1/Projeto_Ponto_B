using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PontoB.Migrations
{
    public partial class Escalas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Escala",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escala", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EscalaHorario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DiaSemana = table.Column<string>(nullable: true),
                    EntradaHora = table.Column<int>(nullable: false),
                    EntradaMinuto = table.Column<int>(nullable: false),
                    SaidaHora = table.Column<int>(nullable: false),
                    SaidaMinuto = table.Column<int>(nullable: false),
                    EscalaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EscalaHorario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EscalaHorario_Escala_EscalaId",
                        column: x => x.EscalaId,
                        principalTable: "Escala",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EscalaHorario_EscalaId",
                table: "EscalaHorario",
                column: "EscalaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EscalaHorario");

            migrationBuilder.DropTable(
                name: "Escala");
        }
    }
}
