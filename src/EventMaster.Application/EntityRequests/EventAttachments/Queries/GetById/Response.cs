namespace EventMaster.Application.EntityRequests.EventAttachments.Queries.GetById;

public record Response(
    Guid Id,
    Guid EventId,
    string? Text,
    string FileUrl,
    DateTime UploadedAt
);
