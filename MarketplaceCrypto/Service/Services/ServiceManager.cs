using AutoMapper;
using Cryptography;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Repository.Contracts;
using Service.Contracts;
using Shared.Utility;

namespace Service.Services;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IUserService> _userService;
    private readonly Lazy<ICryptoHubService> _cryptoHubService;
    private readonly Lazy<IBinanceService> _binanceService;
    private readonly Lazy<IWatchlistService> _watchlistService;
    private readonly Lazy<ICryptocurrencyService> _cryptocurrencyService;
    public ServiceManager(IRepositoryManager repositoryManager
    , IMapper mapper
    , ILoggerManager logger
    , UserManager<ApplicationUser> userManager
    , SignInManager<ApplicationUser> signInManager
    , IOptions<JwtConfiguration> jwtConfiguration
    , ICryptoUtils cryptoUtils
    , IHubContext<SignalHub> hub
    ,HttpClient httpClient
    ,ICryptoHubService hubService
         )
    {
        _userService = new Lazy<IUserService>(() => new UserService(logger,mapper,repositoryManager,userManager,signInManager,cryptoUtils,jwtConfiguration));
        _cryptoHubService = new Lazy<ICryptoHubService>(() => new CryptoHubService(hub));
        _binanceService = new Lazy<IBinanceService>(() => new BinanceService(httpClient, hub, mapper,hubService));
        _watchlistService = new Lazy<IWatchlistService>(() => new WatchlistService(logger, mapper, repositoryManager,httpClient));
        _cryptocurrencyService = new Lazy<ICryptocurrencyService>(() =>
            new CryptocurrencyService(logger, mapper, repositoryManager, httpClient));
    }
    public IUserService UserService => _userService.Value;
    public ICryptoHubService CryptoHubService => _cryptoHubService.Value;
    public IBinanceService BinanceService => _binanceService.Value;
    public IWatchlistService WatchlistService => _watchlistService.Value;
    public ICryptocurrencyService CryptocurrencyService => _cryptocurrencyService.Value;
}