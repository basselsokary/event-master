namespace EventMaster.Application.Common.Interfaces.Services;

public interface INotificationService
{
    Task NotifyEventUpdate(
        Guid eventId,
        string eventTitle,
        string message,
        object? data = null,
        CancellationToken cancellationToken = default);

    Task NotifyEventCancelled(
        Guid eventId,
        string eventTitle,
        string reason = "",
        CancellationToken cancellationToken = default);

    Task NotifyNewParticipant(
        Guid eventId,
        string eventTitle,
        string participantName,
        CancellationToken cancellationToken = default);

    Task NotifyEventReminder(
        Guid eventId,
        string eventTitle,
        string reminderMessage,
        CancellationToken cancellationToken = default);
}
