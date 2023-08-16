using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository.Repository;

public class CryptoListRepository:RepositoryBase<CryptoList>,ICryptoListRepository
{
    public CryptoListRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public void CreateRecord(CryptoList list) => Create(list);

    public void DeleteRecord(CryptoList list)=> Delete(list);

    public void UpdateRecord(CryptoList list)=> Update(list);

    public async Task<CryptoList> GetRecordById(int id) => await FindByCondition(e => e.Id.Equals(id)).FirstOrDefaultAsync();

    public async Task<CryptoList> GetRecordByCryptoIdAndWatchlistId(int cryptoId, int watchlistId) =>
        await FindByCondition(e => e.CryptoCurrencyId.Equals(cryptoId) && e.WatchlistId.Equals(watchlistId))
            .FirstOrDefaultAsync();

    public async Task<IEnumerable<CryptoList>> GetAll() => await FindAll().ToListAsync();

    public async Task<IEnumerable<CryptoList>> GetWatchlistFromCryptoId(int cryptoId) =>
        await FindByCondition(e => e.CryptoCurrencyId.Equals(cryptoId)).OrderByDescending(e => e.DateCreated)
            .ToListAsync();

    public async Task<IEnumerable<CryptoList>> GetCryptosFromWatchlistId(int watchlistId) =>await FindByCondition(e => e.WatchlistId.Equals(watchlistId)).OrderByDescending(e => e.DateCreated)
        .ToListAsync();
}