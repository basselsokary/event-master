namespace EventMaster.Application.EntityRequests.Auth.Commands.OrganizerStatus;

internal class ChangeOrganizerStatusCommandHandler(IUnitOfWork unitOfWork)
    : ICommandHandler<ChangeOrganizerStatusCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(ChangeOrganizerStatusCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Users.ApproveOrganizerAsync(request.OrganizerId, request.IsApproved, cancellationToken);
        if (result)
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        return Result.Failure(["Organizer doesn't exist."]);
    }
}