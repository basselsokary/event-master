using FluentValidation;

namespace EventMaster.Application.EntityRequests.Auth.Commands.OrganizerStatus;

public class ChangeOrganizerStatusCommandValidator : AbstractValidator<ChangeOrganizerStatusCommand>
{
    public ChangeOrganizerStatusCommandValidator()
    {
        RuleFor(x => x.OrganizerId)
            .NotEmpty()
            .WithMessage("Organizer ID is required.");
    }
}
