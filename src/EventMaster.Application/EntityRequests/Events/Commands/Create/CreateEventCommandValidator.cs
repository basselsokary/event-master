using EventMaster.Application.EntityRequests.Common.Money;
using FluentValidation;
using static EventMaster.Domain.Constants.DomainConstants.Event;

namespace EventMaster.Application.EntityRequests.Events.Commands.Create;

public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
{
    public CreateEventCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required.")
            .MaximumLength(MaxTitleLength)
            .WithMessage($"Title must not exceed {MaxTitleLength} characters.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MaximumLength(MaxDescriptionLength)
            .WithMessage("Description must not exceed 500 characters.");

        RuleFor(x => x.Venue)
            .NotEmpty()
            .WithMessage("Venue is required.")
            .MaximumLength(MaxVenueLength)
            .WithMessage("Venue must not exceed 200 characters.");

        RuleFor(x => x.Location)
            .NotEmpty()
            .WithMessage("Location is required.")
            .MaximumLength(MaxLocationLength)
            .WithMessage("Location must not exceed 200 characters.");

        RuleFor(x => x.TicketPrice)
            .SetValidator(new MoneyDtoValidator());

        RuleFor(x => x.TotalTickets)
            .GreaterThan(0)
            .WithMessage("Total tickets must be greater than zero.");

        RuleFor(x => x.Date)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Event date must be in the future.");
    }
}