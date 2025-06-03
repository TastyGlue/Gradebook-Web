namespace Gradebook.Data.Models;

public class Grade
{
    public Guid Id { get; set; }

    public decimal Value { get; set; }

    public DateTime Date { get; set; }

    [ForeignKey(nameof(Subject))]
    public Guid SubjectId { get; set; }

    public Subject Subject { get; set; } = default!;

    [ForeignKey(nameof(Student))]
    public Guid StudentId { get; set; }

    public Student Student { get; set; } = default!;

    [ForeignKey(nameof(Teacher))]
    public Guid? TeacherId { get; set; }

    public Teacher? Teacher { get; set; }
}
