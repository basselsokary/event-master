using EventMaster.Domain.ValueObjects;

namespace EventMaster.Application.DTOs;

public class EventDto
{
    public Guid Id { get; set; }
    public string OrganizerId { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Venue { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;

    public Money TicketPrice { get; set; } = null!;

    public int TotalTickets { get; set; }
    public int TicketsLeft { get; set; }

    public DateTime Date { get; set; }
}
