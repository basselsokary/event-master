namespace EventMaster.Domain.Errors;

public class TicketErrors
{
    public static IEnumerable<string> AlreadyHasTicketForThisEvent() =>
    [
        "Already Has Ticket For This Event",
        "The event you are tying to register to, you're already have ticket for it."
    ];

    public static IEnumerable<string> EventNotFound() =>
    [
        "Event Was Not Found",
        "The event you are tying to register to was not found."
    ];

    public static IEnumerable<string> NotFound() =>
    [
        "Ticket Was Not Found"
    ];

    public static IEnumerable<string> NoTicketsLeft() =>
    [
        "No Tickets Left",
        "There is no tickets left to register for this event."
    ];

    public static IEnumerable<string> Unauthorized() =>
    [
        "Unauthorized User"
    ];
}
