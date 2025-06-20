using EventMaster.Domain.Entities;
using EventMaster.Domain.Enums;

namespace EventMaster.Infrastructure.User;

public class EventOrganizer : AppUser
{
    public EventOrganizer()
    {
        Status = OrganizerStatus.Pending;
    }

    public OrganizerStatus Status { get; private set; }

    public ICollection<Event>? OrganizedEvents { get; private set; }

    public void Approve(bool isApproved = true)
    {
        if (isApproved)
            Status = OrganizerStatus.Approved;
        else
            Status = OrganizerStatus.Rejected;
    }
}

