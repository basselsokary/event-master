namespace EventMaster.Application.EntityRequests.Auth.Queries.GetUsers.GetOrganizersForAdmin;

public class GetOrganizersForAdminsQueryHandler(IUnitOfWork unitOfWork)
    : IQueryHandler<GetOrganizersForAdminsQuery, List<Response>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<List<Response>>> Handle(GetOrganizersForAdminsQuery request, CancellationToken cancellationToken)
    {
        var organizers = await _unitOfWork.Users.GetOrganizersAsync(request.Status, cancellationToken);

        return Result.Success(organizers.Select(o => new Response(o.Id, o.UserName, o.Email, o.Status)).ToList());
    }
}