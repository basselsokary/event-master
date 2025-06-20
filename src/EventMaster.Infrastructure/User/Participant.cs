using EventMaster.Domain.Entities;

namespace EventMaster.Infrastructure.User;

public class Participant : AppUser
{
    public Basket Basket { get; private set; } = null!;
    public ICollection<Ticket> Tickets { get; private set; } = null!;
}
