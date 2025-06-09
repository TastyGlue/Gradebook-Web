namespace Gradebook.Shared.Interfaces;

public interface ISchoolMember
{
    Guid? SchoolId { get; set; }

    string SchoolName { get; }
}
