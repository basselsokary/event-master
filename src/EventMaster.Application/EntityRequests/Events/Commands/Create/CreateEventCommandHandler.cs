using EventMaster.Application.Common.Interfaces.Authentication;
using EventMaster.Domain.Entities;
using EventMaster.Domain.ValueObjects;

namespace EventMaster.Application.EntityRequests.Events.Commands.Create;

public class CreateEventCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext)
    : ICommandHandler<CreateEventCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserContext _userContext = userContext;

    public async Task<Result> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var money = Money.Create(request.TicketPrice.Amount, request.TicketPrice.Currency);

        var eventEntity = Event.Create(
            _userContext.Id ,
            request.Title,
            request.Description,
            request.Venue,
            request.Location,
            money,
            request.TotalTickets,
            request.Date);

        await _unitOfWork.Events.AddAsync(eventEntity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}