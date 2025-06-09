namespace Gradebook.Data.Models;

public class Timetable
{
    public Guid Id { get; set; }

    [ForeignKey(nameof(SchoolYear))]
    public Guid SchoolYearId { get; set; }

    public SchoolYear SchoolYear { get; set; } = default!;

    public DateTime TimeOfDay { get; set; }

    public DayOfWeek DayOfWeek { get; set; }

    [ForeignKey(nameof(Teacher))]
    public Guid? TeacherId { get; set; }

    public Teacher? Teacher { get; set; }

    [ForeignKey(nameof(Class))]
    public Guid? ClassId { get; set; }

    public Class? Class { get; set; }

    [ForeignKey(nameof(Subject))]
    public Guid SubjectId { get; set; }

    public Subject Subject { get; set; } = default!;
}
