namespace EventMaster.Application.EntityRequests.Users.Commands.Login;

public record LoginCommand(
    string Email,
    string Password) : ICommand<Response>;
