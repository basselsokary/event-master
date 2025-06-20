using EventMaster.Application.Common.Interfaces.Authentication;
using EventMaster.Domain.Errors;
using EventMaster.Domain.ValueObjects;

namespace EventMaster.Application.EntityRequests.Events.Commands.Update.UpdateByOrganizer;

internal class UpdateEventByOrganizerCommandHandler(
    IUnitOfWork unitOfWork,
    IUserContext userContext) : ICommandHandler<UpdateEventByOrganizerCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserContext userContext = userContext;

    public async Task<Result> Handle(UpdateEventByOrganizerCommand request, CancellationToken cancellationToken)
    {
        var eventEntity = await _unitOfWork.Events.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (eventEntity == null)
            return Result.Failure(EventErrors.NotFound(request.Id));

        if (userContext.Id != eventEntity.OrganizerId)
            return Result.Failure(EventErrors.NotEventOwner());

        var money = Money.Create(request.TicketPrice.Amount, request.TicketPrice.Currency);

        eventEntity.Update(
            request.Title,
            request.Description,
            request.Venue,
            request.Location,
            money,
            request.TotalTickets,
            request.Date);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
