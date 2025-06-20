using System.Linq.Expressions;
using EventMaster.Application.DTOs;
using EventMaster.Domain.Entities;

namespace EventMaster.Application.Common.Interfaces.Repositories;

public interface IBasketRepository : IEntityBaseRepository<Basket>
{
    Task<bool> IsEventSavedAsync(Guid eventId, string participantId, CancellationToken cancellationToken = default);
    Task<List<TResult>> GetSavedEvents<TResult>(
        string participantId,
        Expression<Func<Event, TResult>> selector,
        CancellationToken cancellationToken = default);
}
