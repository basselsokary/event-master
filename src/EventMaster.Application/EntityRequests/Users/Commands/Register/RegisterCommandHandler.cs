using EventMaster.Application.Common.Interfaces.Authentication;
using EventMaster.Domain.Errors;

namespace EventMaster.Application.EntityRequests.Users.Commands.Register;

internal class RegisterCommandHandler(IIdentityService userService)
    : ICommandHandler<RegisterCommand>
{
    private readonly IIdentityService _userService = userService;

    public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var userExist = await _userService.IsUserExistAsync(request.Email);
        if (userExist)
            return Result.Failure(UserErrors.AlreadyExist(request.Email));

        var result = await _userService.CreateUserAsync(
            request.UserName,
            request.Email,
            request.Password,
            request.Role);
            
        if (!result.Result.Succeeded)
            return result.Result;

        return Result.Success();
    }
}
