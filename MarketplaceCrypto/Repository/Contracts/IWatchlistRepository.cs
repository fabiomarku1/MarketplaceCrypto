using Entities.Models;

namespace Repository.Contracts;

public interface IWatchlistRepository
{
    void CreateRecord(Watchlist watchlist);
    void UpdateRecord(Watchlist watchlist);
    void DeleteRecord(Watchlist watchlist);
    Task<Watchlist> GetRecordById(int id);
    Task<IEnumerable<Watchlist>> GetAllWatchlistElements();
    Task<Watchlist> GetWatchlistFormUserId(int userId);
}