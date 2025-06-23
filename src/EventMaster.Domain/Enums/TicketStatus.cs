namespace EventMaster.Domain.Enums;

public enum TicketStatus : byte
{
    Active = 1,
    Canceled,
    Used,
    Refunded
}
