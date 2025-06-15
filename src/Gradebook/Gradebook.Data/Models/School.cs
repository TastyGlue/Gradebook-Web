namespace Gradebook.Data.Models;

[Index(nameof(Name), IsUnique = true, Name = SCHOOL_NAME_INDEX)]
public class School
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public string Address { get; set; } = default!;

    public string? Website { get; set; }

    public bool IsActive { get; set; }

    public ICollection<Class> Classes { get; set; } = [];

    public ICollection<Headmaster> Headmasters { get; set; } = [];

    public ICollection<Teacher> Teachers { get; set; } = [];

    public ICollection<Student> Students { get; set; } = [];

    public ICollection<SchoolYear> SchoolYears { get; set; } = [];
}
