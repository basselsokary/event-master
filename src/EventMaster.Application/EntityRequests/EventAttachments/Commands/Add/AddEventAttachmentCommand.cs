namespace EventMaster.Application.EntityRequests.EventAttachments.Commands.Add;

public record AddEventAttachmentCommand(
    Guid EventId,
    string? Text,
    string FileUrl) : ICommand;
