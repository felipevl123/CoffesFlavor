using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffesFlavor.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoAvaliacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nota",
                table: "AvaliacaoPedidos",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nota",
                table: "AvaliacaoPedidos");
        }
    }
}
