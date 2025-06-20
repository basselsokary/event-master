using System.Linq.Expressions;
using EventMaster.Domain.Entities;
using EventMaster.Domain.Enums;
using EventMaster.Domain.ValueObjects;

namespace EventMaster.Application.EntityRequests.Events.Queries.Get.GetForAdmins;

internal class GetEventsForAdminQueryHandler(IUnitOfWork unitOfWork)
    : IQueryHandler<GetEventsForAdminQuery, List<Response>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<List<Response>>> Handle(GetEventsForAdminQuery request, CancellationToken cancellationToken)
    {
        var unApprovedEvents = await _unitOfWork.Events.GetAllProjectedAsync(
            filter: e => e.Status == EventStatus.Pending,
            selector: GetProjection(),
            cancellationToken: cancellationToken);

        return Result.Success(unApprovedEvents.ToList());
    }

    private static Expression<Func<Event, Response>> GetProjection()
    {
        return e => new Response(
                e.Id,
                e.Title,
                e.Description,
                e.Venue,
                e.Location,
                e.Date,
                new(e.TicketPrice.Amount, e.TicketPrice.Currency),
                e.TotalTickets,
                e.TicketsLeft
            );
    }
}
