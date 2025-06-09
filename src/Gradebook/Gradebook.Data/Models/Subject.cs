namespace Gradebook.Data.Models;

[Index(nameof(Name), nameof(SchoolId), IsUnique = true, Name = SUBJECT_NAME_INDEX)]
public class Subject
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    [ForeignKey(nameof(School))]
    public Guid SchoolId { get; set; }

    public School School { get; set; } = default!;

    public ICollection<Teacher> Teachers { get; set; } = [];

    public ICollection<Timetable> Timetables { get; set; } = [];
}
