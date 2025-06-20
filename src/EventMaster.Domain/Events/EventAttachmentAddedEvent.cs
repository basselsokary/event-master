using EventMaster.Domain.Common;
using EventMaster.Domain.Entities;

namespace EventMaster.Domain.Events;

public class EventAttachmentAddedEvent(
    Event @event,
    EventAttachment eventAttachment) : BaseEvent
{
    public EventAttachment EventAttachment { get; } = eventAttachment;
    public Event Event { get; } = @event;
}
