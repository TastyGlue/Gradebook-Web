namespace Gradebook.Shared.Models.DTOs;

public class TeacherDto : ProfileDto
{
    public string BusinessEmail { get; set; } = default!;

    public string BusinessPhoneNumber { get; set; } = default!;

    public Guid SchoolId { get; set; }

    public SchoolDto? School { get; set; } = default!;

    public Guid? ClassId { get; set; }

    public ClassDto? Class { get; set; }

    public ICollection<SubjectDto>? Subjects { get; set; } = [];
}
