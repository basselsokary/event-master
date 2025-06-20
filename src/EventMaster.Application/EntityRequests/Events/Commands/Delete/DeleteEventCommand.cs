namespace EventMaster.Application.EntityRequests.Events.Commands.Delete;

public record DeleteEventCommand(Guid Id) : ICommand;
