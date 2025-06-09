namespace Gradebook.Shared.Models.DTOs;

public class UserDto
{
    public Guid Id { get; set; }

    public string UserName { get; set; } = default!;

    public string Email { get; set; } = default!;

    public string? PhoneNumber { get; set; }

    public string FullName { get; set; } = default!;

    public bool IsActive { get; set; }

    public ICollection<ProfileDto> Profiles { get; set; } = [];
}
