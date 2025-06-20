using EventMaster.Application.Common.Interfaces.Authentication;
using EventMaster.Domain.Errors;

namespace EventMaster.Application.EntityRequests.Baskets.Commands.DeleteSavedEvent;

internal class RemoveSavedEventCommandHandler(
    IUnitOfWork unitOfWork,
    IUserContext userContext) : ICommandHandler<RemoveSavedEventCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserContext _userContext = userContext;

    public async Task<Result> Handle(RemoveSavedEventCommand request, CancellationToken cancellationToken)
    {
        var basket = await _unitOfWork.Baskets.GetAsync(
            filter: b => b.ParticipantId == _userContext.Id,
            asSplitQuery: true,
            includeProperties: b => b.SavedEventItems,
            cancellationToken: cancellationToken);

        if (basket == null)
            return Result.Failure(BasketErrors.NotFound());

        var result = basket.RemoveItem(request.EventId);
        if (!result.Succeeded)
            return result;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
