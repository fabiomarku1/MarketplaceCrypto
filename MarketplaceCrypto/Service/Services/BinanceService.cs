using System.Text.Json;
using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.SignalR;
using Service.Contracts;
using Shared.Utility;

namespace Service.Services;

public class BinanceService : IBinanceService
{
    private readonly IMapper _mapper;
    private readonly HttpClient _httpClient;
    private readonly IHubContext<SignalHub> _hubContext;
    private readonly ICryptoHubService _hubService;

    public BinanceService(HttpClient httpClient, IHubContext<SignalHub> hubContext, IMapper mapper,
        ICryptoHubService hubService)
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://api.binance.com/")
        };
        _hubContext = hubContext;
        _mapper = mapper;
        _hubService = hubService;
    }


    public async Task<IEnumerable<BinanceData>> GetAllCryptos(string ?sortBy)
    {
        var coins = new List<string>
        {
            "\"BTCUSDT\"",
            "\"ETHUSDT\"",
            "\"ADAUSDT\"",
            "\"BNBUSDT\"",
            "\"XRPUSDT\"",
            "\"SOLUSDT\"",
            "\"DOTUSDT\"",
            "\"DOGEUSDT\"",
            "\"LTCUSDT\"",
            "\"UNIUSDT\"",
            "\"LINKUSDT\"",
            "\"BCHUSDT\"",
            "\"AVAXUSDT\"",
            "\"ALGOUSDT\"",
            "\"ATOMUSDT\"",
            "\"XTZUSDT\"",
            "\"MATICUSDT\"",
            "\"FILUSDT\"",
            "\"ETCUSDT\"",
            "\"VETUSDT\""
        };


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


        // await _hubContext.Clients.All.SendAsync("UpdateCryptocurrencies", crypto);
        await _hubContext.Clients.All.SendAsync("UpdateMarket", binanceDataList);
     //   var timer = new Timer(async _ => await UpdateValues(), null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

     if (sortBy is not null)
     {
         return await SortData(sortBy, binanceDataList);
     }

        return binanceDataList;
    }


    public async Task UpdateValues()
    {
        var coins = new List<string>
        {
            "\"BTCUSDT\"",
            "\"ETHUSDT\"",
            "\"LTCUSDT\"",
            "\"XRPUSDT\"",
            "\"BNBUSDT\"",
            "\"ADAUSDT\"",
            "\"DOTUSDT\"",
            "\"UNIUSDT\"",
            "\"LINKUSDT\"",
            "\"SOLUSDT\"",
            "\"DOGEUSDT\"",
            "\"MATICUSDT\"",
            "\"VETUSDT\"",
            "\"XLMUSDT\"",
            "\"ATOMUSDT\"",
            "\"ICPUSDT\"",
            "\"FILUSDT\"",
            "\"ETCUSDT\"",
            "\"XEMUSDT\"",
            "\"THETAUSDT\""
        };


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

      //  var crypto = _mapper.Map<IEnumerable<CryptoCurrency>>(binanceDataList);

        await _hubContext.Clients.All.SendAsync("updateMarket", binanceDataList);

    }

    public async Task<IEnumerable<BinanceCandlestickData>> GetDataForSymbol(string symbol)
    {
        long startTime = new DateTimeOffset(DateTime.Now.Date).ToUnixTimeMilliseconds();
        var apiUrl = $"https://api.binance.com/api/v3/klines?symbol={symbol}&interval=1h&startTime={startTime}";

        var response = await _httpClient.GetAsync(apiUrl);

        if (!response.IsSuccessStatusCode) return null;

        var jsonResponse = await response.Content.ReadAsStringAsync();
        List<List<object>> dataLists = JsonSerializer.Deserialize<List<List<object>>>(jsonResponse);

        List<BinanceCandlestickData> candlestickDataList = new List<BinanceCandlestickData>();

        foreach (var dataList in dataLists)
        {
            var test = dataList[1].ToString();
            var candlestickData = new BinanceCandlestickData
            {
                Symbol = symbol,
                Open = decimal.Parse(dataList[1].ToString()),
                High = decimal.Parse(dataList[2].ToString()),
                Low = decimal.Parse(dataList[3].ToString()),
                Close = decimal.Parse(dataList[4].ToString()),
                Time = DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(dataList[0].ToString())).UtcDateTime
            };

            candlestickDataList.Add(candlestickData);
        }

        return candlestickDataList;

    }

    #region  private

    private async Task<IEnumerable<BinanceData>> SortData(string sortBy, IEnumerable<BinanceData> list)
    {
        IEnumerable<BinanceData> sortedList;

        switch (sortBy)
        {
            case "symbol":
                sortedList = list.OrderBy(coin => coin.Symbol);
                break;
            case "lastPrice":
                sortedList = list.OrderBy(coin => coin.LastPrice);
                break;
            case "openPrice":
                sortedList = list.OrderBy(coin => coin.OpenPrice);
                break;
            case "highPrice":
                sortedList = list.OrderBy(coin => coin.HighPrice);
                break;
            case "change24h":
                sortedList = list.OrderBy(coin => (coin.LastPrice - coin.OpenPrice) / coin.OpenPrice * 100);
                break;
            case "volume":
                sortedList = list.OrderBy(coin => coin.Volume);
                break;
            default:
                sortedList = list; // Default sorting order if sortBy is not recognized
                break;
        }

        return await Task.FromResult(sortedList);
    }

    #endregion
}