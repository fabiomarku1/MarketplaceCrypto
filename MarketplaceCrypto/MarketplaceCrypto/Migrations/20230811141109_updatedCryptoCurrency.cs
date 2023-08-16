using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoMarketplace.Migrations
{
    /// <inheritdoc />
    public partial class updatedCryptoCurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangePercentage",
                table: "CryptoCurrencies");

            migrationBuilder.DropColumn(
                name: "HighPrice",
                table: "CryptoCurrencies");

            migrationBuilder.DropColumn(
                name: "LastPrice",
                table: "CryptoCurrencies");

            migrationBuilder.DropColumn(
                name: "LowPrice",
                table: "CryptoCurrencies");

            migrationBuilder.DropColumn(
                name: "OpenPrice",
                table: "CryptoCurrencies");

            migrationBuilder.DropColumn(
                name: "QuoteVolume",
                table: "CryptoCurrencies");

            migrationBuilder.DropColumn(
                name: "Volume",
                table: "CryptoCurrencies");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ChangePercentage",
                table: "CryptoCurrencies",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "HighPrice",
                table: "CryptoCurrencies",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LastPrice",
                table: "CryptoCurrencies",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LowPrice",
                table: "CryptoCurrencies",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OpenPrice",
                table: "CryptoCurrencies",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "QuoteVolume",
                table: "CryptoCurrencies",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Volume",
                table: "CryptoCurrencies",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
