namespace Gradebook.Data.Models;

[PrimaryKey(nameof(StudentId), nameof(ParentId))]
public class StudentParent
{
    [ForeignKey(nameof(Student))]
    public Guid StudentId { get; set; }

    public Student Student { get; set; } = default!;

    [ForeignKey(nameof(Parent))]
    public Guid ParentId { get; set; }

    public Parent Parent { get; set; } = default!;
}
