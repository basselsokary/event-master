using EventMaster.Application.EntityRequests.Common.Money;

namespace EventMaster.Application.EntityRequests.Events.Commands.Update.UpdateByOrganizer;

public record UpdateEventByOrganizerCommand(
    Guid Id,
    string Title,
    string Description,
    string Venue,
    string Location,
    MoneyDto TicketPrice,
    int TotalTickets,
    DateTime Date) : ICommand;
