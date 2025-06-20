namespace EventMaster.Application.EntityRequests.Users.Queries.GetUser.ByEmail;

public record GetUserByEmailQuery(string Email) : IQuery<Response>;
