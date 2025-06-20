namespace EventMaster.Application.EntityRequests.Baskets.Commands.SaveEvent;

public record SaveEventCommand(Guid EventId) : ICommand;
