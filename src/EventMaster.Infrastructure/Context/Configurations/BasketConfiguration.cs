using EventMaster.Domain.Entities;
using EventMaster.Infrastructure.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventMaster.Infrastructure.Context.Configurations;

internal class BasketConfiguration : IEntityTypeConfiguration<Basket>
{
    public void Configure(EntityTypeBuilder<Basket> builder)
    {
        builder.HasKey(b => b.Id);

        builder.HasOne<Participant>()
            .WithOne(p => p.Basket)
            .HasForeignKey<Basket>(b => b.ParticipantId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
