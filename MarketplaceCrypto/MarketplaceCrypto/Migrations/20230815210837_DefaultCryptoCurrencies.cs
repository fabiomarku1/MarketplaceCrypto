using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoMarketplace.Migrations
{
    /// <inheritdoc />
    public partial class DefaultCryptoCurrencies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("IF NOT EXISTS (SELECT 1 FROM CryptoCurrencies) " +
                                 "BEGIN " +
                                 "INSERT INTO CryptoCurrencies (Symbol, DateCreated) VALUES" +
                                 "('BTC', GETDATE()),     ('ETH', GETDATE())," +
                                 "('ADA', GETDATE()),     ('BNB', GETDATE())," +
                                 "('XRP', GETDATE()),     ('SOL', GETDATE())," +
                                 "('DOT', GETDATE()),     ('DOGE', GETDATE())," +
                                 "('LTC', GETDATE()),     ('UNI', GETDATE())," +
                                 "('LINK', GETDATE()),     ('BCH', GETDATE())," +
                                 "('AVAX', GETDATE()),     ('ALGO', GETDATE())," +
                                 "('ATOM', GETDATE()),     ('XTZ', GETDATE())," +
                                 "('MATIC', GETDATE()),     ('FIL', GETDATE())," +
                                 "('ETC', GETDATE()),     ('VET', GETDATE()) " +
                                 "END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
