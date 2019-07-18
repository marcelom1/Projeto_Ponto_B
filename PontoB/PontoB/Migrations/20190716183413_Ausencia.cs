using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PontoB.Migrations
{
    public partial class Ausencia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Senha",
                table: "Colaborador",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                name: "Ausencia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ausencia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MotivoAusencia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: true),
                    Abonar = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotivoAusencia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AusenciaColaboradores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    HoraInicio = table.Column<int>(nullable: false),
                    MinutoInicio = table.Column<int>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: false),
                    HoraFim = table.Column<int>(nullable: false),
                    MinutoFim = table.Column<int>(nullable: false),
                    Observacao = table.Column<string>(nullable: true),
                    ColaboradorId = table.Column<int>(nullable: false),
                    MotivoAusenciaId = table.Column<int>(nullable: false),
                    AusenciaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AusenciaColaboradores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AusenciaColaboradores_Ausencia_AusenciaId",
                        column: x => x.AusenciaId,
                        principalTable: "Ausencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AusenciaColaboradores_Colaborador_ColaboradorId",
                        column: x => x.ColaboradorId,
                        principalTable: "Colaborador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AusenciaColaboradores_MotivoAusencia_MotivoAusenciaId",
                        column: x => x.MotivoAusenciaId,
                        principalTable: "MotivoAusencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AusenciaColaboradores_AusenciaId",
                table: "AusenciaColaboradores",
                column: "AusenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_AusenciaColaboradores_ColaboradorId",
                table: "AusenciaColaboradores",
                column: "ColaboradorId");

            migrationBuilder.CreateIndex(
                name: "IX_AusenciaColaboradores_MotivoAusenciaId",
                table: "AusenciaColaboradores",
                column: "MotivoAusenciaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AusenciaColaboradores");

            migrationBuilder.DropTable(
                name: "Ausencia");

            migrationBuilder.DropTable(
                name: "MotivoAusencia");

            migrationBuilder.AlterColumn<string>(
                name: "Senha",
                table: "Colaborador",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
