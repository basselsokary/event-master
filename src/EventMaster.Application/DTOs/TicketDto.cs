using EventMaster.Domain.Enums;

namespace EventMaster.Application.DTOs;

public class TicketDto
{
    public int Id { get; set; }

    public int EventId { get; set; }

    public string ParticipantId { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public TicketStatus Status { get; set; }

    public DateTime PurchaseDate { get; set; }
}
