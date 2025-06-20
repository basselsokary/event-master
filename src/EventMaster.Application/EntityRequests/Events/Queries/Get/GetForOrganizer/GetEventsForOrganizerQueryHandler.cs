using System.Linq.Expressions;
using EventMaster.Application.Common.Interfaces.Authentication;
using EventMaster.Domain.Entities;

namespace EventMaster.Application.EntityRequests.Events.Queries.Get.GetForOrganizer;

internal class GetEventsForOrganizerQueryHandler(
    IUnitOfWork unitOfWork,
    IUserContext userContext) : IQueryHandler<GetEventsForOrganizerQuery, IEnumerable<Response>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserContext _userContext = userContext;

    public async Task<Result<IEnumerable<Response>>> Handle(GetEventsForOrganizerQuery request, CancellationToken cancellationToken)
    {
        var organizedEvents = await _unitOfWork.Events.GetAllProjectedAsync(
            filter: e => e.OrganizerId == _userContext.Id,
            selector: GetProjection(),
            cancellationToken: cancellationToken);

        return Result.Success(organizedEvents);
    }

    private static Expression<Func<Event, Response>> GetProjection()
    {
        return e => new(
            e.Id,
            e.Title,
            e.Description,
            e.Venue,
            e.Location,
            new(e.TicketPrice.Amount, e.TicketPrice.Currency),
            e.TotalTickets,
            e.TicketsLeft,
            e.Date,
            e.Status);
    }
}
