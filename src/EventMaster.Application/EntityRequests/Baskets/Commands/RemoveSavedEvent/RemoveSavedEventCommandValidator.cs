using FluentValidation;

namespace EventMaster.Application.EntityRequests.Baskets.Commands.DeleteSavedEvent;

public class RemoveSavedEventCommandValidator : AbstractValidator<RemoveSavedEventCommand>
{
    public RemoveSavedEventCommandValidator()
    {
        RuleFor(x => x.EventId)
            .NotEmpty()
            .WithMessage("The event ID is required.");
    }
}
