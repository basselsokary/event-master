using EventMaster.Domain.Errors;

namespace EventMaster.Application.EntityRequests.Events.Commands.Update.UpdateByAdmin;

internal class UpdateEventByAdminCommandHandler(IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateEventByAdminCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(UpdateEventByAdminCommand request, CancellationToken cancellationToken)
    {
        var eventEntity = await _unitOfWork.Events.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (eventEntity == null)
            return Result.Failure(EventErrors.NotFound(request.Id));

        eventEntity.Approve();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
