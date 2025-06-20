namespace EventMaster.Application.EntityRequests.Users.Queries.GetUser.ById;

public record GetUserByIdQuery(string UserId) : IQuery<Response>;
