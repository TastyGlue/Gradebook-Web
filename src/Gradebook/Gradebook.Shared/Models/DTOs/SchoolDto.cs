namespace Gradebook.Shared.Models.DTOs;

public class SchoolDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public string Address { get; set; } = default!;

    public string? Website { get; set; }

    public bool IsActive { get; set; }

    public ICollection<HeadmasterDto> Headmasters { get; set; } = [];

    public ICollection<SchoolYearDto> SchoolYears { get; set; } = [];
}
