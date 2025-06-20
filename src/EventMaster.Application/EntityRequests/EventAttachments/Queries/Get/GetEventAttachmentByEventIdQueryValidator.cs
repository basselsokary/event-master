using FluentValidation;

namespace EventMaster.Application.EntityRequests.EventAttachments.Queries.Get;

public class GetEventAttachmentsByEventIdQueryValidator : AbstractValidator<GetEventAttachmentsByEventIdQuery>
{
    public GetEventAttachmentsByEventIdQueryValidator()
    {
        RuleFor(x => x.EventId)
            .NotEmpty()
            .WithMessage("Event ID must not be empty.");
    }
}