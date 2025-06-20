using FluentValidation;

namespace EventMaster.Application.EntityRequests.Tickets.Queries.GetById;

public class GetTicketByIdQueryValidator : AbstractValidator<GetTicketByIdQuery>
{
    public GetTicketByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Ticket ID is required.");
    }
}