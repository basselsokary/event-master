using FluentValidation;

namespace EventMaster.Application.EntityRequests.Baskets.Commands.SaveEvent;

public class SaveEventCommandValidator : AbstractValidator<SaveEventCommand>
{
    public SaveEventCommandValidator()
    {
        RuleFor(x => x.EventId)
            .NotEmpty()
            .WithMessage("Event ID is required.");
    }
}