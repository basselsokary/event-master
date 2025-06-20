namespace EventMaster.Application.EntityRequests.Users.Commands.Login;

public record Response(string AccessToken, string RefreshToken);
