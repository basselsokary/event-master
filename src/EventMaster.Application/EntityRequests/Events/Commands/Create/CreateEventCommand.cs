using EventMaster.Application.EntityRequests.Common.Money;

namespace EventMaster.Application.EntityRequests.Events.Commands.Create;

public record CreateEventCommand(
    string Title,
    string Description,
    string Venue,
    string Location,
    MoneyDto TicketPrice,
    int TotalTickets,
    DateTime Date) : ICommand;