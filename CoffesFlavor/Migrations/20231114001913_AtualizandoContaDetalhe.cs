using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffesFlavor.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoContaDetalhe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "ContaDetalhes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "ContaDetalhes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "ContaDetalhes");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "ContaDetalhes");
        }
    }
}
