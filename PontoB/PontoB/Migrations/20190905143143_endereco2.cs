using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PontoB.Migrations
{
    public partial class endereco2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ManutencaoPonto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ManutencaoPonto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ColaboradorId = table.Column<int>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    SaldoEmMinutos = table.Column<int>(nullable: false),
                    TotalPrevistoEmMinutos = table.Column<int>(nullable: false),
                    TotalRealizadoEmMinutos = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManutencaoPonto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManutencaoPonto_Colaborador_ColaboradorId",
                        column: x => x.ColaboradorId,
                        principalTable: "Colaborador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ManutencaoPonto_ColaboradorId",
                table: "ManutencaoPonto",
                column: "ColaboradorId");
        }
    }
}
