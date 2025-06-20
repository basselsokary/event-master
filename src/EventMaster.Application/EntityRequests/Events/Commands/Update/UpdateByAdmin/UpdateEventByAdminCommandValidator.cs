using FluentValidation;

namespace EventMaster.Application.EntityRequests.Events.Commands.Update.UpdateByAdmin;

public class UpdateEventByAdminCommandValidator : AbstractValidator<UpdateEventByAdminCommand>
{
    public UpdateEventByAdminCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Event ID is required.");
    }
}