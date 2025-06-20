using EventMaster.Domain.Enums;

namespace EventMaster.Application.DTOs;

public class OrganizerDto : UserDto
{
    public OrganizerStatus Status { get; set; }
}
