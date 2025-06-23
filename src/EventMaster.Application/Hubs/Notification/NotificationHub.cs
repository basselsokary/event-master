using Microsoft.AspNetCore.SignalR;

namespace EventMaster.Application.Hubs.Notification;

public class NotificationHub : Hub
{
    public async Task JoinEventGroup(Guid eventId)
    {
        Console.WriteLine($"Client {Context.ConnectionId} joining event group: event_{eventId}");
        await Groups.AddToGroupAsync(Context.ConnectionId, $"event_{eventId}");
        await Clients.Caller.SendAsync("JoinedEventGroup", eventId);
    }

    public async Task LeaveEventGroup(Guid eventId)
    {
        Console.WriteLine($"Client {Context.ConnectionId} leaving event group: event_{eventId}");
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"event_{eventId}");
        await Clients.Caller.SendAsync("LeftEventGroup", eventId);
    }

    public override async Task OnConnectedAsync()
    {
        if (Context.ConnectionId == null)
        {
            Console.WriteLine("Connection ID is null. Cannot join event group.");
            return;
        }
        
        Console.WriteLine($"Client connected: {Context.ConnectionId}");

        await Clients.Caller.SendAsync("Connected", Context.ConnectionId);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        Console.WriteLine($"Client disconnected: {Context.ConnectionId}. Exception: {exception?.Message}");
        await base.OnDisconnectedAsync(exception);
    }
}
