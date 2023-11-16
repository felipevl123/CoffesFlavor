using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffesFlavor.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaCupomDesconto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CupomDesconto",
                columns: table => new
                {
                    CupomDescontoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cupom = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Desconto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CupomDesconto", x => x.CupomDescontoId);
                });

            migrationBuilder.CreateTable(
                name: "PedidosComDesconto",
                columns: table => new
                {
                    PedidosComDescontoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedidoId = table.Column<int>(type: "int", nullable: false),
                    CupomDescontoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidosComDesconto", x => x.PedidosComDescontoId);
                    table.ForeignKey(
                        name: "FK_PedidosComDesconto_CupomDesconto_CupomDescontoId",
                        column: x => x.CupomDescontoId,
                        principalTable: "CupomDesconto",
                        principalColumn: "CupomDescontoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidosComDesconto_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "PedidoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidosComDesconto_CupomDescontoId",
                table: "PedidosComDesconto",
                column: "CupomDescontoId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidosComDesconto_PedidoId",
                table: "PedidosComDesconto",
                column: "PedidoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidosComDesconto");

            migrationBuilder.DropTable(
                name: "CupomDesconto");
        }
    }
}
