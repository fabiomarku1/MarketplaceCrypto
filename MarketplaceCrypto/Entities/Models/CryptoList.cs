using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class CryptoList
{
    [Key] public int Id { get; set; }
    [ForeignKey("CryptoCurrencyId")]
    public int CryptoCurrencyId { get; set; }
    public CryptoCurrency? CryptoCurrency { get; set; }

   [ForeignKey("WatchlistId")] public int WatchlistId { get; set; }
   public Watchlist ? Watchlist { get; set; }

   public DateTime DateCreated { get; set; }
}