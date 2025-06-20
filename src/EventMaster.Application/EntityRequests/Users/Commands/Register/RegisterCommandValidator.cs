using FluentValidation;
using static EventMaster.Domain.Constants.DomainConstants.User;
namespace EventMaster.Application.EntityRequests.Users.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .MaximumLength(MaxUserNameLength)
            .WithMessage($"Username must not exceed {MaxUserNameLength} characters.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Invalid email format.")
            .MaximumLength(MaxEmailLength)
            .WithMessage($"Email must not exceed {MaxEmailLength} characters.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(MinPasswordLength)
            .WithMessage($"Password must be at least {MinPasswordLength} characters long.")
            .MaximumLength(MaxPasswordLength)
            .WithMessage($"Password must not exceed {MaxPasswordLength} characters.");

        RuleFor(x => x.Role)
            .IsInEnum()
            .WithMessage("Invalid enum value.");
    }
}