namespace Gradebook.Data.Models;

public class Teacher : Profile, IBusinessEmail, ISchoolMember
{
    public string BusinessEmail { get; set; } = default!;

    public string BusinessPhoneNumber { get; set; } = default!;

    [ForeignKey(nameof(School))]
    public Guid? SchoolId { get; set; }

    public School School { get; set; } = default!;

    public Guid? ClassId { get; set; }

    public Class? Class { get; set; }

    public ICollection<Subject> Subjects { get; set; } = [];
}
