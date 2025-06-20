using EventMaster.Infrastructure.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static EventMaster.Domain.Constants.DomainConstants.User;

namespace EventMaster.Infrastructure.Context.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.HasKey(user => user.Id);

        builder.Property(user => user.FullName).HasMaxLength(MaxFullNameLength);
        builder.Property(user => user.Email).HasMaxLength(MaxEmailLength);
        builder.Property(user => user.PasswordHash).HasMaxLength(MaxPasswordHashLength);
        
        builder.Property(user => user.CreatedAt).IsRequired();

        builder.HasIndex(user => user.Email).IsUnique();
    }
}
