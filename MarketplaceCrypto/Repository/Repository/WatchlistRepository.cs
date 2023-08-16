using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository.Repository;

public class WatchlistRepository:RepositoryBase<Watchlist>,IWatchlistRepository
{
    public WatchlistRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public void CreateRecord(Watchlist watchlist)=>Create(watchlist);
    public void UpdateRecord(Watchlist watchlist) => Update(watchlist);
    public void DeleteRecord(Watchlist watchlist) => Delete(watchlist);

    public async Task<Watchlist> GetRecordById(int id) => await FindByCondition(e => e.Id.Equals(id)).FirstOrDefaultAsync();

    public async Task<IEnumerable<Watchlist>> GetAllWatchlistElements() => await FindAll()
        .OrderBy(e=>e.DateCreated)
        .ToListAsync();

    public async Task<Watchlist> GetWatchlistFormUserId(int userId) =>
        await FindByCondition(e => e.UserId.Equals(userId)).FirstOrDefaultAsync();
            
}