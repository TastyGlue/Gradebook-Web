namespace Gradebook.Data.Models;

[Index(nameof(SchoolId), nameof(Year), nameof(Semester), IsUnique = true, Name = SCHOOL_YEAR_UNIQUE_INDEX)]
public class SchoolYear
{
    public Guid Id { get; set; }

    [ForeignKey(nameof(School))]
    public Guid SchoolId { get; set; }

    public School School { get; set; } = default!;

    public int Year { get; set; }

    public int Semester { get; set; }

    public DateTime? Start { get; set; }

    public DateTime? End { get; set; }

    public ICollection<Grade> Grades { get; set; } = [];

    public ICollection<Absence> Absences { get; set; } = [];

    public ICollection<Timetable> Timetables { get; set; } = [];
}
