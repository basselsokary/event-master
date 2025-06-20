namespace EventMaster.Application.EntityRequests.Auth.Commands.Refresh;

public record RefreshCommand(string RefreshToken) : ICommand<Response>;
