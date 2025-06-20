using EventMaster.Application.Common.Interfaces.Services;
using EventMaster.Domain.Events;

namespace EventMaster.Application.EntityRequests.EventAttachments.EventHandlers;

public class EventAttachmentAddedEventHandler : INotificationHandler<EventAttachmentAddedEvent>
{
    private readonly INotificationService _notificationService;

    public EventAttachmentAddedEventHandler(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public async Task Handle(EventAttachmentAddedEvent notification, CancellationToken cancellationToken)
    {
        await _notificationService.SendEventAttachmentsUpdateAsync(notification.Event.Id, cancellationToken);
    }
}
