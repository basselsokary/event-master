using EventMaster.Domain.Enums;

namespace EventMaster.Application.EntityRequests.Auth.Queries.GetUsers.GetOrganizersForAdmin;

public record GetOrganizersForAdminsQuery(OrganizerStatus Status) : IQuery<List<Response>>;
