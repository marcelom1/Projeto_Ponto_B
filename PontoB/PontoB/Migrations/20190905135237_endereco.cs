using Microsoft.EntityFrameworkCore.Migrations;

namespace PontoB.Migrations
{
    public partial class endereco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_EstadoUF_EstadoId",
                table: "Endereco");

            migrationBuilder.AlterColumn<string>(
                name: "Logradouro",
                table: "Endereco",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EstadoId",
                table: "Endereco",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cidade",
                table: "Endereco",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Bairro",
                table: "Endereco",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Endereco_EstadoUF_EstadoId",
                table: "Endereco",
                column: "EstadoId",
                principalTable: "EstadoUF",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_EstadoUF_EstadoId",
                table: "Endereco");

            migrationBuilder.AlterColumn<string>(
                name: "Logradouro",
                table: "Endereco",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "EstadoId",
                table: "Endereco",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Cidade",
                table: "Endereco",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Bairro",
                table: "Endereco",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Endereco_EstadoUF_EstadoId",
                table: "Endereco",
                column: "EstadoId",
                principalTable: "EstadoUF",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
