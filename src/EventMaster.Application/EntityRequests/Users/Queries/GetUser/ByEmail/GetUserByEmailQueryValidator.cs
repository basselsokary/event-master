using FluentValidation;
using static EventMaster.Domain.Constants.DomainConstants.User;

namespace EventMaster.Application.EntityRequests.Users.Queries.GetUser.ByEmail;
public class GetUserByEmailQueryValidator : AbstractValidator<GetUserByEmailQuery>
{
    public GetUserByEmailQueryValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.")
            .MaximumLength(MaxEmailLength)
            .WithMessage($"Email must not exceed {MaxEmailLength} characters.");
    }
}