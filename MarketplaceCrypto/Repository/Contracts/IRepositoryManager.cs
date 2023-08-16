namespace Repository.Contracts;

public interface IRepositoryManager
{
    IUserRepository UserRepository { get; }
    IWatchlistRepository WatchlistRepository { get; }
    ICryptoListRepository CryptoListRepository { get; }
    ICryptoRepository CryptoCurrencyRepository { get; }
    Task SaveAsync();
}