namespace EventMaster.Application.EntityRequests.EventAttachments.Queries.Get;

public record GetEventAttachmentsByEventIdQuery(Guid EventId) : IQuery<List<Response>>;
