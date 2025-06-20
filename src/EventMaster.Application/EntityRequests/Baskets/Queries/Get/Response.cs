using EventMaster.Application.EntityRequests.Common.Money;

namespace EventMaster.Application.EntityRequests.Baskets.Queries.Get;

public record Response(
    Guid EventId,
    string Title,
    string Description,
    string Location,
    string Venue,
    DateTime Date,
    MoneyDto TicketPrice,
    int TotalTickets,
    int TicketsLeft);
