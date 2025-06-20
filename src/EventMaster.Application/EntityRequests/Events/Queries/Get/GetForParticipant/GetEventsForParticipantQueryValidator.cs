using FluentValidation;
using static EventMaster.Domain.Constants.DomainConstants.Event;
namespace EventMaster.Application.EntityRequests.Events.Queries.Get.GetForParticipant;

public class GetEventsForParticipantQueryValidator : AbstractValidator<GetEventsForParticipantQuery>
{
    public GetEventsForParticipantQueryValidator()
    {
        RuleFor(x => x.Location)
            .MaximumLength(MaxLocationLength)
            .WithMessage($"Location must not exceed {MaxLocationLength} characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.Location));

        RuleFor(x => x.Date)
            .Must(date => date.HasValue && date.Value > DateTime.MinValue)
            .WithMessage("Date must be a valid date.");
    }
}