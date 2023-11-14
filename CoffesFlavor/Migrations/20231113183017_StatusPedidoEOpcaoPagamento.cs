using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffesFlavor.Migrations
{
    /// <inheritdoc />
    public partial class StatusPedidoEOpcaoPagamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatusPedido",
                columns: table => new
                {
                    StatusPedidoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusPedido", x => x.StatusPedidoId);
                });

            

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_StatusPedidoId",
                table: "Pedidos",
                column: "StatusPedidoId");



            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_StatusPedido_StatusPedidoId",
                table: "Pedidos",
                column: "StatusPedidoId",
                principalTable: "StatusPedido",
                principalColumn: "StatusPedidoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_StatusPedido_StatusPedidoId",
                table: "Pedidos");

            

            migrationBuilder.DropTable(
                name: "StatusPedido");

           

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_StatusPedidoId",
                table: "Pedidos");

            

            migrationBuilder.DropColumn(
                name: "StatusPedidoId",
                table: "Pedidos");
        }
    }
}
