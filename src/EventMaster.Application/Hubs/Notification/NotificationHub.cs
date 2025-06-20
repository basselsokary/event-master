using Microsoft.AspNetCore.SignalR;

namespace EventMaster.Application.Hubs.Notification;

public class NotificationHub : Hub<INotificationClient>
{
    public override Task OnConnectedAsync()
    {
        Console.WriteLine($"User connected: {Context.UserIdentifier}");
        Console.WriteLine($"Connection Id: {Context.ConnectionId}");
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        Console.WriteLine(exception?.Message);
        return base.OnDisconnectedAsync(exception);
    }
    
    //public async Task SendNotification(string userId, string message)
    //{
    //    await Clients.All.ReceiveEventUpdates($"{Context.ConnectionId}: {message}");
    //}
}
