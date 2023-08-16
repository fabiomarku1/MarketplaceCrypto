using System.Text.Json;
using AutoMapper;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Repository.Contracts;
using Service.Contracts;

namespace Service.Services;

public class WatchlistService : IWatchlistService
{
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repositoryManager;
    private readonly HttpClient _httpClient;

    public WatchlistService(ILoggerManager logger, IMapper mapper, IRepositoryManager repositoryManager, HttpClient httpClient)
    {
        _logger = logger;
        _mapper = mapper;
        _repositoryManager = repositoryManager;
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://api.binance.com/")
        };
    }

    public async Task AddItemToWatchlist(string symbol, int userId)
    {
        var watchlist = await GetWatchlist(userId);
        var crypto = await GetCrypto(symbol);

        await AddCryptoToWatchlist(watchlist, crypto);

    }

    public async Task<IEnumerable<BinanceData>> GetWatchlistItems(int userId)
    {
        var watchlist = await GetWatchlist(userId);
        var cryptoSymbols = await _repositoryManager.CryptoListRepository.GetCryptosFromWatchlistId(watchlist.Id);
        if (cryptoSymbols is null || !cryptoSymbols.Any())
            return null;

        var coins = new List<string>();

        //zgjidhje "naive" , sepse mund te behet thirrja e query-t me DAPPER edhe merren fiks Symbols qe jane
        // ne watchlist-en e userit
        foreach (var i in cryptoSymbols) 
        {
            var crypto = await _repositoryManager.CryptoCurrencyRepository.GetRecordById(i.CryptoCurrencyId);
            coins.Add($"\"{crypto.Symbol}USDT\"");
        }
        
        var symbols = string.Join(",", coins);

        var response = await _httpClient.GetAsync($"api/v3/ticker/24hr?symbols=[{symbols}]&type=MINI");
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new CustomDecimalLongConverter(), new CustomLongConverter() }
        };
        var binanceDataList = JsonSerializer.Deserialize<List<BinanceData>>(jsonResponse, options);

        return binanceDataList;
    }

    public async Task RemoveFromWatchlist(int userId, string symbol)
    {
        var watchlist = await GetWatchlist(userId);
        var crypto = await _repositoryManager.CryptoCurrencyRepository.GetRecordBySymbol(symbol.Replace("USDT", ""));

        var cryptoList = await _repositoryManager.CryptoListRepository.GetCryptosFromWatchlistId(watchlist.Id);

        var cryptoToDelete = cryptoList.SingleOrDefault(e => e.CryptoCurrencyId.Equals(crypto.Id));

        if (cryptoToDelete is not null)
        {
            _repositoryManager.CryptoListRepository.DeleteRecord(cryptoToDelete);
            await _repositoryManager.SaveAsync();
        }

    }

    #region private

    private async Task AddCryptoToWatchlist(Watchlist watchlist, CryptoCurrency crypto)
    {
        var existingItem = await _repositoryManager.CryptoListRepository.GetRecordByCryptoIdAndWatchlistId(crypto.Id, watchlist.Id);
        if (existingItem is not null)
        {
            return;
        }

        var cryptoItem = new CryptoList
        {
            WatchlistId = watchlist.Id,
            CryptoCurrencyId = crypto.Id,
            DateCreated = DateTime.Now
        };

        _repositoryManager.CryptoListRepository.CreateRecord(cryptoItem);
        await _repositoryManager.SaveAsync();
    }

    private async Task<CryptoCurrency> GetCrypto(string symbol)
    {

        var crypto = await _repositoryManager.CryptoCurrencyRepository.GetRecordBySymbol(symbol.Replace("USDT", ""));
        if (crypto is null)
            throw new NotFoundException($"No cryptocurrency was found with symbol {symbol}");

        return crypto;
    }

    private async Task<Watchlist> GetWatchlist(int userId)
    {
        var existingWatchlist = await _repositoryManager.WatchlistRepository.GetWatchlistFormUserId(userId);

        if (existingWatchlist is null)
        {
            var createdWatchlist = await CreateWatchlist(userId);
            return createdWatchlist;
        }

        return existingWatchlist;
    }

    private async Task<Watchlist> CreateWatchlist(int userId)
    {
        var watchlist = new Watchlist
        {
            UserId = userId,
            DateCreated = DateTime.Now
        };
        _repositoryManager.WatchlistRepository.CreateRecord(watchlist);
        await _repositoryManager.SaveAsync();

        return watchlist;
    }

    #endregion


}