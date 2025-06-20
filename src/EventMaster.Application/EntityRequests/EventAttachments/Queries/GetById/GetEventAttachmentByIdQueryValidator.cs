using FluentValidation;

namespace EventMaster.Application.EntityRequests.EventAttachments.Queries.GetById;

public class GetEventAttachmentByIdQueryValidator : AbstractValidator<GetEventAttachmentByIdQuery>
{
    public GetEventAttachmentByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Attachment ID cannot be empty.");

        RuleFor(x => x.EventId)
            .NotEmpty()
            .WithMessage("Event ID cannot be empty.");
    }
}