using Microsoft.EntityFrameworkCore.Migrations;

namespace PontoB.Migrations
{
    public partial class InicialEmpresa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "MinhaSequencia");

            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR MinhaSequencia"),
                    RazaoSocial = table.Column<string>(nullable: false),
                    Cnpj = table.Column<string>(nullable: false),
                    NomeFantasia = table.Column<string>(nullable: true),
                    CEP = table.Column<int>(nullable: false),
                    Logradouro = table.Column<string>(nullable: true),
                    Numero = table.Column<int>(nullable: false),
                    Complemento = table.Column<string>(nullable: true),
                    Bairro = table.Column<string>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    Cidade = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Empresa");

            migrationBuilder.DropSequence(
                name: "MinhaSequencia");
        }
    }
}
