using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffesFlavor.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoOpcaoPagamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "OpcaoPagamento",
                table: "Pedidos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OpcaoPagamento",
                table: "Pedidos");

            migrationBuilder.AddColumn<int>(
                name: "OpcaoPagamentoId",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "OpcaoPagamento",
                columns: table => new
                {
                    OpcaoPagamentoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OpcoesPagamento = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpcaoPagamento", x => x.OpcaoPagamentoId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_OpcaoPagamentoId",
                table: "Pedidos",
                column: "OpcaoPagamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_OpcaoPagamento_OpcaoPagamentoId",
                table: "Pedidos",
                column: "OpcaoPagamentoId",
                principalTable: "OpcaoPagamento",
                principalColumn: "OpcaoPagamentoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
