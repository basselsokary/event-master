namespace EventMaster.Application.EntityRequests.EventAttachments.Commands.Delete;

public record DeleteEventAttachmentCommand(Guid Id, Guid EventId) : ICommand;
