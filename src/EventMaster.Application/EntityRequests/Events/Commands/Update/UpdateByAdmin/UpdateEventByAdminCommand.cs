namespace EventMaster.Application.EntityRequests.Events.Commands.Update.UpdateByAdmin;

public record UpdateEventByAdminCommand(Guid Id, bool IsApproved) : ICommand;
