using System.Linq.Expressions;
using EventMaster.Application.Common.Interfaces.Authentication;
using EventMaster.Domain.Entities;

namespace EventMaster.Application.EntityRequests.Baskets.Queries.Get;

internal class GetSavedEventsQueryHandler(
    IUnitOfWork unitOfWork,
    IUserContext userContex) : IQueryHandler<GetSavedEventsQuery, List<Response>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserContext _userContext = userContex;

    public async Task<Result<List<Response>>> Handle(GetSavedEventsQuery request, CancellationToken cancellationToken)
    {
        var events = await _unitOfWork.Baskets.GetSavedEvents(
            _userContext.Id,
            GetProjection(),
            cancellationToken);

        return Result.Success(events);
    }

    private static Expression<Func<Event, Response>> GetProjection()
    {
        return e => new Response(
            e.Id,
            e.Title,
            e.Description,
            e.Location,
            e.Venue,
            e.Date,
            new(e.TicketPrice.Amount, e.TicketPrice.Currency),
            e.TotalTickets,
            e.TicketsLeft
        );
    }
}
