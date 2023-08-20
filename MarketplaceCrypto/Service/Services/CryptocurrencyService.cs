using System.Text.Json;
using AutoMapper;
using Entities.Exceptions;
using Entities.Models;
using Repository.Contracts;
using Service.Contracts;
using Shared.DTO;

namespace Service.Services;

public class CryptocurrencyService:ICryptocurrencyService
{
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repositoryManager;
    private readonly HttpClient _httpClient;

    public CryptocurrencyService(ILoggerManager logger, IMapper mapper, IRepositoryManager repositoryManager, HttpClient httpClient)
    {
        _logger = logger;
        _mapper = mapper;
        _repositoryManager = repositoryManager;
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://api.binance.com/")
        };
    }

    public async Task<IEnumerable<BinanceData>> GetCryptocurrencyByPattern(string pattern)
    {
        var list = await _repositoryManager.CryptoCurrencyRepository.GetRecordsByStartingPattern(pattern);
        if (list is null)
            throw new NotFoundException($"No cryptos were found for the patter {pattern}");
       
        if(!list.Any())
            throw new NotFoundException($"No data found for the pattern {pattern}");

        var coins = list.Select(i => $"\"{i.Symbol}USDT\"").ToList();

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
      //  return _mapper.Map<IEnumerable<GetCryptocurrencyDTO>>(list);
    }

   
}