namespace Gradebook.Shared.Models.DTOs;

public class SubjectDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public Guid SchoolId { get; set; }

    public SchoolDto School { get; set; } = default!;

    public ICollection<TeacherDto> Teachers { get; set; } = [];
}
