using EventMaster.Application.Common.Interfaces.Authentication;
using EventMaster.Application.Helpers;
using EventMaster.Application.DTOs;
using EventMaster.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Shared.Models;
using EventMaster.Domain.Constants;

namespace EventMaster.Infrastructure.User.Services;

public class IdentityService : IIdentityService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IUserClaimsPrincipalFactory<AppUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;

    public IdentityService(
        UserManager<AppUser> userManager,
        IUserClaimsPrincipalFactory<AppUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService)
    {
        _userManager = userManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
    }

    public async Task<UserDto?> GetUserDtoByEmailAsync(string email)
    {
        var user = await GetUserByEmailAsync(email);

        return user == null ? null
            : await ToUserDto(user);
    }

    public async Task<UserDto?> GetUserDtoByIdAsync(string userId)
    {
        var user = await GetUserByIdAsync(userId);

        return user == null ? null
            : await ToUserDto(user);
    }

    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await GetUserByIdAsync(userId);

        return user?.UserName;
    }

    public async Task<string?> GetRolesAsync(string userId)
    {
        var user = await GetUserByIdAsync(userId);
        if (user == null)
            return null;

        return (await _userManager.GetRolesAsync(user)).FirstOrDefault();
    }

    private async Task<ICollection<string>> GetRolesAsync(AppUser user)
        => await _userManager.GetRolesAsync(user);

    public async Task<(Result Result, string UserId)> CreateUserAsync(
        string userName,
        string email,
        string password,
        Role role)
    {
        AppUser user = role switch
        {
            Role.Admin => new Admin(),
            Role.EventOrganizer => new EventOrganizer(),
            Role.Participant => new Participant(),
            _ => throw new ArgumentException($"Invalid Role")
        };

        user.Email = email;
        user.UserName = userName;

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException("Failed to create user.");
        }

        result = await _userManager.AddToRoleAsync(user, role.GetName());

        return (result.ToApplicationResult(), user.Id);
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = await GetUserByIdAsync(userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> IsUserExistAsync(string email)
    {
        var user = await GetUserByEmailAsync(email);
        return user == null ? false : true;
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = await GetUserByIdAsync(userId);

        if (user == null)
            return false;

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<Result> DeleteUserAsync(string userId)
    {
        var user = await GetUserByIdAsync(userId);

        return user != null ? await DeleteUserAsync(user) : Result.Success();
    }

    public async Task<Result> DeleteUserAsync(AppUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }

    private async Task<AppUser?> GetUserByEmailAsync(string email)
        => await _userManager.FindByEmailAsync(email);
    private async Task<AppUser?> GetUserByIdAsync(string userId)
        => await _userManager.FindByIdAsync(userId);

    public async Task<bool> CheckPasswordAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user != null && await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task<bool> AddRoleToUserAsync(string userId, string role)
    {
        var user = await GetUserByIdAsync(userId);
        if (user == null)
            return false;

        var identityResult = await _userManager.AddToRoleAsync(user, role);
        return identityResult.Succeeded;
    }

    private async Task<UserDto> ToUserDto(AppUser user)
    {
        return new()
        {
            Id = user.Id,
            Email = user.Email!,
            UserName = user.UserName!,
            Roles = await GetRolesAsync(user)
        };
    }
}
