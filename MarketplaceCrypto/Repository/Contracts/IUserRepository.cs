using Entities.Models;

namespace Repository.Contracts;

public interface IUserRepository
{
    void CreateRecord(ApplicationUser user);
    void UpdateRecord(ApplicationUser user);
    void DeleteRecord(ApplicationUser user);
    Task<IEnumerable<ApplicationUser>> GetAllUsers();
    Task<ApplicationUser> GetRecordById(int userId);
    Task<ApplicationUser> GetRecordByEmail(string email);
    Task<ApplicationUser> GetRecordByPhone(string phone);

}