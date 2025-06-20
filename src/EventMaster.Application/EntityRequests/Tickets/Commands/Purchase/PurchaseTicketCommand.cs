namespace EventMaster.Application.EntityRequests.Tickets.Commands.Purchase;

public record PurchaseTicketCommand(Guid EventId) : ICommand;
