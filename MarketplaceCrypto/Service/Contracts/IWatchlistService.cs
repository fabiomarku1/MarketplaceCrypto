using Entities.Models;

namespace Service.Contracts;

public interface IWatchlistService
{
    Task AddItemToWatchlist(string symbol,int userId);
    Task<IEnumerable<BinanceData>> GetWatchlistItems(int userid);
    Task RemoveFromWatchlist(int userId, string symbol);
}