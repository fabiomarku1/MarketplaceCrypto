using Entities.Models;

namespace Repository.Contracts;

public interface ICryptoRepository
{
    void CreateRecord(CryptoCurrency crypto);
    void UpdateRecord(CryptoCurrency crypto);
    Task<IEnumerable<CryptoCurrency>> GetAllCrypto();
    Task<CryptoCurrency> GetRecordById(int cryptoId);
    Task<CryptoCurrency> GetRecordBySymbol(string symbol);
    Task<IEnumerable<CryptoCurrency>> GetRecordsByStartingPattern(string pattern);

}