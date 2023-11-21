using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffesFlavor.Migrations
{
    /// <inheritdoc />
    public partial class PopulandoStatusPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO StatusPedido(Status) " +
                "VALUES('Em preparação')");
            migrationBuilder.Sql("INSERT INTO StatusPedido(Status) " +
                "VALUES('Saiu para entrega')");
            migrationBuilder.Sql("INSERT INTO StatusPedido(Status) " +
                "VALUES('Concluido')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM StatusPedido");
        }
    }
}
