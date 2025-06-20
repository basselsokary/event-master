namespace EventMaster.Application.EntityRequests.Tickets.Queries.GetById;

public record GetTicketByIdQuery(Guid Id) : IQuery<Response>;
