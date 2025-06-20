using EventMaster.Application.Common.Interfaces.Authentication;
using EventMaster.Domain.Errors;

namespace EventMaster.Application.EntityRequests.EventAttachments.Commands.Delete;

internal class DeleteEventAttachmentCommandHandler(
    IUnitOfWork unitOfWork,
    IUserContext userContext) : ICommandHandler<DeleteEventAttachmentCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserContext _userContext = userContext;

    public async Task<Result> Handle(DeleteEventAttachmentCommand request, CancellationToken cancellationToken)
    {
        var @event = await _unitOfWork.Events.GetAsync(
            filter: e => e.Id == request.EventId,
            asSplitQuery: true,
            includeProperties: e => e.EventAttachments,
            cancellationToken: cancellationToken);

        if (@event == null)
            return Result.Failure(EventErrors.NotFound(request.EventId));

        if (@event.OrganizerId != _userContext.Id)
            return Result.Failure(EventErrors.NotEventOwner());
        
        var result = @event.DeleteAttachment(request.Id);
        if (!result.Succeeded)
            return result;
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
