using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffesFlavor.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoObservacaoPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ObservacaoPedido",
                table: "Pedidos",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ObservacaoPedido",
                table: "Pedidos");
        }
    }
}
