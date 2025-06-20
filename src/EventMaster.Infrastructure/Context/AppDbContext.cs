using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EventMaster.Domain.Entities;
using EventMaster.Infrastructure.User;
using EventMaster.Infrastructure.Authentication;

namespace EventMaster.Infrastructure.Context;

internal class AppDbContext : IdentityDbContext<AppUser>
{
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

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(builder);
    }
}