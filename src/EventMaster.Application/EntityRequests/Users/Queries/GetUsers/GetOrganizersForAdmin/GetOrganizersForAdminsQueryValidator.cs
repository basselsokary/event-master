using FluentValidation;

namespace EventMaster.Application.EntityRequests.Auth.Queries.GetUsers.GetOrganizersForAdmin;

public class GetOrganizersForAdminsQueryValidator : AbstractValidator<GetOrganizersForAdminsQuery>
{
    public GetOrganizersForAdminsQueryValidator()
    {
        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Invalid organizer status.");
    }
}