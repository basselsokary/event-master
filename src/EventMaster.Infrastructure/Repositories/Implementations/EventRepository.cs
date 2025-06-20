using EventMaster.Application.Common.Interfaces.Repositories;
using EventMaster.Domain.Entities;
using EventMaster.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace EventMaster.Infrastructure.Repositories.Implementations;

internal class EventRepository(AppDbContext context)
    : EntityBaseRepository<Event>(context),
    IEventRepository
{
    public async Task<bool> IsOrganizerEventOwnerAsync(
        Guid eventId,
        string ownerId,
        CancellationToken cancellationToken = default)
    {
        var organizerId = await DbContext.Events
            .Where(e => e.Id == eventId && e.OrganizerId == ownerId)
            .Select(e => e.OrganizerId)
            .FirstOrDefaultAsync(cancellationToken);

        if (organizerId == null)
            return false;

        return organizerId == ownerId;
    }

}
