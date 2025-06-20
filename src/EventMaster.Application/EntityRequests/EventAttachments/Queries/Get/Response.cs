namespace EventMaster.Application.EntityRequests.EventAttachments.Queries.Get;

public record Response(
    Guid Id,
    string? Text,
    string FileUrl,
    DateTime UploadedAt);