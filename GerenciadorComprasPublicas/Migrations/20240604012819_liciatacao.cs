using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciadorComprasPublicas.Migrations
{
    public partial class liciatacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NumeroCertame",
                table: "Licitacoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NumeroProcesso",
                table: "Licitacoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroCertame",
                table: "Licitacoes");

            migrationBuilder.DropColumn(
                name: "NumeroProcesso",
                table: "Licitacoes");
        }
    }
}
