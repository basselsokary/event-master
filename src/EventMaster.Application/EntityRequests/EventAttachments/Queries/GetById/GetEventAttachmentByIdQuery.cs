namespace EventMaster.Application.EntityRequests.EventAttachments.Queries.GetById;

public record GetEventAttachmentByIdQuery(Guid Id, Guid EventId) : IQuery<Response>;