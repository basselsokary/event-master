using EventMaster.Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;

namespace EventMaster.Application.Hubs.Notification;

internal class NotificationService : INotificationService
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationService(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task NotifyEventUpdate(
        Guid eventId,
        string eventTitle,
        string message,
        object? data = null,
        CancellationToken cancellationToken = default)
    {
        var notification = new EventNotification
        {
            EventId = eventId,
            EventTitle = eventTitle,
            Message = message,
            NotificationType = "updated",
            Data = data
        };

        Console.WriteLine($"Sending notification for event {eventId}: {message}");

        await _hubContext.Clients.Group($"event_{eventId}")
            .SendAsync("EventUpdated", notification, cancellationToken);
    }

    public async Task NotifyEventCancelled(
        Guid eventId,
        string eventTitle,
        string reason = "",
        CancellationToken cancellationToken = default)
    {
        var notification = new EventNotification
        {
            EventId = eventId,
            EventTitle = eventTitle,
            Message = $"Event '{eventTitle}' has been cancelled. {reason}",
            NotificationType = "cancelled"
        };

        await _hubContext.Clients.Group($"event_{eventId}")
            .SendAsync("EventCancelled", notification, cancellationToken);
    }

    public async Task NotifyNewParticipant(
        Guid eventId,
        string eventTitle,
        string participantName,
        CancellationToken cancellationToken = default)
    {
        var notification = new EventNotification
        {
            EventId = eventId,
            EventTitle = eventTitle,
            Message = $"{participantName} joined the event!",
            NotificationType = "participant_joined",
            Data = new { ParticipantName = participantName }
        };

        await _hubContext.Clients.Group($"event_{eventId}")
            .SendAsync("ParticipantJoined", notification, cancellationToken);
    }

    public async Task NotifyEventReminder(
        Guid eventId,
        string eventTitle,
        string reminderMessage,
        CancellationToken cancellationToken = default)
    {
        var notification = new EventNotification
        {
            EventId = eventId,
            EventTitle = eventTitle,
            Message = reminderMessage,
            NotificationType = "reminder"
        };

        await _hubContext.Clients.Group($"event_{eventId}")
            .SendAsync("EventReminder", notification, cancellationToken);
    }
}
