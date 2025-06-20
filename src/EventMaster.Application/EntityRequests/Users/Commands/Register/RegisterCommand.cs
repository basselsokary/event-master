using EventMaster.Domain.Enums;

namespace EventMaster.Application.EntityRequests.Users.Commands.Register;

public record RegisterCommand(
    string UserName,
    string Email,
    string Password,
    Role Role) : ICommand;