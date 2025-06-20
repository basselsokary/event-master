using EventMaster.Application.Helpers;
using EventMaster.Domain.Entities;
using System.Linq.Expressions;
using EventMaster.Domain.Enums;

namespace EventMaster.Application.EntityRequests.Events.Queries.Get.GetForParticipant;

internal class GetEventsForParticipantQueryHandler(IUnitOfWork unitOfWork)
    : IQueryHandler<GetEventsForParticipantQuery, List<Response>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<List<Response>>> Handle(GetEventsForParticipantQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Event, bool>> filter = e => e.Status == EventStatus.Approved; // Only approved events are returned
        if (request.Date.HasValue)
            filter = filter.AndAlso(e => e.Date.Date == request.Date.Value.Date);

        if (!string.IsNullOrWhiteSpace(request.Location))
            filter = filter.AndAlso(e => e.Location.ToLower().Contains(request.Location.ToLower()));

        Func<IQueryable<Event>, IOrderedQueryable<Event>>? orderByFunc = null;
        if (request.OrderBy?.ToLower() == nameof(Event.Date).ToLower())
        {
            orderByFunc = q => q.OrderBy(e => e.Date.ToString("yyyy-MM-dd"));
        }
        else if (request.OrderBy?.ToLower() == nameof(Event.Location).ToLower())
        {
            orderByFunc = q => q.OrderBy(e => e.TicketPrice.Amount.ToString());
        }

        var events = await _unitOfWork.Events.GetAllProjectedAsync(
            filter: filter,
            selector: GetProjection(),
            orderBy: orderByFunc,
            cancellationToken: cancellationToken);

        return Result.Success(events.ToList());
    }

    private static Expression<Func<Event, Response>> GetProjection()
    {
        return e => new Response(
            e.Id,
            e.Title,
            e.Description,
            e.Venue,
            e.Location,
            new(e.TicketPrice.Amount, e.TicketPrice.Currency),
            e.TotalTickets,
            e.TicketsLeft,
            e.Date
        );
    }
}
