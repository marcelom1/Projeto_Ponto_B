using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PontoB.Migrations
{
    public partial class EstadosUf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Endereco");

            migrationBuilder.AddColumn<int>(
                name: "EstadoId",
                table: "Endereco",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EstadoUF",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Sigla = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoUF", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_EstadoId",
                table: "Endereco",
                column: "EstadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Endereco_EstadoUF_EstadoId",
                table: "Endereco",
                column: "EstadoId",
                principalTable: "EstadoUF",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_EstadoUF_EstadoId",
                table: "Endereco");

            migrationBuilder.DropTable(
                name: "EstadoUF");

            migrationBuilder.DropIndex(
                name: "IX_Endereco_EstadoId",
                table: "Endereco");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "Endereco");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Endereco",
                nullable: true);
        }
    }
}
