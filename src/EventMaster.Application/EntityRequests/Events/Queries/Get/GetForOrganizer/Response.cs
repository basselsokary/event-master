using EventMaster.Application.EntityRequests.Common.Money;
using EventMaster.Domain.Enums;

namespace EventMaster.Application.EntityRequests.Events.Queries.Get.GetForOrganizer;

public record Response(
    Guid Id,
    string Title,
    string Description,
    string Venue,
    string Location,
    MoneyDto TicketPrice,
    int TotalTickets,
    int TicketsLeft,
    DateTime Date,
    EventStatus Status);
