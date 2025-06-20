using EventMaster.Application.EntityRequests.Common.Money;
using EventMaster.Domain.Enums;

namespace EventMaster.Application.EntityRequests.Tickets.Queries.GetById;

public record Response(
    Guid Id,
    Guid EventId,
    string ParticipantId,
    MoneyDto Price,
    TicketStatus Status,
    DateTime PurchaseDate
);