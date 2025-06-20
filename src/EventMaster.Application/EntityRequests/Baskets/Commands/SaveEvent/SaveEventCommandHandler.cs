using EventMaster.Application.Common.Interfaces.Authentication;
using EventMaster.Domain.Entities;
using EventMaster.Domain.Errors;

namespace EventMaster.Application.EntityRequests.Baskets.Commands.SaveEvent;

internal class SaveEventCommandHandler(
    IUnitOfWork unitOfWork,
    IUserContext userContext) : ICommandHandler<SaveEventCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserContext _userContext = userContext;

    public async Task<Result> Handle(SaveEventCommand request, CancellationToken cancellationToken)
    {
        if (!await _unitOfWork.Events.AnyAsync(e => e.Id == request.EventId, cancellationToken))
            return Result.Failure(EventErrors.NotFound(request.EventId));

        var basket = await _unitOfWork.Baskets.GetAsync(
            filter: b => b.ParticipantId == _userContext.Id,
            asSplitQuery: true,
            includeProperties: b => b.SavedEventItems,
            cancellationToken: cancellationToken);

        if (basket == null) // If basket does not exist, create a new one
        {
            basket = Basket.Create(_userContext.Id);
            await _unitOfWork.Baskets.AddAsync(basket, cancellationToken);
        }

        var savedEvent = SavedEventItem.Create(request.EventId);
        
        var result = basket.AddItem(savedEvent);
        if (!result.Succeeded)
            return result;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
