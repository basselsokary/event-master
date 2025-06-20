namespace EventMaster.Application.Common.Interfaces.Services;

public interface INotificationService
{
    Task SendEventAttachmentsUpdateAsync(Guid eventId, CancellationToken cancellationToken = default);
}
