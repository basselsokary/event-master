using EventMaster.Domain.Entities;
using EventMaster.Infrastructure.Authentication;
using Microsoft.AspNetCore.Identity;

namespace EventMaster.Infrastructure.User;

public class AppUser : IdentityUser
{
    public string? FullName { get; set; }

    //public string RoleType { get; set; } = string.Empty; // This could be "Admin", "Organizer", or "Participant"

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Notification> Notifications { get; set; } = null!;
    public ICollection<RefreshToken> RefreshTokens { get; set; } = null!;
}
