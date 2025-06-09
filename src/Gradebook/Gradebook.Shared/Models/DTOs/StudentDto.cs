namespace Gradebook.Shared.Models.DTOs;

public class StudentDto : ProfileDto
{
    public Guid? SchoolId { get; set; }

    public SchoolDto School { get; set; } = default!;

    public Guid? ClassId { get; set; }

    public ClassDto? Class { get; set; }

    public ICollection<GradeDto> Grades { get; set; } = [];

    public ICollection<AbsenceDto> Absences { get; set; } = [];
}
