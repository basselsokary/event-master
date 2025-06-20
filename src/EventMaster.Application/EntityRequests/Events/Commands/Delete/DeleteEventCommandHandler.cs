using EventMaster.Application.Common.Interfaces.Authentication;
using EventMaster.Domain.Errors;

namespace EventMaster.Application.EntityRequests.Events.Commands.Delete;

public class DeleteEventCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext)
    : ICommandHandler<DeleteEventCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserContext _userContext = userContext;

    public async Task<Result> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        var @event = await _unitOfWork.Events.GetByIdAsync(request.Id, cancellationToken: cancellationToken);

        if (@event == null)
            return Result.Failure(EventErrors.NotFound(request.Id));

        if (_userContext.Id != @event.OrganizerId)
            return Result.Failure(EventErrors.NotEventOwner());

        await _unitOfWork.Events.DeleteAsync(@event, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
