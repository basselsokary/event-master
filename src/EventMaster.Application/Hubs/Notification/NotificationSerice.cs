using EventMaster.Application.Common.Interfaces.Repositories;
using EventMaster.Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;

namespace EventMaster.Application.Hubs.Notification;

internal class NotificationSerice(
    IHubContext<NotificationHub, INotificationClient> hubContext,
    IUnitOfWork unitOfWork) : INotificationService
{
    private readonly IHubContext<NotificationHub, INotificationClient> _hubContext = hubContext;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task SendEventAttachmentsUpdateAsync(Guid eventId, CancellationToken cancellationToken = default)
    {
        var participantIds = await _unitOfWork.Tickets.GetAllProjectedAsync(
            filter: t => t.EventId == eventId,
            selector: t => new { ParticipantIds = t.ParticipantId },
            cancellationToken: cancellationToken
        );
        if (participantIds == null)
            return;
            
        // foreach (var id in participantIds)
        // {
        //     Console.WriteLine($"Participant Id: {id}");
        //     await _hubContext.Clients.User(id).ReceiveEventUpdates($"Attachment has been updated to event {eventEntity.Title}");
        // }

        Console.WriteLine("Send to all clients");
        await _hubContext.Clients.All.ReceiveEventUpdates($"Attachment has been updated to event {eventId}");
        // await _hubContext.Clients.Users([.. participantIds]).ReceiveEventUpdates($"Attachment has been {str} to event {eventEntity.Title}");
    }
}
