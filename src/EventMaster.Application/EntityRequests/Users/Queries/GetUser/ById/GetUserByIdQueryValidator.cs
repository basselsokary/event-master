using FluentValidation;

namespace EventMaster.Application.EntityRequests.Users.Queries.GetUser.ById;

public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required.")
            .MaximumLength(128);
    }
}