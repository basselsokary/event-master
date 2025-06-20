namespace EventMaster.Application.Hubs.Notification;

public interface INotificationClient
{
    Task ReceiveEventUpdates(string message);
}