namespace EventMaster.Application.DTOs;

public class EventAttachmentDto
{
    public Guid Id { get; set; }
    public Guid? EventId { get; set; }

    public string? Text { get; set; }

    public string FileUrl { get; set; } = null!;

    public DateTime UploadedAt { get; set; }
}
