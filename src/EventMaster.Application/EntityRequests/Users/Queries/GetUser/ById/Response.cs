namespace EventMaster.Application.EntityRequests.Users.Queries.GetUser.ById;

public record Response(
    string Id,
    string UserName,
    string Email,
    IEnumerable<string> Roles);
