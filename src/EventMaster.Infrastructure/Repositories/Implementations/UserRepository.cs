using EventMaster.Application.Common.Interfaces.Repositories;
using EventMaster.Domain.Constants;
using EventMaster.Application.DTOs;
using EventMaster.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using EventMaster.Domain.Enums;

namespace EventMaster.Infrastructure.Repositories.Implementations;

internal class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task<List<OrganizerDto>> GetOrganizersAsync(OrganizerStatus status, CancellationToken cancellationToken = default)
    {
        return await context.EventOrganizers
            .Where(eo => eo.Status == status)
            .Select(eo => new OrganizerDto()
            {
                Id = eo.Id,
                UserName = eo.UserName!,
                Email = eo.Email!,
                Status = eo.Status,
                Roles = new List<string>(){UserRoles.EventOrganizer}
            }).ToListAsync(cancellationToken);
    }

    public async Task<List<ParticipantDto>> GetParticipantsAsync(CancellationToken cancellationToken = default)
    {
        return await context.Participants
            .Select(p => new ParticipantDto()
            {
                Id = p.Id,
                UserName = p.UserName!,
                Email = p.Email!,
                Roles = new List<string>(){UserRoles.Participant}
            }).ToListAsync(cancellationToken);
    }

    public async Task<bool> ApproveOrganizerAsync(string organizerId, bool isApproved, CancellationToken cancellationToken = default)
    {
        var organizer = await context.EventOrganizers.FirstOrDefaultAsync(eo => eo.Id == organizerId, cancellationToken: cancellationToken);

        if (organizer == null)
            return false;

        organizer.Approve(isApproved);

        return true;
    }

    public async Task<bool> IsOrganizerApprovedAsync(string organizerId, CancellationToken cancellationToken = default)
        => await context.EventOrganizers.AnyAsync(
            eo => eo.Id == organizerId && eo.Status == OrganizerStatus.Approved, cancellationToken);
}
