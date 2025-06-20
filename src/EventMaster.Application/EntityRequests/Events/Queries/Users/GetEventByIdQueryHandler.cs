using System.Linq.Expressions;
using EventMaster.Domain.Entities;
using EventMaster.Domain.Enums;
using EventMaster.Domain.Errors;

namespace EventMaster.Application.EntityRequests.Events.Queries.GetById;

internal class GetEventByIdQueryHandler(IUnitOfWork unitOfWork)
    : IQueryHandler<GetEventByIdQuery, Response>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Response>> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.Events.GetProjectedAsync(
            filter: e => e.Id == request.Id,
            selector: GetProjection(),
            cancellationToken: cancellationToken);

        if (response == null || response.Status != EventStatus.Approved)
            return Result.Failure<Response>(EventErrors.NotFound(request.Id));

        return Result.Success(response);
    }

    private Expression<Func<Event, Response>> GetProjection()
    {
        return e => new(
            e.Id,
            e.Title,
            e.Description,
            e.Venue,
            e.Location,
            e.Date,
            new(e.TicketPrice.Amount, e.TicketPrice.Currency),
            e.TotalTickets,
            e.TicketsLeft,
            e.Status
        );
    }
}
