using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PontoB.Migrations
{
    public partial class EnderecoEmpresa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bairro",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "CEP",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "Complemento",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "Logradouro",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "Numero",
                table: "Empresa");

            migrationBuilder.AddColumn<int>(
                name: "EnderecoEmpresaId",
                table: "Empresa",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CEP = table.Column<int>(nullable: false),
                    Logradouro = table.Column<string>(nullable: true),
                    Numero = table.Column<int>(nullable: false),
                    Complemento = table.Column<string>(nullable: true),
                    Bairro = table.Column<string>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    Cidade = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Empresa_EnderecoEmpresaId",
                table: "Empresa",
                column: "EnderecoEmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Empresa_Endereco_EnderecoEmpresaId",
                table: "Empresa",
                column: "EnderecoEmpresaId",
                principalTable: "Endereco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empresa_Endereco_EnderecoEmpresaId",
                table: "Empresa");

            migrationBuilder.DropTable(
                name: "Endereco");

            migrationBuilder.DropIndex(
                name: "IX_Empresa_EnderecoEmpresaId",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "EnderecoEmpresaId",
                table: "Empresa");

            migrationBuilder.AddColumn<string>(
                name: "Bairro",
                table: "Empresa",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CEP",
                table: "Empresa",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "Empresa",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Complemento",
                table: "Empresa",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Empresa",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logradouro",
                table: "Empresa",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Numero",
                table: "Empresa",
                nullable: false,
                defaultValue: 0);
        }
    }
}
