using EventMaster.Application.Common.Interfaces.Services;
using EventMaster.Domain.Events;

namespace EventMaster.Application.EntityRequests.Events.EventHandlers;

public class EventUpdatedEventHandler(INotificationService notificationService)
    : INotificationHandler<EventUpdatedEvent>
{
    private readonly INotificationService _notificationService = notificationService;

    public Task Handle(EventUpdatedEvent notification, CancellationToken cancellationToken)
    {
        // Notify all subscribers about the event update
        return _notificationService.NotifyEventUpdate(
            notification.Event.Id,
            notification.Event.Title,
            $"Event '{notification.Event.Title}' has been updated.",
            notification.Event,
            cancellationToken);
    }
}
