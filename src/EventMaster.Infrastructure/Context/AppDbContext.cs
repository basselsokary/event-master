using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EventMaster.Domain.Entities;
using EventMaster.Infrastructure.User;
using EventMaster.Infrastructure.Authentication;
using EventMaster.Domain.Common;
using MediatR;

namespace EventMaster.Infrastructure.Context;

internal class AppDbContext : IdentityDbContext<AppUser>
{
    private readonly IPublisher _publisher;

    public virtual DbSet<Event> Events { get; set; }
    public virtual DbSet<EventAttachment> EventAttachments { get; set; }
    public virtual DbSet<Ticket> Tickets { get; set; }
    public virtual DbSet<SavedEventItem> SavedEventItems { get; set; }
    public virtual DbSet<Basket> Baskets { get; set; }
    public virtual DbSet<Notification> Notifications { get; set; }
    public virtual DbSet<Admin> Admins { get; set; }
    public virtual DbSet<EventOrganizer> EventOrganizers { get; set; }
    public virtual DbSet<Participant> Participants { get; set; }
    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options, IPublisher publisher) : base(options)
    {
        _publisher = publisher;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(builder);
    }

    public override int SaveChanges()
    {
        return SaveChangesAsync().GetAwaiter().GetResult();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestampsAsync();
        var domainEvents = GetDomainEvents(cancellationToken);
        int result = await base.SaveChangesAsync(cancellationToken);
        await PublishDomainEventsAsync(domainEvents, cancellationToken);
        return result;
    }

    private List<BaseEvent> GetDomainEvents(CancellationToken cancellationToken)
    {
        // Get all entities that have domain events
        var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity)
            .ToList();

        // Collect all domain events
        var domainEvents = entitiesWithEvents
            .SelectMany(e => e.DomainEvents)
            .ToList();

        // Clear domain events from entities to prevent them from being dispatched multiple times
        entitiesWithEvents.ForEach(e => e.ClearDomainEvents());

        return domainEvents;
    }

    private async Task PublishDomainEventsAsync(List<BaseEvent> domainEvents, CancellationToken cancellationToken)
    {
        // Dispatch domain events after successful save
        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }
    }

    private void UpdateTimestampsAsync()
    {
        var entries = ChangeTracker.Entries<BaseAuditableEntity>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.LastModified = DateTime.UtcNow;
            }
        }
    }
}