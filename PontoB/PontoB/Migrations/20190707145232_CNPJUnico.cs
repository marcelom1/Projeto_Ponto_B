using Microsoft.EntityFrameworkCore.Migrations;

namespace PontoB.Migrations
{
    public partial class CNPJUnico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Cnpj",
                table: "Empresa",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Empresa_Cnpj",
                table: "Empresa",
                column: "Cnpj",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Empresa_Cnpj",
                table: "Empresa");

            migrationBuilder.AlterColumn<string>(
                name: "Cnpj",
                table: "Empresa",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
