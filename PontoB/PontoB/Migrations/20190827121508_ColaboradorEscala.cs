using Microsoft.EntityFrameworkCore.Migrations;

namespace PontoB.Migrations
{
    public partial class ColaboradorEscala : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Colaborador_Escala_EscalaId",
                table: "Colaborador");

            migrationBuilder.AddForeignKey(
                name: "FK_Colaborador_Escala_EscalaId",
                table: "Colaborador",
                column: "EscalaId",
                principalTable: "Escala",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Colaborador_Escala_EscalaId",
                table: "Colaborador");

            migrationBuilder.AddForeignKey(
                name: "FK_Colaborador_Escala_EscalaId",
                table: "Colaborador",
                column: "EscalaId",
                principalTable: "Escala",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
