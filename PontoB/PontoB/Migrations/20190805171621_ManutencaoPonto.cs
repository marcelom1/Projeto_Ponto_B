using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PontoB.Migrations
{
    public partial class ManutencaoPonto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DesconsiderarMarcacao",
                table: "RegistroPonto",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Observacao",
                table: "RegistroPonto",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RegistroManual",
                table: "RegistroPonto",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ManutencaoPonto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Data = table.Column<DateTime>(nullable: false),
                    ColaboradorId = table.Column<int>(nullable: false),
                    TotalPrevistoEmMinutos = table.Column<int>(nullable: false),
                    TotalRealizadoEmMinutos = table.Column<int>(nullable: false),
                    SaldoEmMinutos = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculoPonto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalculoPonto_Colaborador_ColaboradorId",
                        column: x => x.ColaboradorId,
                        principalTable: "Colaborador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalculoPonto_ColaboradorId",
                table: "ManutencaoPonto",
                column: "ColaboradorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ManutencaoPonto");

            migrationBuilder.DropColumn(
                name: "DesconsiderarMarcacao",
                table: "RegistroPonto");

            migrationBuilder.DropColumn(
                name: "Observacao",
                table: "RegistroPonto");

            migrationBuilder.DropColumn(
                name: "RegistroManual",
                table: "RegistroPonto");
        }
    }
}
