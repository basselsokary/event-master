using EventMaster.Application.DTOs;
using EventMaster.Domain.Enums;

namespace EventMaster.Application.Common.Interfaces.Authentication;

public interface IIdentityService
{
    Task<UserDto?> GetUserDtoByEmailAsync(string email);

    Task<UserDto?> GetUserDtoByIdAsync(string userId);

    Task<string?> GetUserNameAsync(string userId);

    Task<string?> GetRolesAsync(string userId);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result Result, string UserId)> CreateUserAsync(string userName, string email, string password, Role role);
    
    Task<Result> DeleteUserAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> CheckPasswordAsync(string email, string password);

    Task<bool> IsUserExistAsync(string email);

    Task<bool> AddRoleToUserAsync(string userId, string role);
}
