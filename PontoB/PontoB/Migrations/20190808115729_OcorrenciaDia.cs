using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PontoB.Migrations
{
    public partial class OcorrenciaDia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OcorrenciaDia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    ColaboradorId = table.Column<int>(nullable: false),
                    CodigoOcorrencia = table.Column<int>(nullable: false),
                    QtdMinutos = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OcorrenciaDia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OcorrenciaDia_Colaborador_ColaboradorId",
                        column: x => x.ColaboradorId,
                        principalTable: "Colaborador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OcorrenciaDia_ColaboradorId",
                table: "OcorrenciaDia",
                column: "ColaboradorId");

            migrationBuilder.CreateIndex(
                name: "IX_OcorrenciaDia_Date_ColaboradorId_CodigoOcorrencia",
                table: "OcorrenciaDia",
                columns: new[] { "Date", "ColaboradorId", "CodigoOcorrencia" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OcorrenciaDia");
        }
    }
}
