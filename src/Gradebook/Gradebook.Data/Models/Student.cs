namespace Gradebook.Data.Models;

public class Student : Profile
{
    [ForeignKey(nameof(School))]
    public Guid SchoolId { get; set; }

    public School School { get; set; } = default!;

    [ForeignKey(nameof(Class))]
    public Guid? ClassId { get; set; }

    public Class? Class { get; set; }

    public ICollection<Grade> Grades { get; set; } = [];

    public ICollection<Absence> Absences { get; set; } = [];

    public ICollection<Parent> Parents { get; set; } = [];
}