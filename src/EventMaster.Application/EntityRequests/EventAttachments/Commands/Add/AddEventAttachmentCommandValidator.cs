using FluentValidation;
using static EventMaster.Domain.Constants.DomainConstants.EventAttachment;

namespace EventMaster.Application.EntityRequests.EventAttachments.Commands.Add;

public class AddEventAttachmentCommandValidator : AbstractValidator<AddEventAttachmentCommand>
{
    public AddEventAttachmentCommandValidator()
    {
        RuleFor(x => x.EventId)
            .NotEmpty()
            .WithMessage("Event ID is required.");

        RuleFor(x => x.Text)
            .MaximumLength(MaxTextLength)
            .WithMessage($"Event attachment text cannot exceed {MaxTextLength} characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.Text));

        RuleFor(x => x.FileUrl)
            .NotEmpty()
            .WithMessage("File URL is required.")
            .MaximumLength(MaxFileUrlLength)
            .WithMessage($"Question text cannot exceed {MaxFileUrlLength} characters.");
    }
}