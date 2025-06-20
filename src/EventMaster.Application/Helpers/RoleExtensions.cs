using EventMaster.Domain.Constants;
using EventMaster.Domain.Enums;

namespace EventMaster.Application.Helpers;

public static class RoleExtensions
{
    public static string GetName(this Role role) => role switch
    {
        Role.Admin => UserRoles.Admin,
        Role.EventOrganizer => UserRoles.EventOrganizer,
        Role.Participant => UserRoles.Participant,
        _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
    };

    public static Role GetRole(this string roleString) => roleString switch
    {
        UserRoles.Admin => Role.Admin,
        UserRoles.EventOrganizer => Role.EventOrganizer,
        UserRoles.Participant => Role.Participant,
        _ => throw new ArgumentOutOfRangeException(roleString, roleString, null)
    };
}

