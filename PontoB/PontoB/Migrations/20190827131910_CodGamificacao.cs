using Microsoft.EntityFrameworkCore.Migrations;

namespace PontoB.Migrations
{
    public partial class CodGamificacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrigemPontuacaoId",
                table: "Pontuacao",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrigemPontuacaoId",
                table: "Pontuacao");
        }
    }
}
