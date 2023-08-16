using Repository.Contracts;

namespace Repository.Repository;

public class RepositoryManager:IRepositoryManager
{   
    private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<IUserRepository> _userRepository;
    private readonly Lazy<ICryptoListRepository> _cryptoListRepository;
    private readonly Lazy<IWatchlistRepository> _watchlistRepository;
    private readonly Lazy<ICryptoRepository> _cryptoRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _userRepository = new Lazy<IUserRepository>(() => new UserRepository(repositoryContext));
        _cryptoListRepository = new Lazy<ICryptoListRepository>(() => new CryptoListRepository(repositoryContext));
        _cryptoRepository = new Lazy<ICryptoRepository>(() => new CryptoRepository(repositoryContext));
        _watchlistRepository = new Lazy<IWatchlistRepository>(() => new WatchlistRepository(repositoryContext));
    }


    public IUserRepository UserRepository => _userRepository.Value;
    public ICryptoListRepository CryptoListRepository => _cryptoListRepository.Value;
    public IWatchlistRepository WatchlistRepository => _watchlistRepository.Value;
    public ICryptoRepository CryptoCurrencyRepository => _cryptoRepository.Value;

    public async Task SaveAsync()
    {
        _repositoryContext.ChangeTracker.AutoDetectChangesEnabled = false;
        await _repositoryContext.SaveChangesAsync();
    }
}