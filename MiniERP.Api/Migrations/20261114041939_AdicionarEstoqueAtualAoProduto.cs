using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniERP.Api.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarEstoqueAtualAoProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "EstoqueAtual",
                table: "Produtos",
                type: "decimal(18,3)",
                precision: 18,
                scale: 3,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstoqueAtual",
                table: "Produtos");
        }
    }
}
