namespace EventMaster.Application.EntityRequests.Baskets.Commands.DeleteSavedEvent;

public record RemoveSavedEventCommand(Guid EventId) : ICommand;
