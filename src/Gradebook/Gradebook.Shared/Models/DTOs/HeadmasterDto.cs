namespace Gradebook.Shared.Models.DTOs;

public class HeadmasterDto : ProfileDto
{
    public string BusinessEmail { get; set; } = default!;

    public string BusinessPhoneNumber { get; set; } = default!;

    public Guid SchoolId { get; set; }

    public SchoolDto School { get; set; } = default!;
}
