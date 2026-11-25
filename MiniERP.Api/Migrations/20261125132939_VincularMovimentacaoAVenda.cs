using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniERP.Api.Migrations
{
    /// <inheritdoc />
    public partial class VincularMovimentacaoAVenda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemVendaId",
                table: "MovimentacoesEstoque",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VendaId",
                table: "MovimentacoesEstoque",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacoesEstoque_ItemVendaId",
                table: "MovimentacoesEstoque",
                column: "ItemVendaId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacoesEstoque_VendaId",
                table: "MovimentacoesEstoque",
                column: "VendaId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovimentacoesEstoque_ItensVenda_ItemVendaId",
                table: "MovimentacoesEstoque",
                column: "ItemVendaId",
                principalTable: "ItensVenda",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovimentacoesEstoque_Vendas_VendaId",
                table: "MovimentacoesEstoque",
                column: "VendaId",
                principalTable: "Vendas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovimentacoesEstoque_ItensVenda_ItemVendaId",
                table: "MovimentacoesEstoque");

            migrationBuilder.DropForeignKey(
                name: "FK_MovimentacoesEstoque_Vendas_VendaId",
                table: "MovimentacoesEstoque");

            migrationBuilder.DropIndex(
                name: "IX_MovimentacoesEstoque_ItemVendaId",
                table: "MovimentacoesEstoque");

            migrationBuilder.DropIndex(
                name: "IX_MovimentacoesEstoque_VendaId",
                table: "MovimentacoesEstoque");

            migrationBuilder.DropColumn(
                name: "ItemVendaId",
                table: "MovimentacoesEstoque");

            migrationBuilder.DropColumn(
                name: "VendaId",
                table: "MovimentacoesEstoque");
        }
    }
}
