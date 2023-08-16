using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository.Repository;

public class UserRepository:RepositoryBase<ApplicationUser>,IUserRepository

{
    public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public void CreateRecord(ApplicationUser user) => Create(user);
    public void UpdateRecord(ApplicationUser user) => Update(user);
    public void DeleteRecord(ApplicationUser user) => Delete(user);

    public async Task<IEnumerable<ApplicationUser>> GetAllUsers() =>
        await FindAll().ToListAsync();

    public async Task<ApplicationUser> GetRecordById(int userId) =>
        await FindByCondition(e => e.Id.Equals(userId) ).FirstOrDefaultAsync();

    public async Task<ApplicationUser> GetRecordByEmail(string email) =>
        await FindByCondition(e => e.Email.Equals(email) ).FirstOrDefaultAsync();
    
    public async Task<ApplicationUser> GetRecordByPhone(string phone) => 
        await FindByCondition(e => e.PhoneNumber.Equals(phone) ).FirstOrDefaultAsync();

}