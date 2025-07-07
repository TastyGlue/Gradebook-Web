namespace Gradebook.Shared.Models.DTOs;

public class AbsenceDto
{
    public Guid Id { get; set; }

    public DateTime Date { get; set; }

    public bool Excused { get; set; }
    public bool IsLate { get; set; }

    public Guid SchoolYearId { get; set; }

    public SchoolYearDto? SchoolYear { get; set; } = default!;

    public Guid StudentId { get; set; }

    public StudentDto? Student { get; set; } = default!;

    public Guid TimetableId { get; set; }

    public TimetableDto? Timetable { get; set; } = default!;
}
