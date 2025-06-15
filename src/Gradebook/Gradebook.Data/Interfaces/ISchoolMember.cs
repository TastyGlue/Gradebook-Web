namespace Gradebook.Data.Interfaces;

public interface ISchoolMember
{
    Guid? SchoolId { get; set; }

    School School { get; set; }
}
