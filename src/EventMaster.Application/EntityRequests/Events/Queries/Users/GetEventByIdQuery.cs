namespace EventMaster.Application.EntityRequests.Events.Queries.GetById;

public record GetEventByIdQuery(Guid Id) : IQuery<Response>;
