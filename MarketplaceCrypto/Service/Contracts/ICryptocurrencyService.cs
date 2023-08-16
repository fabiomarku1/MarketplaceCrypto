using Entities.Models;
using Shared.DTO;

namespace Service.Contracts;

public interface ICryptocurrencyService
{
    Task<IEnumerable<BinanceData>> GetCryptocurrencyByPattern(string pattern);
}