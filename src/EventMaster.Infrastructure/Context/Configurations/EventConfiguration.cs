using EventMaster.Domain.Entities;
using EventMaster.Infrastructure.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static EventMaster.Domain.Constants.DomainConstants.Event;

namespace EventMaster.Infrastructure.Context.Configurations;

internal class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
            .HasMaxLength(MaxTitleLength)
            .IsRequired();

        builder.Property(e => e.Description)
            .HasMaxLength(MaxDescriptionLength)
            .IsRequired();

        builder.Property(e => e.Venue)
            .HasMaxLength(MaxVenueLength)
            .IsRequired();

        builder.Property(e => e.Location)
            .HasMaxLength(MaxLocationLength)
            .IsRequired();

        builder.Property(e => e.Status)
            .IsRequired();
        builder.Property(e => e.TotalTickets)
            .IsRequired();
        builder.Property(e => e.TicketsLeft)
            .IsRequired();
        builder.Property(e => e.Date)
            .IsRequired();
        
        builder.HasIndex(e => e.Venue);
        builder.HasIndex(e => e.Title);
        
        builder.OwnsOne(
            t => t.TicketPrice,
            b => MoneyConfiguration.Configure(b));

        builder.HasOne<EventOrganizer>()
            .WithMany(user => user.OrganizedEvents)
            .HasForeignKey(e => e.OrganizerId);
    }
}
