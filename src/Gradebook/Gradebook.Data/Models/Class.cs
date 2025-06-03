namespace Gradebook.Data.Models;

[Index(nameof(Year), nameof(Signature), nameof(SchoolId), IsUnique = true, Name = CLASS_SIGNATURE_INDEX)]
public class Class
{
    public Guid Id { get; set; }

    public int Year { get; set; }

    public string Signature { get; set; } = default!;

    [ForeignKey(nameof(School))]
    public Guid SchoolId { get; set; }

    public School School { get; set; } = default!;

    [ForeignKey(nameof(ClassTeacher))]
    public Guid? ClassTeacherId { get; set; }

    public Teacher? ClassTeacher { get; set; }
}
