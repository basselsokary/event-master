namespace EventMaster.Application.Hubs.Notification;

public interface INotificationClient
{
    Task EventUpdated(string message);
    Task JoinedEventGroup(string message);
    Task LeftEventGroup(string message);
    Task Connected(string message);
}