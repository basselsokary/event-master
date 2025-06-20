using FluentValidation;

namespace EventMaster.Application.EntityRequests.EventAttachments.Commands.Delete;

public class DeleteEventAttachmentCommandValidator : AbstractValidator<DeleteEventAttachmentCommand>
{
    public DeleteEventAttachmentCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Event attachment ID is required.");

        RuleFor(x => x.EventId)
            .NotEmpty()
            .WithMessage("Event ID is required.");
    }
}