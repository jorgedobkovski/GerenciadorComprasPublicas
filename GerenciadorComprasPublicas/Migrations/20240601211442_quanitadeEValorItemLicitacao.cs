using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciadorComprasPublicas.Migrations
{
    public partial class quanitadeEValorItemLicitacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValorHomologado",
                table: "Licitacoes",
                newName: "ValorHomologadoTotal");

            migrationBuilder.RenameColumn(
                name: "ValorEstimado",
                table: "Licitacoes",
                newName: "ValorHomologadoItem");

            migrationBuilder.AddColumn<decimal>(
                name: "ValorEstimadoItem",
                table: "Licitacoes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorEstimadoTotal",
                table: "Licitacoes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorEstimadoItem",
                table: "Licitacoes");

            migrationBuilder.DropColumn(
                name: "ValorEstimadoTotal",
                table: "Licitacoes");

            migrationBuilder.RenameColumn(
                name: "ValorHomologadoTotal",
                table: "Licitacoes",
                newName: "ValorHomologado");

            migrationBuilder.RenameColumn(
                name: "ValorHomologadoItem",
                table: "Licitacoes",
                newName: "ValorEstimado");
        }
    }
}
