using Microsoft.EntityFrameworkCore.Migrations;

namespace PontoB.Migrations
{
    public partial class TotalEscala : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EscalaHorario_Escala_EscalaId",
                table: "EscalaHorario");

            migrationBuilder.AlterColumn<int>(
                name: "EscalaId",
                table: "EscalaHorario",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalEmMinutos",
                table: "EscalaHorario",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_EscalaHorario_Escala_EscalaId",
                table: "EscalaHorario",
                column: "EscalaId",
                principalTable: "Escala",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EscalaHorario_Escala_EscalaId",
                table: "EscalaHorario");

            migrationBuilder.DropColumn(
                name: "TotalEmMinutos",
                table: "EscalaHorario");

            migrationBuilder.AlterColumn<int>(
                name: "EscalaId",
                table: "EscalaHorario",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_EscalaHorario_Escala_EscalaId",
                table: "EscalaHorario",
                column: "EscalaId",
                principalTable: "Escala",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
