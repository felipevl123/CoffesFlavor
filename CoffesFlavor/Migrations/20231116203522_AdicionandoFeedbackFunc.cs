using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffesFlavor.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoFeedbackFunc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AvaliacaoPedidos",
                columns: table => new
                {
                    AvaliacaoPedidoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedidoId = table.Column<int>(type: "int", nullable: false),
                    FeedBackCliente = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvaliacaoPedidos", x => x.AvaliacaoPedidoId);
                    table.ForeignKey(
                        name: "FK_AvaliacaoPedidos_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "PedidoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AvaliacaoPedidos_PedidoId",
                table: "AvaliacaoPedidos",
                column: "PedidoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvaliacaoPedidos");
        }
    }
}
