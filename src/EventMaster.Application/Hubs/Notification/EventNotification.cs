namespace EventMaster.Application.Hubs.Notification;

public class EventNotification
{
    public Guid EventId { get; set; }
    public string EventTitle { get; set; } = "";
    public string Message { get; set; } = "";
    public string NotificationType { get; set; } = ""; // "updated", "cancelled", "reminder"
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public object? Data { get; set; }
}
