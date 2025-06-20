using EventMaster.Application.Common.Interfaces.Authentication;
using EventMaster.Domain.Entities;
using EventMaster.Domain.Errors;
using EventMaster.Domain.ValueObjects;

namespace EventMaster.Application.EntityRequests.Tickets.Commands.Purchase;

internal class PurchaseTicketCommandHandler(
    IUnitOfWork unitOfWork,
    IUserContext userContext) : ICommandHandler<PurchaseTicketCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserContext _userContext = userContext;

    public async Task<Result> Handle(PurchaseTicketCommand request, CancellationToken cancellationToken)
    {
        var @event = await _unitOfWork.Events.GetAsync(
            filter: e => e.Id == request.EventId,
            cancellationToken: cancellationToken);
        if (@event == null)
            return Result.Failure(TicketErrors.EventNotFound());

        if (@event.TicketsLeft < 1)
            return Result.Failure(TicketErrors.NoTicketsLeft());
        
        var hasTicket = await _unitOfWork.Tickets.AnyAsync(
            t => t.ParticipantId == _userContext.Id && t.EventId == @event.Id,
            cancellationToken);

        if (hasTicket)
            return Result.Failure(TicketErrors.AlreadyHasTicketForThisEvent());

        var money = Money.Create(0); // For now

        var ticket = Ticket.Create(_userContext.Id, @event.Id, money);

        @event.PurshaseTicket(ticket);
        
        await _unitOfWork.Tickets.AddAsync(ticket);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
