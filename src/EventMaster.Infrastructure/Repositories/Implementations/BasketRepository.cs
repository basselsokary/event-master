using System.Linq.Expressions;
using EventMaster.Application.Common.Interfaces.Repositories;
using EventMaster.Domain.Entities;
using EventMaster.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace EventMaster.Infrastructure.Repositories.Implementations;

internal class BasketRepository(AppDbContext context)
    : EntityBaseRepository<Basket>(context),
    IBasketRepository
{
    public async Task<bool> IsEventSavedAsync(
        Guid eventId,
        string participantId,
        CancellationToken cancellationToken = default)
    {
        return await DbContext.Baskets
            .Where(b => b.ParticipantId == participantId)
            .SelectMany(b => b.SavedEventItems)
            .AnyAsync(se => se.EventId == eventId, cancellationToken);
    }

    public async Task<List<TResult>> GetSavedEvents<TResult>(
        string participantId,
        Expression<Func<Event, TResult>> selector,
        CancellationToken cancellationToken = default)
    {
        var savedEventIds = await DbContext.Baskets
            .Where(b => b.ParticipantId == participantId)
            .SelectMany(b => b.SavedEventItems.Select(se => se.EventId))
            .ToListAsync(cancellationToken);

        if (savedEventIds.Count == 0)
            return [];

        return await DbContext.Events
            .Where(e => savedEventIds.Contains(e.Id))
            .Select(selector)
            .ToListAsync(cancellationToken);
    }
}
