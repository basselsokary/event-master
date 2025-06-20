using EventMaster.Domain.Enums;

namespace EventMaster.Application.EntityRequests.Auth.Queries.GetUsers.GetOrganizersForAdmin;

public record Response(
    string Id,
    string UserName,
    string Email,
    OrganizerStatus Status
);