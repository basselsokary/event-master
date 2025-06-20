using EventMaster.Application.Common.Interfaces.Authentication;
using EventMaster.Domain.Errors;

namespace EventMaster.Application.EntityRequests.Auth.Commands.Refresh;

public class RefreshCommandHandler(
    IUnitOfWork unitOfWork,
    ITokenProvider tokenProvider) : ICommandHandler<RefreshCommand, Response>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ITokenProvider _tokenProvider = tokenProvider;

    public async Task<Result<Response>> Handle(RefreshCommand request, CancellationToken cancellationToken)
    {
        var userDto = await _unitOfWork.RefreshTokens.GetUserDtoByRefreshTokenAsync(request.RefreshToken, cancellationToken);
        if (userDto == null || !await _unitOfWork.RefreshTokens.IsRefreshTokenActiveAsync(request.RefreshToken, cancellationToken))
            return Result.Failure<Response>(UserErrors.InvalidRefreshToken());

        if (!await _unitOfWork.RefreshTokens.RevokeRefreshTokenAsync(request.RefreshToken))
            return Result.Failure<Response>(["Failed to revoke the refresh token."]);

        var newAccessToken = _tokenProvider.GenerateAccessToken(userDto.Id, userDto.Email, userDto.Roles);
        var newRefreshToken = _tokenProvider.GenerateRefreshToken();

        await _unitOfWork.RefreshTokens.AddRefreshTokenAsync(userDto.Id, newRefreshToken, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(new Response(newAccessToken, newRefreshToken));
    }
}
