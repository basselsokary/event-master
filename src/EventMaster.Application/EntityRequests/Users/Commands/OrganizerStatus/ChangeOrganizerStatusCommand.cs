namespace EventMaster.Application.EntityRequests.Auth.Commands.OrganizerStatus;

public record ChangeOrganizerStatusCommand(
    string OrganizerId,
    bool IsApproved) : ICommand;