using EventMaster.Domain.Entities;
using EventMaster.Infrastructure.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static EventMaster.Domain.Constants.DomainConstants.Notification;

namespace EventMaster.Infrastructure.Context.Configurations;

internal class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.HasKey(n => n.Id);

        builder.Property(n => n.Title)
            .HasMaxLength(MaxTitleLength)
            .IsRequired();
        
        builder.Property(n => n.Message)
            .HasMaxLength(MaxMessageLength)
            .IsRequired();

        builder.Property(n => n.SendBy)
            .HasMaxLength(MaxIdLength)
            .IsRequired();

        builder.Property(n => n.IsRead)
            .IsRequired();
        builder.Property(n => n.SendAt)
            .IsRequired();

        builder.HasOne<AppUser>()
            .WithMany(user => user.Notifications)
            .HasForeignKey(n => n.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
