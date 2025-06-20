using System.Linq.Expressions;
using EventMaster.Application.DTOs;
using EventMaster.Domain.Entities;

namespace EventMaster.Application.Mappers;

public static class EventAttachmentMapper
{
    public static EventAttachmentDto Map(this EventAttachment eventAttachment)
    {
        return new EventAttachmentDto()
        {
            Id = eventAttachment.Id,
            Text = eventAttachment.Text,
            FileUrl = eventAttachment.FileUrl,
            UploadedAt = eventAttachment.UploadedAt
        };
    }

    public static Expression<Func<EventAttachment, EventAttachmentDto>> Project()
    {
        return e => new EventAttachmentDto()
        {
            Id = e.Id,
            Text = e.Text,
            FileUrl = e.FileUrl,
            UploadedAt = e.UploadedAt
        };
    }
}