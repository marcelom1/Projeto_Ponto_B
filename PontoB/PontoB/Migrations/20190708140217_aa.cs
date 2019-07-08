using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PontoB.Migrations
{
    public partial class aa : Migration
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
                    EscalaIdId = table.Column<int>(nullable: true),
                    DiaSemana = table.Column<string>(nullable: true),
                    HoraEntrada = table.Column<int>(nullable: false),
                    HoraSaida = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EscalaHorario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EscalaHorario_Escala_EscalaIdId",
                        column: x => x.EscalaIdId,
                        principalTable: "Escala",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EscalaHorario_EscalaIdId",
                table: "EscalaHorario",
                column: "EscalaIdId");
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
