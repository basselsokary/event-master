using EventMaster.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static EventMaster.Domain.Constants.DomainConstants.EventAttachment;

namespace EventMaster.Infrastructure.Context.Configurations;

internal class EventAttachmentConfiguration : IEntityTypeConfiguration<EventAttachment>
{
    public void Configure(EntityTypeBuilder<EventAttachment> builder)
    {
        builder.Property(ea => ea.Text)
            .HasMaxLength(MaxTextLength)
            .IsRequired(false);

        builder.Property(ea => ea.FileUrl)
            .HasMaxLength(MaxFileUrlLength)
            .IsRequired();

        builder.Property(ea => ea.UploadedAt)
            .IsRequired();

        builder.HasOne<Event>()
            .WithMany(e => e.EventAttachments)
            .HasForeignKey(ea => ea.EventId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
