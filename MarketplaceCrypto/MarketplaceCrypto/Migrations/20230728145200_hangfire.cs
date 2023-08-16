using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoMarketplace.Migrations
{
    /// <inheritdoc />
    public partial class hangfire : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "CryptoCurrencies");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "CryptoCurrencies",
                newName: "Volume");

            migrationBuilder.RenameColumn(
                name: "MarketCapitalization",
                table: "CryptoCurrencies",
                newName: "QuoteVolume");

            migrationBuilder.AddColumn<decimal>(
                name: "HighPrice",
                table: "CryptoCurrencies",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LowPrice",
                table: "CryptoCurrencies",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HighPrice",
                table: "CryptoCurrencies");

            migrationBuilder.DropColumn(
                name: "LowPrice",
                table: "CryptoCurrencies");

            migrationBuilder.RenameColumn(
                name: "Volume",
                table: "CryptoCurrencies",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "QuoteVolume",
                table: "CryptoCurrencies",
                newName: "MarketCapitalization");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CryptoCurrencies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
