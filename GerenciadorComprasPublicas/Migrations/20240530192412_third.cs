using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciadorComprasPublicas.Migrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoricoPreco_Itens_ItemId",
                table: "HistoricoPreco");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HistoricoPreco",
                table: "HistoricoPreco");

            migrationBuilder.RenameTable(
                name: "HistoricoPreco",
                newName: "HistoricoPrecos");

            migrationBuilder.RenameIndex(
                name: "IX_HistoricoPreco_ItemId",
                table: "HistoricoPrecos",
                newName: "IX_HistoricoPrecos_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistoricoPrecos",
                table: "HistoricoPrecos",
                column: "HistoricoId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricoPrecos_Itens_ItemId",
                table: "HistoricoPrecos",
                column: "ItemId",
                principalTable: "Itens",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoricoPrecos_Itens_ItemId",
                table: "HistoricoPrecos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HistoricoPrecos",
                table: "HistoricoPrecos");

            migrationBuilder.RenameTable(
                name: "HistoricoPrecos",
                newName: "HistoricoPreco");

            migrationBuilder.RenameIndex(
                name: "IX_HistoricoPrecos_ItemId",
                table: "HistoricoPreco",
                newName: "IX_HistoricoPreco_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistoricoPreco",
                table: "HistoricoPreco",
                column: "HistoricoId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricoPreco_Itens_ItemId",
                table: "HistoricoPreco",
                column: "ItemId",
                principalTable: "Itens",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
