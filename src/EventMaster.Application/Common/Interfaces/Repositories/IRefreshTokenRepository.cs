using EventMaster.Application.DTOs;

namespace EventMaster.Application.Common.Interfaces.Repositories;

public interface IRefreshTokenRepository
{
    Task AddRefreshTokenAsync(string userId, string refreshToken, CancellationToken cancellationToken = default);
    Task<UserDto?> GetUserDtoByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
    Task<bool> IsRefreshTokenActiveAsync(string refreshToken, CancellationToken cancellationToken = default);
    Task<bool> RevokeRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
}
