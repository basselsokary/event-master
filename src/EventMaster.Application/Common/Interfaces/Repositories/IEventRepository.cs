using EventMaster.Application.EntityRequests.Events.Queries.Get.GetForOrganizer;
using EventMaster.Domain.Entities;

namespace EventMaster.Application.Common.Interfaces.Repositories;

public interface IEventRepository : IEntityBaseRepository<Event>
{
    Task<bool> IsOrganizerEventOwnerAsync(Guid eventId, string ownerId, CancellationToken cancellationToken = default);
}