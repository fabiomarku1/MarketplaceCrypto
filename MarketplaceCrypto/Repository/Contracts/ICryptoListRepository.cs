using Entities.Models;

namespace Repository.Contracts;

public interface ICryptoListRepository
{
    void CreateRecord(CryptoList list);
    void DeleteRecord(CryptoList list);
    void UpdateRecord(CryptoList list);
    Task<CryptoList> GetRecordById(int id);
    Task<CryptoList> GetRecordByCryptoIdAndWatchlistId(int cryptoId,int watchlistId);
    Task<IEnumerable<CryptoList>> GetAll();
    Task<IEnumerable<CryptoList>> GetWatchlistFromCryptoId(int cryptoId);
    Task<IEnumerable<CryptoList>> GetCryptosFromWatchlistId(int watchlistId);
}