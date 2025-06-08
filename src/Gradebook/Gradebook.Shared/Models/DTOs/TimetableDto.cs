namespace Gradebook.Shared.Models.DTOs;

public class TimetableDto
{
    public Guid Id { get; set; }

    public Guid SchoolYearId { get; set; }

    public SchoolYearDto SchoolYear { get; set; } = default!;

    public DateTime TimeOfDay { get; set; }

    public DayOfWeek DayOfWeek { get; set; }

    public Guid? TeacherId { get; set; }

    public TeacherDto? Teacher { get; set; }

    public Guid? ClassId { get; set; }

    public ClassDto? Class { get; set; }

    public Guid SubjectId { get; set; }

    public SubjectDto Subject { get; set; } = default!;
}
