using System.Linq.Expressions;
using EventMaster.Application.Common.Interfaces.Authentication;
using EventMaster.Domain.Entities;
using EventMaster.Domain.Errors;

namespace EventMaster.Application.EntityRequests.Tickets.Queries.GetById;

internal class GetTicketByIdQueryHandler(
    IUnitOfWork unitOfWork,
    IUserContext userContext) : IQueryHandler<GetTicketByIdQuery, Response>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserContext _userContext = userContext;

    public async Task<Result<Response>> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
    {
        var ticketResponse = await _unitOfWork.Tickets.GetProjectedAsync(
            filter: t => t.Id == request.Id,
            selector: GetProjection(),
            cancellationToken: cancellationToken);

        if (ticketResponse == null)
            return Result.Failure<Response>(TicketErrors.NotFound());

        if (_userContext.Id != ticketResponse.ParticipantId)
            return Result.Failure<Response>(TicketErrors.Unauthorized());

        return Result.Success(ticketResponse);
    }

    private static Expression<Func<Ticket, Response?>> GetProjection()
    {
        return ticket => new Response(
            ticket.Id,
            ticket.EventId,
            ticket.ParticipantId,
            new(ticket.Price.Amount, ticket.Price.Currency),
            ticket.Status,
            ticket.CreatedAt);
    }
}
