namespace EventMaster.Application.EntityRequests.Users.Queries.GetUsers.GetParticipantsForAdmin;

public class GetParticipantsForAdminQueryHandler(IUnitOfWork unitOfWork)
    : IQueryHandler<GetParticipantsForAdminQuery, List<Response>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<List<Response>>> Handle(GetParticipantsForAdminQuery request, CancellationToken cancellationToken)
    {
        var participants = await _unitOfWork.Users.GetParticipantsAsync(cancellationToken);

        return Result.Success(participants.Select(p => new Response(p.Id, p.UserName, p.Email)).ToList());
    }
}