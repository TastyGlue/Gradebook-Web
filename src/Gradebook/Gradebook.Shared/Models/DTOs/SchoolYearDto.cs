namespace Gradebook.Shared.Models.DTOs;

public class SchoolYearDto
{
    public Guid Id { get; set; } = default!;

    public Guid SchoolId { get; set; } = default!;

    public SchoolDto School { get; set; } = default!;

    public int Year { get; set; }

    public int Semester { get; set; }

    public DateTime? Start { get; set; }

    public DateTime? End { get; set; }
}
