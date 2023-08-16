using Microsoft.AspNetCore.Identity;
using Shared.DTO;
using System.Security.Claims;

namespace Service.Contracts;

public interface IUserService
{
    Task<bool> CreateUser(RegisterUserDTO request);
    Task<bool> UpdateUser(int userId,UpdateUserDTO request);
    Task<bool> DeleteUser(int userId);
    Task<GetUserDTO> GetUserById(int userId);
    Task<IEnumerable<GetUserDTO>> GetListOfUsers(); //Do pagination
    Task<ClaimsIdentity> SignUpUserAsync(RegisterUserDTO signUp);
    Task<ClaimsIdentity> Login(LoginUserDTO login);

}