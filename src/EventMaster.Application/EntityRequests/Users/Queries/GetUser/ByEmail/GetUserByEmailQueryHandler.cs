using EventMaster.Application.Common.Interfaces.Authentication;
using EventMaster.Application.DTOs;
using EventMaster.Domain.Errors;

namespace EventMaster.Application.EntityRequests.Users.Queries.GetUser.ByEmail;

internal class GetUserByEmailQueryHandler(
    IIdentityService identityService,
    IUserContext userContext) : IQueryHandler<GetUserByEmailQuery, Response>
{
    private readonly IIdentityService _identityService = identityService;
    private readonly IUserContext _userContext = userContext;

    public async Task<Result<Response>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
       var user = await _identityService.GetUserDtoByEmailAsync(request.Email);

       if (user is null)
           return Result.Failure<Response>(UserErrors.NotFound(request.Email));

       return Result.Success(ToResponse(user));
    }

    private static Response? ToResponse(UserDto user)
    {
        return new Response(user.Id, user.UserName, user.Email, user.Roles);
    }
}