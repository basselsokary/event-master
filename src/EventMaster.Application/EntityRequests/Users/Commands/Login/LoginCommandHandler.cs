using EventMaster.Application.Common.Interfaces.Authentication;
using EventMaster.Application.DTOs;
using EventMaster.Domain.Constants;
using EventMaster.Domain.Errors;

namespace EventMaster.Application.EntityRequests.Users.Commands.Login;

internal class LoginCommandHandler(
    IIdentityService userService,
    ITokenProvider tokenProvider,
    IUnitOfWork unitOfWork) : ICommandHandler<LoginCommand, Response>
{
    private readonly IIdentityService _userService = userService;
    private readonly ITokenProvider _tokenProvider = tokenProvider;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Response>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserDtoByEmailAsync(request.Email);
        if (user == null)
            return Result.Failure<Response>(UserErrors.NotFound(request.Email));

        if(!await HandleOrganizerApproval(user))
            return Result.Failure<Response>(UserErrors.OrganizerNotAllowed());

        var result = await _userService.CheckPasswordAsync(user.Email, request.Password);
        if (!result)
            return Result.Failure<Response>(UserErrors.InvalidCredentials());

        string accessToken = _tokenProvider.GenerateAccessToken(user.Id, user.Email, user.Roles);
        string refreshToken = _tokenProvider.GenerateRefreshToken();

        await _unitOfWork.RefreshTokens.AddRefreshTokenAsync(user.Id, refreshToken, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(new Response(accessToken, refreshToken));
    }

    private async Task<bool> HandleOrganizerApproval(UserDto user)
    {
        if (user.Roles.Contains(UserRoles.EventOrganizer)
            && !await _unitOfWork.Users.IsOrganizerApprovedAsync(user.Id))
        {
            return false;
        }
        
        return true;
    }
}
