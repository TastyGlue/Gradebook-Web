namespace Gradebook.Data.Models;

public class Headmaster : Profile
{
    public string BusinessEmail { get; set; } = default!;

    public string BusinessPhoneNumber { get; set; } = default!;

    [ForeignKey(nameof(School))]
    public Guid SchoolId { get; set; }

    public School School { get; set; } = default!;
}
