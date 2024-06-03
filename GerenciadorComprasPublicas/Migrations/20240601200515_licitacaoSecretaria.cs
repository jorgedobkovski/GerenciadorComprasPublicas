using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciadorComprasPublicas.Migrations
{
    public partial class licitacaoSecretaria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SecretariaId",
                table: "Licitacoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Licitacoes_SecretariaId",
                table: "Licitacoes",
                column: "SecretariaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Licitacoes_Secretarias_SecretariaId",
                table: "Licitacoes",
                column: "SecretariaId",
                principalTable: "Secretarias",
                principalColumn: "SecretariaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Licitacoes_Secretarias_SecretariaId",
                table: "Licitacoes");

            migrationBuilder.DropIndex(
                name: "IX_Licitacoes_SecretariaId",
                table: "Licitacoes");

            migrationBuilder.DropColumn(
                name: "SecretariaId",
                table: "Licitacoes");
        }
    }
}
