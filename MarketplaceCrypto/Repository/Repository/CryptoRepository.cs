using System.Linq.Expressions;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository.Repository;

public class CryptoRepository : RepositoryBase<CryptoCurrency>, ICryptoRepository
{
    public CryptoRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public void CreateRecord(CryptoCurrency crypto) => Create(crypto);

    public void UpdateRecord(CryptoCurrency crypto) => Update(crypto);

    public async Task<IEnumerable<CryptoCurrency>> GetAllCrypto() => await FindAll().ToListAsync();

    public async Task<CryptoCurrency> GetRecordById(int cryptoId) =>
        await FindByCondition(e => e.Id.Equals(cryptoId)).FirstOrDefaultAsync();

    public async Task<CryptoCurrency> GetRecordBySymbol(string symbol) =>
        await FindByCondition(e => e.Symbol.Equals(symbol)).FirstOrDefaultAsync();

    public async Task<IEnumerable<CryptoCurrency>> GetRecordsByStartingPattern(string pattern)
    {
        return await FindByCondition(e => e.Symbol.StartsWith(pattern))
            .ToListAsync();
    }
}