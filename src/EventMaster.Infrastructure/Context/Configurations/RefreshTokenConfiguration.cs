using EventMaster.Infrastructure.Authentication;
using EventMaster.Infrastructure.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static EventMaster.Domain.Constants.DomainConstants.RefreshToken;

namespace EventMaster.Infrastructure.Context.Configurations;

internal class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(rt => rt.Id);

        builder.Property(rt => rt.Token)
            .IsRequired()
            .HasMaxLength(MaxRefreshTokenLength);

        builder.Property(rt => rt.ExpiresOn)
            .IsRequired();

        builder.Property(rt => rt.IsRevoked)
            .IsRequired();

        // Foreign key to ApplicationUser
        builder.HasOne<AppUser>()
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(rt => rt.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        // Shadow property IsActive is not mapped
        builder.Ignore(rt => rt.IsActive);
    }
}
