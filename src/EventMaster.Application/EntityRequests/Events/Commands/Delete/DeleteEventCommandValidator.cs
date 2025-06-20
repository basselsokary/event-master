using FluentValidation;

namespace EventMaster.Application.EntityRequests.Events.Commands.Delete;

public class DeleteEventCommandValidator : AbstractValidator<DeleteEventCommand>
{
    public DeleteEventCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Event ID must not be empty.");
    }
}