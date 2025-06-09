namespace Gradebook.Shared.Models.DTOs;

public class GradeDto
{
    public Guid Id { get; set; }

    public decimal Value { get; set; }

    public Guid SchoolYearId { get; set; }

    public SchoolYearDto SchoolYear { get; set; } = default!;

    public DateTime Date { get; set; }

    public Guid SubjectId { get; set; }

    public SubjectDto Subject { get; set; } = default!;

    public Guid StudentId { get; set; }

    public StudentDto Student { get; set; } = default!;

    public Guid? TeacherId { get; set; }

    public TeacherDto? Teacher { get; set; }
}
