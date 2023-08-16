namespace Service.Contracts;

public interface IServiceManager
{
    IUserService UserService { get; }
    IBinanceService BinanceService { get; }
    ICryptoHubService CryptoHubService { get; }
    IWatchlistService WatchlistService { get; }
    ICryptocurrencyService CryptocurrencyService { get; }
}