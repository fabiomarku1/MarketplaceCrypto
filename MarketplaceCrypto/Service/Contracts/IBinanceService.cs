using Entities.Models;

namespace Service.Contracts;

public interface IBinanceService
{
   // Task FetchAndNotifyClients();
    Task<IEnumerable<BinanceData>> GetAllCryptos(string? sortBy);
    Task UpdateValues();
    Task<IEnumerable<BinanceCandlestickData>> GetDataForSymbol(string symbol,string? interval);
    Task<IEnumerable<BinanceCandlestickData>> GetDataForCustomRange(string symbol,DateTime startTime,DateTime endTime);
}