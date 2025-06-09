namespace Gradebook.Data.Models;

public class Absence
{
    public Guid Id { get; set; }

    public DateTime Date { get; set; }

    public bool Excused { get; set; }

    [ForeignKey(nameof(SchoolYear))]
    public Guid SchoolYearId { get; set; }

    public SchoolYear SchoolYear { get; set; } = default!;

    [ForeignKey(nameof(Student))]
    public Guid StudentId { get; set; }

    public Student Student { get; set; } = default!;

    [ForeignKey(nameof(Timetable))]
    public Guid TimetableId { get; set; }

    public Timetable Timetable { get; set; } = default!;
}
