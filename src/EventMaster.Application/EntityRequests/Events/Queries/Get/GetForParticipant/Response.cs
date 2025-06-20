using EventMaster.Application.EntityRequests.Common.Money;

namespace EventMaster.Application.EntityRequests.Events.Queries.Get.GetForParticipant;

public record Response(
    Guid Id,
    string Title,
    string Description,
    string Venue,
    string Location,
    MoneyDto TicketPrice,
    int TotalTickets,
    int TicketsLeft,
    DateTime Date);