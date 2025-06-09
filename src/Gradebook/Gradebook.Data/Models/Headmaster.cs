namespace Gradebook.Data.Models;

public class Headmaster : Profile, IBusinessEmail, ISchoolMember
{
    public string BusinessEmail { get; set; } = default!;

    public string BusinessPhoneNumber { get; set; } = default!;

    [ForeignKey(nameof(School))]
    public Guid? SchoolId { get; set; }

    public School School { get; set; } = default!;

    [NotMapped]
    [JsonIgnore]
    public string SchoolName => School?.Name ?? string.Empty;
}
