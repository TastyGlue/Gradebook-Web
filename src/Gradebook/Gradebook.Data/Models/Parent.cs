namespace Gradebook.Data.Models;

public class Parent : Profile
{
    public ICollection<Student> Students { get; set; } = [];
}
