using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PontoB.Migrations
{
    public partial class Colaborador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DiaSemana",
                table: "EscalaHorario",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Colaborador",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NomeCompleto = table.Column<string>(nullable: false),
                    CPF = table.Column<string>(nullable: false),
                    RG = table.Column<string>(nullable: true),
                    Cargo = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    Telefone = table.Column<string>(nullable: true),
                    DataAdmissao = table.Column<DateTime>(nullable: false),
                    DataDemissao = table.Column<DateTime>(nullable: false),
                    DataNascimento = table.Column<DateTime>(nullable: false),
                    Pis = table.Column<string>(nullable: false),
                    EnderecoColaboradorId = table.Column<int>(nullable: false),
                    EscalaColaboradorId = table.Column<int>(nullable: true),
                    EmpresaColaboradorId = table.Column<int>(nullable: false),
                    Senha = table.Column<string>(nullable: false),
                    Master = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colaborador", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Colaborador_Empresa_EmpresaColaboradorId",
                        column: x => x.EmpresaColaboradorId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Colaborador_Endereco_EnderecoColaboradorId",
                        column: x => x.EnderecoColaboradorId,
                        principalTable: "Endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Colaborador_Escala_EscalaColaboradorId",
                        column: x => x.EscalaColaboradorId,
                        principalTable: "Escala",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Colaborador_EmpresaColaboradorId",
                table: "Colaborador",
                column: "EmpresaColaboradorId");

            migrationBuilder.CreateIndex(
                name: "IX_Colaborador_EnderecoColaboradorId",
                table: "Colaborador",
                column: "EnderecoColaboradorId");

            migrationBuilder.CreateIndex(
                name: "IX_Colaborador_EscalaColaboradorId",
                table: "Colaborador",
                column: "EscalaColaboradorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Colaborador");

            migrationBuilder.AlterColumn<string>(
                name: "DiaSemana",
                table: "EscalaHorario",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
