using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciadorComprasPublicas.Migrations
{
    public partial class quanitadeItemLicitacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuantidadeItem",
                table: "Licitacoes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantidadeItem",
                table: "Licitacoes");
        }
    }
}
