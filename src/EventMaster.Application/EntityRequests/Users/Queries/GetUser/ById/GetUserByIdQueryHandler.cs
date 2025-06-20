using EventMaster.Application.Common.Interfaces.Authentication;
using EventMaster.Application.DTOs;
using EventMaster.Domain.Errors;

namespace EventMaster.Application.EntityRequests.Users.Queries.GetUser.ById;

internal class GetUserByIdQueryHandler(IIdentityService userService)
    : IQueryHandler<GetUserByIdQuery, Response>
{
    private readonly IIdentityService _userService = userService;

    public async Task<Result<Response>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserDtoByIdAsync(request.UserId);

        if (user == null)
            return Result.Failure<Response>(UserErrors.NotFound(request.UserId));

        return Result.Success(ToResponse(user));
    }
    
    private static Response? ToResponse(UserDto user)
    {
        return new Response(user.Id, user.UserName, user.Email, user.Roles);
    }
}
