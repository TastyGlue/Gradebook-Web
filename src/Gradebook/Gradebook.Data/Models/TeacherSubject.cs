namespace Gradebook.Data.Models;

[PrimaryKey(nameof(TeacherId), nameof(SubjectId))]
public class TeacherSubject
{
    [ForeignKey(nameof(Teacher))]
    public Guid TeacherId { get; set; }

    public Teacher Teacher { get; set; } = default!;

    [ForeignKey(nameof(Subject))]
    public Guid SubjectId { get; set; }

    public Subject Subject { get; set; } = default!;
}
