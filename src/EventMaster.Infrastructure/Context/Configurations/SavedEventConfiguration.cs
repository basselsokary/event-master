using EventMaster.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventMaster.Infrastructure.Context.Configurations;

internal class SavedEventConfiguration : IEntityTypeConfiguration<SavedEventItem>
{
    public void Configure(EntityTypeBuilder<SavedEventItem> builder)
    {
        builder.HasKey(se => new { se.EventId, se.BasketId });

        builder.HasOne<Basket>()
            .WithMany(b => b.SavedEventItems)
            .HasForeignKey(se => se.BasketId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Event>()
            .WithMany()
            .HasForeignKey(se => se.EventId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
