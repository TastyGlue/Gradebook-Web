namespace Gradebook.Shared.Models.DTOs;

public class ClassDto
{
    public Guid Id { get; set; }

    public int Year { get; set; }

    public string Signature { get; set; } = default!;

    public Guid SchoolId { get; set; }

    public SchoolDto School { get; set; } = default!;

    public Guid? ClassTeacherId { get; set; }

    public TeacherDto? ClassTeacher { get; set; }

    public ICollection<StudentDto> Students { get; set; } = [];

    public ICollection<TimetableDto> Timetables { get; set; } = [];
}
