using EventMaster.Application.EntityRequests.Common.Money;

namespace EventMaster.Application.EntityRequests.Events.Queries.Get.GetForAdmins;

public record Response(
    Guid Id,
    string Title,
    string Description,
    string Venue,
    string Location,
    DateTime Date,
    MoneyDto TicketPrice,
    int TotalTickets,
    int TicketsLeft
);