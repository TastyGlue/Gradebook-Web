namespace Gradebook.Shared.Models.DTOs;

public class ProfileDto
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public UserDto? User { get; set; } = default!;

    public RoleType RoleType { get; set; }

    public bool IsActive { get; set; }
}
