using EventMaster.Application.Common.Interfaces.Authentication;
using EventMaster.Application.Common.Interfaces.Services;
using EventMaster.Domain.Entities;
using EventMaster.Domain.Errors;

namespace EventMaster.Application.EntityRequests.EventAttachments.Commands.Add;

internal class AddEventAttachmentCommandHandler(
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    INotificationService notificationSerice) : ICommandHandler<AddEventAttachmentCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserContext _userContext = userContext;
    private readonly INotificationService _notificationSerice = notificationSerice;

    public async Task<Result> Handle(AddEventAttachmentCommand request, CancellationToken cancellationToken)
    {
        var @event = await _unitOfWork.Events.GetAsync(
            filter: e => e.Id == request.EventId,
            asSplitQuery: true,
            includeProperties: e => e.EventAttachments,
            cancellationToken: cancellationToken);

        if (@event == null)
            return Result.Failure(EventErrors.NotFound(request.EventId));

        if (@event.EventAttachments.Count >= 10)
            return Result.Failure(EventErrors.TooManyAttachments());

        if (@event.OrganizerId != _userContext.Id)
            return Result.Failure(EventErrors.NotEventOwner());

        var eventAttachment = EventAttachment.Create(request.FileUrl, request.Text, @event.Id);

        var result = @event.AddAttachment(eventAttachment);
        if (!result.Succeeded)
            return result;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
