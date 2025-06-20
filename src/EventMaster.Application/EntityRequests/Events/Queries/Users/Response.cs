using EventMaster.Application.EntityRequests.Common.Money;
using EventMaster.Domain.Enums;

namespace EventMaster.Application.EntityRequests.Events.Queries.GetById;

public record Response(
    Guid Id,
    string Title,
    string Description,
    string Venue,
    string Location,
    DateTime Date,
    MoneyDto TicketPrice,
    int TotalTickets,
    int TicketsLeft,
    EventStatus Status
);