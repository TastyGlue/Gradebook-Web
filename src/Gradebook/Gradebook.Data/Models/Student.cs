namespace Gradebook.Data.Models;

public class Student : Profile
{
    public Guid SchoolId { get; set; }

    public Guid? ClassId { get; set; }
}