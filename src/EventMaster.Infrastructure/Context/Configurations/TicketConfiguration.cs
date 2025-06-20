using EventMaster.Domain.Entities;
using EventMaster.Infrastructure.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventMaster.Infrastructure.Context.Configurations;

internal class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Status).IsRequired();

        builder.OwnsOne(
            t => t.Price,
            b => MoneyConfiguration.Configure(b));

        builder.HasOne<Participant>()
            .WithMany(p => p.Tickets)
            .HasForeignKey(t => t.ParticipantId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Event>()
            .WithMany()
            .HasForeignKey(t => t.EventId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);           
    }
}
